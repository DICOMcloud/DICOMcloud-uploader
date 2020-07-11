using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace DICOMcloudUploader
{
    static class DICOMStore
    {
        /// <summary>
        /// The method will read all files in a directory/sub-directories and send a DICOMweb Store request (STOW-RS)
        /// Each 5 DICOM files will be grouped as a multi-part content and sent in a single request.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="storeUrl"></param>
        public static void StoreDicomInDirectory(string directory, string storeUrl)
        {
            var mimeType = "application/dicom";
            MultipartContent multiContent = GetMultipartContent(mimeType);
            int count = 0;

            //Enumerate all files in a directory/sub-directories
            foreach (var path in Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories))
            {
                count++;

                StreamContent sContent = new StreamContent(File.OpenRead(path));

                sContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);

                multiContent.Add(sContent);

                if (count % 5 == 0)
                {
                    count = 0;

                    StoreToServer(multiContent, storeUrl);

                    multiContent = GetMultipartContent(mimeType);
                }
            }

            //Flush any remaining images (should be less than 5)
            if (multiContent.Count() > 0)
            {
                StoreToServer(multiContent, storeUrl);
            }
        }

        /// <summary>
        /// Get a valid multipart content.
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static MultipartContent GetMultipartContent(string mimeType)
        {
            var multiContent = new MultipartContent("related", "DICOM DATA BOUNDARY");

            multiContent.Headers.ContentType.Parameters.Add(new System.Net.Http.Headers.NameValueHeaderValue("type", "\"" + mimeType + "\""));
            return multiContent;
        }

        /// <summary>
        /// Send the multipart content to the server using the STOW-RS service
        /// </summary>
        /// <param name="multiContent"></param>
        /// <param name="storeUrl"></param>
        private static void StoreToServer(MultipartContent multiContent, string storeUrl)
        {
            try
            {
                HttpClient client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, storeUrl);

                request.Content = multiContent;

                var result = client.SendAsync(request);

                result.Wait();

                HttpResponseMessage response = result.Result;

                Console.WriteLine(response.StatusCode);

                var result2 = response.Content.ReadAsStringAsync();

                result2.Wait();

                string responseText = result2.Result;

                Console.WriteLine(responseText);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }
    }
}
