using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestExtentReportFix
{
    public class ExtentReportFileFix
    {
        public static void Fix(String fullPathToFile, String pathToAssetsFolder)
        {
            //copy the assest folder to where the html file is
            String reportFolder = Path.GetDirectoryName(fullPathToFile);
            String pathToCopyTo = Path.Combine(reportFolder, "assets");
            Directory.CreateDirectory(pathToCopyTo);
            DirectoryCopy(pathToAssetsFolder,pathToCopyTo, true);

            //read the html file
            string htmlFileContent = File.ReadAllText(fullPathToFile);

            /*  OLD VERSION
            //replacing refrences from the internet to the local assets folder
            htmlFileContent = htmlFileContent.Replace(@"https://cdn.jsdelivr.net/gh/extent-framework/extent-github-cdn@3da944e596e81f10ac6c1042c792af8a5a741023/commons/img/logo.png", @"assets/logo.png");
            htmlFileContent = htmlFileContent.Replace(@"https://cdn.jsdelivr.net/gh/extent-framework/extent-github-cdn@8539670db814bf19a0ddbe9a6d19058aea0cf981/spark/css/spark-style.css", @"assets/spark-style.css");
            htmlFileContent = htmlFileContent.Replace(@"https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css", @"assets/font-awesome.min.css");
            htmlFileContent = htmlFileContent.Replace(@"https://cdn.rawgit.com/extent-framework/extent-github-cdn/7cc78ce/spark/js/jsontree.js", @"assets/jsontree.js");
            htmlFileContent = htmlFileContent.Replace(@"https://cdn.rawgit.com/extent-framework/extent-github-cdn/d74480e/commons/img/logo.png", @"assets/logo-dark/logo.png");
            htmlFileContent = htmlFileContent.Replace(@"https://cdn.jsdelivr.net/gh/extent-framework/extent-github-cdn@cc89904c1a12065be1d08fc4994a258ef01b12bd/spark/js/scripts.js", @"assets/scripts.js");
            */

            //replacing refrences from the internet to the local assets folder
            htmlFileContent = htmlFileContent.Replace(@"https://fonts.googleapis.com/css?family=Source+Sans+Pro:400,600", @"assets/source_san_pro.css");
            htmlFileContent = htmlFileContent.Replace(@"https://cdn.rawgit.com/anshooarora/extentreports/97fc3fe7f55cba86a4f5b6ff9a2bb80de3e4867c/cdn/extent.css", @"assets/extent.css");
            htmlFileContent = htmlFileContent.Replace(@"https://cdn.rawgit.com/anshooarora/extentreports/051be9b627c84bde3591f7e6268e8b70e334a760/cdn/extent.js", @"assets/extent.js");
            
            //writing the updated html file back to disk
            File.WriteAllText(fullPathToFile, htmlFileContent);
        }



        //taken from: https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source irectory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
