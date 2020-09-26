﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace MusicNation.Models.GoogleDrive
{
    public class DriveSession
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Music Nation";

        private UserCredential Credential { get; set; }
        private DriveService Service { get; set; }
        private bool IsAuthorized { get; set; }

        public DriveSession()
        {
            if (!IsAuthorized)
            {
                Authorize();
            }
        }

        private void Authorize()
        {
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                Credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            Service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = ApplicationName,
            });

            IsAuthorized = true;
        }

        public List<string> GetFiles()
        {
            // Define parameters of request.
            FilesResource.ListRequest listRequest = Service.Files.List();
            listRequest.PageSize = 100;
            listRequest.Fields = "nextPageToken, files(id, name)";

            var result = new List<string>();

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    result.Add(file.Name);
                }
            }

            return result;
        }

        public async Task<IActionResult> Upload(IFormFile file)
        {
            string filePath = "~/temp/" + file.Name;

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = file.Name;
            fileMetadata.MimeType = MimeTypes.GetMimeType(filePath);

            FilesResource.CreateMediaUpload request;

            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = Service.Files.Create(fileMetadata, stream, fileMetadata.MimeType);
                request.Fields = "id";
                await request.UploadAsync();
            }

            return new OkResult();
        }

        public async Task<MemoryStream> Download(string id)
        {
            var stream = new MemoryStream();
            
            await Service.Files.Get(id).DownloadAsync(stream);

            return stream;
        }
    }
}
