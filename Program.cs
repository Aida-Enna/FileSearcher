using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace FileSearcher
{
    internal class Program
    {
        private static readonly byte[] TextToFind = Encoding.Default.GetBytes("acTL"); //Text to search the file for

        private static void Main(string[] args)
        {
            string FolderPath = @"C:\Stuff\idk\"; //Change this to whatever directory

            string[] fileArray = Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories); // and filetype

            foreach (string Filename in fileArray)
            {
                //Read file bytes.
                byte[] fileContent = File.ReadAllBytes(Filename);

                //Look for text
                for (int p = 0; p < fileContent.Length; p++)
                {
                    if (!SearchForText(fileContent, p)) continue;

                    for (int w = 0; w < TextToFind.Length; w++)
                    {
                        //We found the thing! Let's do something with it!
                        if (!Directory.Exists(Path.Combine(FolderPath,"Matches"))) { Directory.CreateDirectory(Path.Combine(FolderPath, "Matches")); }
                        File.Copy(Filename, Path.Combine(FolderPath, @"Matches\" + Filename.Split('\\')[Filename.Split('\\').Length-1]));
                        break;
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool SearchForText(byte[] sequence, int position)
        {
            if (position + TextToFind.Length > sequence.Length) return false;
            for (int p = 0; p < TextToFind.Length; p++)
            {
                if (TextToFind[p] != sequence[position + p]) return false;
            }
            return true;
        }
    }
}