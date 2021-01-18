using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

using ConfigDict = System.Collections.Generic.Dictionary<string, 
        System.Collections.Generic.Dictionary<string, string>>;

namespace Framework.AI.FSM
{
    public static class FSMConfigurationReader
    {
        /// <summary>
        /// 配置文件缓存
        /// </summary>
        /// <typeparam name="string">文件名</typeparam>
        /// <typeparam name="ConfigDict">配置字典</typeparam>
        /// <returns></returns>
        private static Dictionary<string, ConfigDict> configCache = new Dictionary<string, ConfigDict>();

        /// <summary>
        /// 反序列化得到配置文件
        /// </summary>
        /// <param name="filename">StreamingAssets下的filename</param>
        /// <returns>配置文件</returns>
        public static ConfigDict ReadCongfig(string filename)
        {
            if(configCache.ContainsKey(filename))
            {
                return configCache[filename];
            }
            string path = Path.Combine(Application.streamingAssetsPath, filename);
            using(StreamReader streamReader = File.OpenText(path))
            {
                string data = streamReader.ReadToEnd();
                ConfigDict config = JsonConvert.DeserializeObject(data, typeof(ConfigDict)) as ConfigDict;
                configCache.Add(filename, config);
                return config;
            }
        }

        public static void WriteConfig(string filename, ConfigDict map)
        {
            string path = Path.Combine(Application.streamingAssetsPath, filename);
            using(StreamWriter streamWriter = File.CreateText(path))
            {
                string data = JsonConvert.SerializeObject(map);
                streamWriter.Write(data);
            }
        }
    }
}
