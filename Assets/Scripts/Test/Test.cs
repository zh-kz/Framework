using System;
using UnityEngine;

namespace Test
{
    public class Test : MonoBehaviour
    {
        [HideInInspector, NonSerialized]
        private Transform _transform;

        /// <summary>
        /// Gets the Transform attached to the object.
        /// </summary>
        public new Transform transform { get { return _transform ? _transform : (_transform = base.transform); } }

        private void Start()
        {
            Debug.Log("base:");
            ExecTimeTest(TestBase);
            Debug.Log("new:");
            ExecTimeTest(TestNew);
        }

        private void ExecTimeTest(Action act)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            act();
            sw.Stop();
            var ts = sw.Elapsed;
            Debug.Log("Time used: " + ts.TotalMilliseconds);
        }

        private void TestBase()
        {
            for(int i = 0; i < 1000; i++){
                for(int j = 0; j < 1000; j++){
                    base.transform.rotation = Quaternion.identity;
                }
            }
        }

        private void TestNew()
        {
            for(int i = 0; i < 1000; i++){
                for(int j = 0; j < 1000; j++){
                    transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}
