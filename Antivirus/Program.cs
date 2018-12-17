using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Antivirus
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            String _base = System.AppDomain.CurrentDomain.BaseDirectory;
            String[] dirs = Directory.GetDirectories(_base);
            String[] _files = Directory.GetFiles(_base);
            foreach (String dir in dirs)
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                if (di.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    String[] hiddenFiles = Directory.GetFiles(Path.Combine(_base, dir));
                    foreach(String file in hiddenFiles)
                    {
                        foreach(String badFile in _files)
                        {
                            if(file.Split('\\').Last().Split('.')[0] == badFile.Split('\\').Last().Split('.')[0])
                            {
                                File.Delete(badFile);
                                File.Move(Path.Combine(_base, dir, file.Split('\\').Last()), Path.Combine(_base, file.Split('\\').Last()));
                            } else if(badFile.Split('.')[1] == "exe" && badFile.Split('\\').Last() != String.Concat(Process.GetCurrentProcess().ProcessName, ".exe"))
                            {
                                File.Delete(badFile);
                            }
                        }
                    }
                    Directory.Delete(dir);
                }
            }
        }
    }
}
