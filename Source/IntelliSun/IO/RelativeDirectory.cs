using System;
using System.IO;

namespace IntelliSun.IO
{
    public class RelativeDirectory
    {
        private DirectoryInfo dirInfo;

        public string Dir
        {
            get
            {
                return dirInfo.Name;
            }
        }

        public string Path
        {
            get { return dirInfo.FullName; }
            set
            {
                try
                {
                    var newDir = new DirectoryInfo(value);
                    dirInfo = newDir;
                }
                catch
                {
                    throw new DirectoryNotFoundException();
                }
            }
        }

        public RelativeDirectory(string dirPath)
        {
            dirInfo = new DirectoryInfo(dirPath);
        }

        public RelativeDirectory(DirectoryInfo dir)
        {
            dirInfo = dir;
        }

        public string Up(int numLevels)
        {
            for (var i = 0; i < numLevels; i++)
            {
                var tempDir = dirInfo.Parent;
                if (tempDir != null)
                    dirInfo = tempDir;
                else
                    break;
            }
            return dirInfo.FullName;
        }

        public string Up()
        {
            return Up(1);
        }

        public bool Down(string match)
        {
            var directories = dirInfo.GetDirectories(match + '*');
            dirInfo = directories[0];
            return true;
        }

    }
}
