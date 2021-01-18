using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 变换组件扩展
    /// </summary>
    public static class TransformHelper
    {
        /// <summary>
        /// 未知层级，查找后代指定名称的变换组件，扩展方法
        /// </summary>
        /// <param name="curTF">当前变换组件</param>
        /// <param name="childName">后代物体名称</param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform curTF, string childName)
        {
            Transform childTF = curTF.Find(childName);
            if(childTF != null)
            {
                return childTF;
            }
            for(int i = curTF.childCount - 1; i >= 0; i--)
            {
                childTF = FindChildByName(curTF.GetChild(i), childName);
                if(childTF != null)
                {
                    return childTF;
                }
            }
            return null;
        }
    }
}
