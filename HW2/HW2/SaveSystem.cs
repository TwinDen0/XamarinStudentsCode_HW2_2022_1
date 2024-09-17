using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace HW2
{
    struct JsonData
    {
        public List<Note> list;
    }

    static class SaveSystem
    {
        static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data");

        public static void Save(in List<Note> list) 
        {
            var obj = new JsonData { list = list };

            var jsonString = JsonConvert.SerializeObject(obj);

            File.WriteAllText(path, jsonString);
        }

        public static void Load(out List<Note> list)
        {
            list = new List<Note>();

            if (File.Exists(path))
            {
                var jsonString = File.ReadAllText(path);
                var obj = JsonConvert.DeserializeObject<JsonData>(jsonString);
                if(obj.list != null)
                {
                    list = obj.list;
                }
            }
        }
    }
}
