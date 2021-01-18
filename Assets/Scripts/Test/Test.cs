using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Test1
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace);
        }
    }
}
