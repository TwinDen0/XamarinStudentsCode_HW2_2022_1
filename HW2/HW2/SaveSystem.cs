using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;

namespace HW2
{
    struct JsonData
    {
        public ObservableCollection<Note> left;
        public ObservableCollection<Note> right;
    }

    static class SaveSystem
    {
        static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data");

        public static void Save(in ObservableCollection<Note> notes_left, in ObservableCollection<Note> notes_right) 
        {
            var obj = new JsonData { left = notes_left, right = notes_right };

            var jsonString = JsonConvert.SerializeObject(obj);

            File.WriteAllText(path, jsonString);
        }

        public static void Load(out ObservableCollection<Note> notes_left, out ObservableCollection<Note> notes_right)
        {
            notes_left = new ObservableCollection<Note>();
            notes_right = new ObservableCollection<Note>();

            if (File.Exists(path))
            {
                var jsonString = File.ReadAllText(path);
                var obj = JsonConvert.DeserializeObject<JsonData>(jsonString);

                notes_left = obj.left;
                notes_right = obj.right;
            }
        }
    }
}
