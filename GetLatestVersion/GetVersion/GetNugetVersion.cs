using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetVersion
{
    public class GetNugetVersion
    {
        public static string DevBranch
        {
            get
            {
                return @"\\nuget\NuGet\Share\drops\ci\NuGet.Client\dev";
            }
        }

        public static string ReleaseRC240Branch
        {
            get
            {
                return @"\\nuget\NuGet\Share\drops\ci\NuGet.Client\release-4.0.0-rc2";
            }
        }

        public static string Release40RC3Branch
        {
            get
            {
                return @"\\nuget\NuGet\Share\drops\ci\NuGet.Client\release-4.0.0-rc3";
            }
        }

        public static string Release40RTMBranch
        {
            get
            {
                return @"\\nuget\nuget\Share\drops\ci\NuGet.Client\release-4.0.0-rtm";
            }
        }

        public static string Release41RTMBranch
        {
            get
            {
                return @"\\nuget\nuget\Share\drops\ci\NuGet.Client\release-4.1.0-rtm";
            }
        }

        public static string Release42RTMBranch
        {
            get
            {
                return @"\\nuget\nuget\Share\drops\ci\NuGet.Client\release-4.2.0-rtm";
            }
        }

        public static string DevBranchTrackFile
        {
            get
            {
                return @"\\nugettestserver\nugetbuild\dev.sem";
            }
        }

        public static string Release40RC3BranchTrackFile
        {
            get
            {
                return @"\\nugettestserver\nugetbuild\release40rc3.sem";
            }
        }

        public static string Release40RTMBranchTrackFile
        {
            get
            {
                return @"\\nugettestserver\nugetbuild\release40rtm.sem";
            }
        }

        public static string Release41RTMBranchTrackFile
        {
            get
            {
                return @"\\nugettestserver\nugetbuild\release41rtm.sem";
            }
        }

        public static string Release42RTMBranchTrackFile
        {
            get
            {
                return @"\\nugettestserver\nugetbuild\release42rtm.sem";
            }
        }
        public string getLatestVersion(string branchName)
        {
            string latestVersionNumber = null;

            DirectoryInfo di = new DirectoryInfo(branchName);
            DirectoryInfo[] diList = di.GetDirectories();

            List<int> folder = new List<int>();

            for (int i = 0; i < diList.Length; i++)
            {
                folder.Add(Int32.Parse(diList[i].Name));
            }

            latestVersionNumber = folder.Max().ToString();

            return latestVersionNumber;
        }

        public void RecordVersionToFile(string filePath, string version)
        {
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
