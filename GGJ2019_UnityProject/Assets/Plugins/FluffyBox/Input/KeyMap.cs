using System.IO;
using System.Collections.Generic;

namespace FluffyBox
{
    public class KeyMap
    {
        public string MapName;
        public string FolderPath;
        public List<KeyAction> KeyActions;

        public KeyMap()
        {
            this.KeyActions = new List<KeyAction>();
        }

        public KeyMap(string folderPath)
        {
            this.KeyActions = new List<KeyAction>();

            if (Directory.Exists(folderPath))
            {
                DirectoryInfo info = new DirectoryInfo(folderPath);
                this.FolderPath = info.FullName;
                this.MapName = info.Name;

                foreach (FileInfo fileInfo in info.GetFiles("*.json"))
                {
                    this.KeyActions.Add(KeyAction.CreateFromJSON(File.ReadAllText(fileInfo.FullName)));
                }
            }
        }
    }
}