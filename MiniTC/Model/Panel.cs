using System.Collections.Generic;
using System.IO;

namespace MiniTC.Model
{
    class Panel
    {
        //Zwróć wszystkie dostępne dyski
        public string[] GetAllDrives() => Directory.GetLogicalDrives();
        //Zwróć wszystkie pliki dla ścieżki
        public string[] GetAllFiles(string path)
        {
            List<string> Content = new List<string>();
            try
            {
                if (Directory.GetParent(path) != null)
                    Content.Add("..");

                string[] Directories = Directory.GetDirectories(path);
                string[] Files = Directory.GetFiles(path);

                for (int i=0; i<Directories.Length; i++)
                    Content.Add("<D>" + Path.GetFileName(Directories[i]));

                for (int j = 0; j < Files.Length; j++)
                    Content.Add(Path.GetFileName(Files[j]));

            }
            catch {}
            return Content.ToArray();
        }
        //Przejdź do nowego folderu
        public string ChangePath(string path, string selectedFile)
        {
            if (selectedFile != null && selectedFile.Contains("<D>") && selectedFile != ".." )
            {
                selectedFile = selectedFile.Replace("<D>", "");
                string newPath = Path.Combine(path, selectedFile);
                return newPath;
            }

            if (selectedFile == "..")
                path = GetParentOfFile(path);

            return path;
        }
        //Zwróć folder nadrzędny
        public static string GetParentOfFile(string path) => Directory.GetParent(path).FullName;
        //Kopiuj plik
        public void CopyFile(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true);
            }
            catch {}
        }
    }
}
