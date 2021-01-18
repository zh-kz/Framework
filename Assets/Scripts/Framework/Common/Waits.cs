using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// yield return WaitFor...
    /// </summary>
    public static class Waits
    {
        /// <summary>
        /// Wait scaled 0.1 seconds
        /// </summary>
        public static readonly WaitForSeconds wait0_1s;

        /// <summary>
        /// Wait scaled 0.5 seconds
        /// </summary>
        public static readonly WaitForSeconds wait0_5s;

        /// <summary>
        /// Wait scaled 1.0 seconds
        /// </summary>
        public static readonly WaitForSeconds wait1_0s;

        /// <summary>
        /// Wait scaled 5.0 seconds
        /// </summary>
        public static readonly WaitForSeconds wait5_0s;

        /// <summary>
        /// Wait for end of frame
        /// </summary>
        public static readonly WaitForEndOfFrame waitForEndOfFrame;

        /// <summary>
        /// Wait until FixedUpdate
        /// </summary>
        public static readonly WaitForFixedUpdate waitForFixedUpdate;

        private static Dictionary<float, WaitForSeconds> waitsTable;

        static Waits()
        {
            wait0_1s = new WaitForSeconds(0.1f);
            wait0_5s = new WaitForSeconds(0.5f);
            wait1_0s = new WaitForSeconds(1.0f);
            wait5_0s = new WaitForSeconds(5.0f);
            waitForEndOfFrame = new WaitForEndOfFrame();
            waitForFixedUpdate = new WaitForFixedUpdate();

            waitsTable = new Dictionary<float, WaitForSeconds>(5)
            {
                {0.1f, wait0_1s},
                {0.5f, wait0_5s},
                {1.0f, wait1_0s},
                {5.0f, wait5_0s}
            };
        }

        /// <summary>
        /// 返回缓存的WaitForSeconds(seconds)
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            WaitForSeconds wait;
            if(!waitsTable.TryGetValue(seconds, out wait))
            {
                waitsTable.Add(seconds, wait = new WaitForSeconds(seconds));
            }
            return wait;
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void ClearWaitForSeconds()
        {
            waitsTable.Clear();
        }
    }
}
