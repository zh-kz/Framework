using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Framework
{
    /// <summary>
    /// Resource管理器：通过名字加载资源（配置文件由GenerateResourceConfig生成）
    ///     事实上并不推荐用Resources文件夹，可以提前用ScriptableObject保存好各资源引用
    /// </summary>
    public static class ResourceManager
    {
        /// <summary>
        /// 配置文件相对于StreamingAssets的路径
        /// </summary>
        public static readonly string configFilePath = "Config/PrefabNameToPath.txt";

        /// <summary>
        /// 资源名 -> 路径名
        /// </summary>
        private static Dictionary<string, string> nameToPathTable;

        static ResourceManager()
        {
            nameToPathTable = new Dictionary<string, string>();
            LoadConfigToTable();
        }

        /// <summary>
        /// 将预制体配置文件加载到字典
        /// </summary>
        private static void LoadConfigToTable()
        {
            // StreamingAssets下资源路径配置文件路径
            string url = configFilePath;     

#region 分平台判断打包之后的StreamingAssets路径

#if UNITY_EDITOR || UNITY_STANDALONE
        url = "file://" + Application.dataPath + "/StreamingAssets/" + url;
#elif UNITY_IPHONE
        url = "file://" + Application.dataPath + "/Raw/" + url;
#elif UNITY_ANDROID
        url = "jar:file://" + Application.dataPath + "!/assets/" + url;
#endif
    
#endregion 分平台判断打包之后的StreamingAssets路径

            string tableText;
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SendWebRequest();
            while(true)
            {
                if(request.downloadHandler.isDone)
                {
                    tableText = request.downloadHandler.text;
                    break;
                }
            }

            using(StringReader reader = new StringReader(tableText))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    var keyValue = line.Split('=');
                    nameToPathTable.Add(keyValue[0], keyValue[1]);
                }
            }

            // TODO: 和FSM结合
        }

        /// <summary>
        /// 代替Resources.Load<T>()
        /// </summary>
        /// <param name="name">预制体名</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static T Load<T>(string name) where T : Object
        {
            string path;
            if(nameToPathTable.TryGetValue(name, out path))
            {
                return Resources.Load<T>(path);
            }
            Debug.LogError("Not have the prefab name: " + name);
            return null;
        }

        /// <summary>
        /// 代替Resources.Load()
        /// </summary>
        /// <param name="name">预制体名</param>
        /// <returns></returns>
        public static Object Load(string name)
        {
            string path;
            if(nameToPathTable.TryGetValue(name, out path))
            {
                return Resources.Load(path);
            }
            Debug.LogError("Not have the prefab name: " + name);
            return null;
        }

    }
}
