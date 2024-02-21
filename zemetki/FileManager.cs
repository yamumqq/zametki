using System;
using System.ComponentModel;
using System.IO;
using zemetki.Models;
using Newtonsoft.Json;

namespace zemetki
{
    static class FileManager
    {
        static public void SaveToFile(BindingList<NoteModel> list, string path)
        {
            try
            {
                string json = JsonConvert.SerializeObject(list);
                File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), path), json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public BindingList<NoteModel> ReadFromFile(string path)
        {
            try
            {
                string resultInfo = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), path));
                return JsonConvert.DeserializeObject<BindingList<NoteModel>>(resultInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BindingList<NoteModel>();
            }
        }
    }
}