using Voxart.Shared;

namespace Voxart.Lib.LowManage
{
    public class LocalDirectory
    {
        private static readonly DirectoryInfo _appData;

        public static DirectoryInfo RootDirectory => _appData;

        static LocalDirectory()
        {
            _appData = Directory.CreateDirectory("AppData");
        }

        public static DirectoryInfo AddDirectory(string name)
        {
            return _appData.CreateSubdirectory(name);
        }

        public static DirectoryInfo AddDirectory(DirectoryInfo directory, string name)
        {
            return directory.CreateSubdirectory(name);
        }

        public static FileInfo CreateFile(DirectoryInfo directory, string name, FileFormat format, string data)
        {
            var path = Path.Combine(directory.FullName, name + Help.GetTextFormat(format));
            File.WriteAllText(path, data);
            return new FileInfo(path);
        }

        public static FileInfo CreateFile(DirectoryInfo directory, string name, FileFormat format, byte[] data)
        {
            var path = Path.Combine(directory.FullName, name + Help.GetTextFormat(format));
            File.WriteAllBytes(path, data);
            return new FileInfo(path);
        }
    }
}
