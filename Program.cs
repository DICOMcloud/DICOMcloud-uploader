using System;
using System.IO;
using System.Text;
using System.CommandLine;
using System.CommandLine.DragonFruit;

namespace DICOMcloudUploader
{
    class Program
    {
        static void Main(string dir = null, string url = null)
        {
            string uploadFolder = "";
            url ??= "http://localhost:44301/stowrs/";

            if (string.IsNullOrWhiteSpace(dir))
            { 
                Console.WriteLine("Enter path for DICOM directory to upload:");

                uploadFolder = Console.ReadLine();
            }
            else
            { 
                uploadFolder = dir;
            }

            string input = "";
            
            do
            { 
                StringBuilder sb = new StringBuilder ( );

                sb.AppendLine($"1. DICOM Directory: {uploadFolder}");
                sb.AppendLine($"2. DICOMweb Store Endpoint: {url}");
                sb.AppendLine($"Press \"Enter\" to accept, \"1\" to change DICOM directory, \"2\" to change store endpoint or any key to exit");
                Console.WriteLine(sb);
            
                input = Console.ReadLine().Trim();

                if (input == "1")
                {
                    Console.WriteLine("Enter path for DICOM directory to upload:");
                    uploadFolder = Console.ReadLine();
                }
                else if (input == "2")
                {
                    Console.WriteLine("Enter URL for DICOMweb Store endpoint:");
                    url = Console.ReadLine();
                }
                else if (input != "")
                { 
                    return;
                }
            } while (input != "");

            if (!Directory.Exists(uploadFolder))
            {
                Console.WriteLine("DICOM upload directory doesn't exists.");

                return;
            }

            DateTime startTime = DateTime.Now;
            Console.WriteLine($"Upload start time: {startTime}");
            DICOMStore.StoreDicomInDirectory(uploadFolder, url);
            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Upload end time: {endTime}");

            Console.WriteLine($"Total upload time: {endTime - startTime}");

        }
    }
}
