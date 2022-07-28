namespace PerformanceDemo.Demo2D
{
    using UnityEngine;
    using Models;
    using IModels;

    public class CircleMoveEffectView : View, IView
    {
        [SerializeField] private GameObject effectObj;
        [SerializeField] private float circleRadius = 150f;
        [SerializeField, Range(1f, 360f)] private float angleAdd = 1f;

        private float updateInterval = 1f;
        private float lastUpdateFpsTime = 0f;
        private float angle = 0f;

        public int Fps
        {
            get => fps;
            set => fps = value;
        }

        public float AngleAdd
        {
            get => angleAdd;
            set => angleAdd = value;
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
                fpsText.text = $"FPS Display{renderCamera.targetDisplay}: limited- {Fps}, real- {tempFps}, angle = {angle}";
                lastUpdateFpsTime = current;
                fpsCount = 0;
            }
        }

        public void EffectCall()
        {
            angle += angleAdd;
            if (angle > 360f) angle = angleAdd;
            float cornerAngle = (2f * Mathf.PI / (360f / angleAdd)) * angle;
            effectObj.transform.localPosition =
                new Vector3(circleRadius * Mathf.Sin(cornerAngle), circleRadius * Mathf.Cos(cornerAngle));
        }

        private void Start()
        {
            effectObj.transform.localPosition = new Vector3(0f, circleRadius, 0f);
            InvokeRepeating("RenderCall", 0f, (1f / (float)Fps));
            Demo2DManager.Instance.AddView(this);
        }

    }
}