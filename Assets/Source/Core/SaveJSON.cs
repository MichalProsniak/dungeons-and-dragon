using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Core
{
    public  class SaveJSON
    {
        public static string ConvertDataToJSON(JSONInfo data)
        { 
            string stringjson = JsonConvert.SerializeObject(data);
            return stringjson;
        }

        public static void SaveJSONToFile(string jsonstring)
        {
            DateTime now = DateTime.Now;
            System.IO.File.WriteAllText(Application.persistentDataPath + $"/{now}.json", jsonstring);
        }
    }
}
