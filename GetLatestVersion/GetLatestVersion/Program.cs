using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetLatestVersion
{
    class Program
    {
        private static string nugetDevfolder = @"\\nuget\NuGet\Share\drops\ci\NuGet.Client\dev";
        private static string nuget35Rtmfolder = @"\\nuget\NuGet\Share\drops\ci\NuGet.Client\release-3.5.0-rtm";
        private static string nuget36Beta1foler = @"\\nuget\NuGet\Share\drops\ci\NuGet.Client\release-3.6.0-beta1";

        static void Main(string[] args)
        {
            //GetLatestVersionNumber();
            WriteVersionNumberToFile(GetLatestVersionNumber());
        }

        public static int GetLatestVersionNumber()
        {
            //Update DirtctoryInfo to attribute later
            DirectoryInfo di = new DirectoryInfo(nugetDevfolder);
            DirectoryInfo[] diList = di.GetDirectories();

            List<int> folder = new List<int>();

            //Update latestVersion to attribute later
            int latestVersionNumber = 0;

            for (int i = 0; i < diList.Length; i++)
            {
                folder.Add(Int32.Parse(diList[i].Name));
            }

            latestVersionNumber = folder.Max();

            return latestVersionNumber;
        }

        public static void WriteVersionNumberToFile(int version)
        {
            string filePath = "Version.sem";

            if (!File.Exists(filePath))
            {
                FileStream stream = File.Create(filePath);
                stream.Close();
            }
            else
            {
                StreamWriter writer = new StreamWriter(filePath, false);
                writer.WriteLine();
                writer.Close();
            }

            File.WriteAllText(filePath, "revision=");
            File.AppendAllText(filePath, version.ToString());
        }
    }
}
