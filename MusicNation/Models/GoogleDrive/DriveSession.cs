using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        static readonly string[] Scopes = { DriveService.Scope.Drive };
        static readonly string ApplicationName = "Music Nation";

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

        public List<(string, string)> GetFiles()
        {
            // Define parameters of request.
            FilesResource.ListRequest listRequest = Service.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name)";

            var result = new List<(string, string)>();

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    result.Add((file.Name, file.DriveId));
                }
            }

            return result;
        }

        public string GetFileId(string name)
        {
            // Define parameters of request.
            FilesResource.ListRequest listRequest = Service.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;

            return files.First(e => e.Name == name).Id;
        }

        public async Task<IActionResult> Upload(MemoryStream stream, string filename)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File {Name = filename, MimeType = "audio/mpeg"};

            var request = Service.Files.Create(fileMetadata, stream, fileMetadata.MimeType);
            request.Fields = "id";
            await request.UploadAsync();

            return new OkResult();
        }

        public async Task<MemoryStream> Download(string id)
        {
            var path = $"C:\\Users\\Mike\\source\\repos\\MusicNation\\{id}.mp3";

            var stream = new MemoryStream();

            await Service.Files.Get(id).DownloadAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
