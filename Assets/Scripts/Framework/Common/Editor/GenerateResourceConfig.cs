using System.IO;
using UnityEngine;
using UnityEditor;

namespace Framework
{
    /// <summary>
    /// 生成Resources文件夹下预制体文件资源路径
    ///     之后可由ResourceManager通过资源名加载
    /// </summary>
    public class GenerateResourceConfig : Editor
    {
        // 生成的配置文件
        private static readonly string configFile = "Assets/StreamingAssets/" + ResourceManager.configFilePath;

        /// <summary>
        /// 生成Resource下所有prefab的路径配置文件
        /// </summary>
        [MenuItem("Tools/Resources/Generate prefab resource config file")]
        public static void Generate()
        {
            // GUID
            string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[]{"Assets/Resources"});
            for(int i = resFiles.Length - 1; i >= 0; i--)
            {
                // Assets/Resources/Path/To/Target.prefab
                resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
                // Target
                string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
                // Path/To/Target
                string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
                // Target=Path/To/Target
                resFiles[i] = fileName + "=" + filePath;
            }
            File.WriteAllLines(configFile, resFiles);
            AssetDatabase.Refresh();
            
            Debug.Log("Generate resource config file ok");
        }
    }
}
