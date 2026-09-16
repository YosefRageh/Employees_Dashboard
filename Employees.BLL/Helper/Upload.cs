
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Employees.PL.Helpers
{
    public static class Upload
    {
        // =========================
        // Upload File
        // =========================
        public static string UploadFile(
            string webRootPath,
            string FolderName,
            IFormFile File)
        {
            try
            {
                // Create folder path
                string FolderPath = Path.Combine(
                    webRootPath,
                    FolderName
                );

                // Create folder if it doesn't exist
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                // Generate unique file name
                string FileName =
                    Guid.NewGuid().ToString() +
                    Path.GetFileName(File.FileName);

                // Create final file path
                string FinalPath = Path.Combine(
                    FolderPath,
                    FileName
                );

                // Save file
                using (var Stream = new FileStream(
                    FinalPath,
                    FileMode.Create))
                {
                    File.CopyTo(Stream);
                }

                // Return saved file name
                return FileName;
            }
            catch (Exception)
            {
                return null;
            }
        }


        // =========================
        // Remove File
        // =========================
        public static string RemoveFile(
            string webRootPath,
            string FolderName,
            string fileName)
        {
            try
            {
                // Get file path
                var directory = Path.Combine(
                    webRootPath,
                    FolderName,
                    fileName
                );

                // Check if file exists
                if (File.Exists(directory))
                {
                    // Delete file
                    File.Delete(directory);

                    return "File Deleted";
                }

                return "File Not Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}


