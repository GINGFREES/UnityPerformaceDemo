namespace PerformanceDemo.Demo2D
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class ParticleAuraView : MonoBehaviour
    {
        [SerializeField] private Camera renderCamera;
        [SerializeField] private TMP_Text fpsText;
        [SerializeField, HideInInspector] private int fps;
        [SerializeField] private Transform particleRoot;
        [SerializeField] private GameObject auraParticleObj;
        [SerializeField, HideInInspector] private int particleCout;

        private List<GameObject> particleList = new List<GameObject>();
        private int fpsCount = 0;
        private float updateInterval = 0.1f;
        private float lastUpdateFpsTime = 0;

        public int Fps
        {
            get => fps;
            set => fps = value;
        }

        public int ParticleCount
        {
            get => particleCout;
            set => particleCout = value;
        }

        public void CreateAuraParticeObj()
        {
            int max = particleCout > particleList.Count
                ? particleCout
                : particleList.Count;
            List<GameObject> newParticleList = new List<GameObject>();
            for (int i = 0; i < max; ++i)
            {
                if (i < particleList.Count && i < particleCout)
                {
                    newParticleList.Add(particleList[i]);
                    continue;
                }

                if (i < particleCout && i >= particleList.Count)
                {
                    GameObject target = Instantiate(auraParticleObj, particleRoot);
                    target.transform.localScale = Vector3.one;
                    newParticleList.Add(target);
                }

                if (i < particleList.Count && i >= particleCout)
                {
                    DestroyImmediate(particleList[i]);
                }
            }

            particleList.Clear();
            particleList = newParticleList;
            GC.Collect();
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
                //Fps = Mathf.FloorToInt(tempFps);
                fpsText.text = $"FPS Display{renderCamera.targetDisplay}: limited- {Fps}, real- {tempFps}";
                lastUpdateFpsTime = current;
                fpsCount = 0;
            }
        }

        private void RenderCall()
        {
            renderCamera.Render();
            fpsCount++;
        }

        private void Start()
        {
            InvokeRepeating("RenderCall", 0f, (1f / (float)Fps));
        }
    }
}
