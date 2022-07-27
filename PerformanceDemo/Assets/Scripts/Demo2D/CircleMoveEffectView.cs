using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PerformanceDemo.Demo2D
{
    public class CircleMoveEffectView : MonoBehaviour
    {
        [SerializeField] private GameObject effectObj;
        [SerializeField, HideInInspector] private int fps;
        [SerializeField] private TMP_Text fpsText;
        [SerializeField] private Camera renderCamera;
        [SerializeField] private float circleRadius = 150f;
        [SerializeField, Range(1f, 360f)] private float angleAdd = 1f;

        private float updateInterval = 1f;
        private float lastUpdateFpsTime = 0f;
        //private float lastRenderTime = 0f;
        private int fpsCount = 0;
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

            if (current > lastUpdateFpsTime + (1f / ((float)Fps)))
            {
                angle += angleAdd;
                if (angle > 360f) angle = angleAdd;
                effectObj.transform.localPosition =
                    new Vector3(circleRadius * Mathf.Sin(angle), circleRadius * Mathf.Cos(angle));
                effectObj.transform.localEulerAngles = new Vector3(0f, 0f, 90f + angle);
            }

            if (current > lastUpdateFpsTime + updateInterval)
            {
                float tempFps = (float)fpsCount / (current - lastUpdateFpsTime);
                //Fps = Mathf.FloorToInt(tempFps);
                fpsText.text = $"FPS Display{renderCamera.targetDisplay}: limited- {Fps}, real- {tempFps}, angle = {angle}";
                lastUpdateFpsTime = current;
                fpsCount = 0;
            }
        }

        private void RenderCall()
        {
            renderCamera.Render();
            fpsCount++;

            //angle += angleAdd;
            //if (angle > 360f) angle = angleAdd;
            //effectObj.transform.localPosition =
            //    new Vector3(circleRadius * Mathf.Sin(angle), circleRadius * Mathf.Cos(angle));
        }

        private void Start()
        {
            effectObj.transform.localPosition = new Vector3(0f, circleRadius, 0f);
            InvokeRepeating("RenderCall", 0f, (1f / (float)Fps));
        }

    }
}