namespace PerformanceDemo.HDRP
{
    using UnityEngine;
    using IModels;
    using Models;

    public class HDRPEffectView : View, IView
    {
        public int Fps
        {
            get => fps;
            set => fps = value;
        }

        private float updateInterval = 1f;
        private float lastUpdateFpsTime = 0f;

        public void EffectCall() { }
        public void RenderInvoke() => RenderCall();
        public void StopRender() => Stop();

        private void Start()
        {
            //InvokeRepeating("RenderCall", 0f, (1f / (float)Fps));
            //EffectManager.Instance.AddView(this);
        }

        private void Update()
        {
            float current = Time.realtimeSinceStartup;
            if (lastUpdateFpsTime == 0)
            {
                lastUpdateFpsTime = Time.realtimeSinceStartup;
            }

            if (current > lastUpdateFpsTime + updateInterval)
            {
                float tempFps = (float)fpsCount / (current - lastUpdateFpsTime);
                fpsText.text = $"FPS Display{renderCamera.targetDisplay}: limited- {Fps}, real- {tempFps}";
                lastUpdateFpsTime = current;
                fpsCount = 0;
            }
        }
    }
}

