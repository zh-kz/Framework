using UnityEngine;

namespace Framework.Character
{
    /// <summary>
    /// 控制选择圈显隐
    /// </summary>
    public class CharacterSelected : CachedBehaviour
    {
        [Tooltip("显示持续时间")]
        public float displayTime = 3;
        [Tooltip("选择圈名")]
        public string selectedName = "Selected";

        private GameObject selectedGO;
        private float hideTime;

        private void Start()
        {
            selectedGO = transform.FindChildByName(selectedName).gameObject;
        }

        private void Update()
        {
            if(hideTime < Time.time)
            {
                hideTime += Time.deltaTime;
            }
        }

        public void SetSelectedActive(bool state)
        {
            selectedGO.SetActive(state);
            enabled = state;
            if(state)
            {
                hideTime = Time.time + displayTime;
            }
        }
    }
}
