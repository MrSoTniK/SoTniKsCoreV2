using System.IO;
using UnityEngine;

namespace Core.Tools 
{
    public static class JsonManager<TJsonData>
    {      
        public static TJsonData LoadData(string filePath)
        {
            string dataToLoad;
            using (FileStream stream = new(filePath, FileMode.Open))
            {
                using (StreamReader reader = new(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            TJsonData loadedData = JsonUtility.FromJson<TJsonData>(dataToLoad);

            return loadedData;
        }

        public static void SaveData(string filePath, TJsonData data) 
        {            
            string dataToStore = JsonUtility.ToJson(data);

            using(FileStream stream = new(filePath, FileMode.Create)) 
            {
                using (StreamWriter writer = new(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        }

        public static string EncryptDecrypt(string data, string key)
        {
            string result = "";

            for (int i = 0; i < data.Length; i++)
            {
                result += (char)(data[i] ^ key[i % key.Length]);
            }

            return result;
        }
    }
}