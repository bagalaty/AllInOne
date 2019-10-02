using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Core
{
    public class Utilities
    {
        public static Config_Section GetConfig_Section()
        {
            return new Config_Section { TraceRequests = "true" };
            /*
             * var ser = new XmlSerializer(typeof(Config_Section));
             * var fileText = System.IO.File.ReadAllText("wwwroot/data/Config.json");
             * var result = JsonConvert.DeserializeObject<Config_Section>(fileText);
             * return result;
             */
        }
    }
}