using UnityEngine;

namespace Framework.Util
{
    public class FPSCounter : MonoSingleton<FPSCounter>
    {
        public bool showFps = false;
        
        private int frameCount;
        private float timeCount;
        private int fps;

        private void Start()
        {
            frameCount = 0;
            timeCount = 0;
            fps = 0;
        }

        private void Update()
        {
            if(showFps)
            {
                frameCount++;
                timeCount += Time.deltaTime;
                if(timeCount > 0.5f)
                {
                    fps = (frameCount << 1);
                    timeCount = 0f;
                    frameCount = 0;
                }
            }
        }

        private void OnGUI()
        {
            if(showFps)
            {
                if(fps > 30)
                    GUI.color = Color.green;
                else if(fps > 15)
                    GUI.color = Color.yellow;
                else
                    GUI.color = Color.red;
                GUILayout.Label(fps.ToString());
            }
        }
    }
}
