using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Http;

namespace Utilities
{
    public class FileUploadHelper
    {
        public FileUploadHelper()
        {
        }

        public async Task<bool> UploadImageToAzureStorage(HttpPostedFileBase image/*, HttpRequest request*/)
        {
            try
            {
                //Console.WriteLine("request.Files.Count: " + request.Files.Count);
                Console.WriteLine("image==null: " + image == null);
                Console.WriteLine(image == null ? "" : "image.ContentLength: " + image.ContentLength + ", image.ContentType: " + image.ContentType);

                if (/*request.Files != null && */image != null && image.ContentLength != 0)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["AzureStorageConnectionString"].ConnectionString;
                    //Connect to Azure
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

                    // Create a reference to the file client.
                    CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

                    // Get a reference to the file share we created previously.
                    CloudFileShare share = fileClient.GetShareReference("organizationfiles");
                    await share.CreateIfNotExistsAsync();

                    if (share.Exists())
                    {
                        // Generate a SAS for a file in the share
                        CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                        CloudFileDirectory cloudFileDirectory = rootDir.GetDirectoryReference("organizationlogos");
                        await cloudFileDirectory.CreateIfNotExistsAsync();
                        CloudFile cloudFile = cloudFileDirectory.GetFileReference(image.FileName);

                        Stream fileStream = image.InputStream;

                        cloudFile.UploadFromStream(fileStream);
                        fileStream.Dispose();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                //throw ex;
                return false;
            }
        }

        public bool UploadImageToDisk()
        {
            return true;
        }

        public bool UploadFileToAzureStorage()
        {
            //string connectionString = "";

            ////Connect to Azure
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse();

            //// Create a reference to the file client.
            //CloudFileClient = storageAccount.CreateCloudFileClient();

            //// Create a reference to the Azure path
            //CloudFileDirectory cloudFileDirectory = GetCloudFileShare().GetRootDirectoryReference().GetDirectoryReference(path);

            ////Create a reference to the filename that you will be uploading
            //CloudFile cloudFile = cloudSubDirectory.GetFileReference(fileName);

            ////Open a stream from a local file.
            //Stream fileStream = File.OpenRead(localfile);

            ////Upload the file to Azure.
            //await cloudFile.UploadFromStreamAsync(fileStream);
            //fileStream.Dispose();

            return true;
        }

        public Task<HttpResponseMessage> UploadFileToDisk(HttpRequestMessage request)
        {
            //HttpRequestMessage request = this.Request; // use this if working directly from a controller
            //if (!request.Content.IsMimeMultipartContent())
            //{
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //}

            //string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
            //var provider = new MultipartFormDataStreamProvider(root);

            //var task = request.Content.ReadAsMultipartAsync(provider).
            //    ContinueWith<HttpResponseMessage>(o =>
            //    {

            //        string file1 = provider.FileData[0].LocalFileName;
            //        // this is the file name on the server where the file was saved 

            //        return new HttpResponseMessage()
            //        {
            //            Content = new StringContent("File uploaded.")
            //        };
            //    }
            //);
            //return task;
            return null;
        }
    }
}