using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

namespace PerformanceDemo.Demo2D
{
    public class ParticleAuraView : MonoBehaviour
    {
        [SerializeField, HideInInspector] private int fps;
        [SerializeField] private Transform particleRoot;
        [SerializeField] private GameObject auraParticleObj;
        [SerializeField, HideInInspector] private int particleCout;

        private List<GameObject> particleList = new List<GameObject>();

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

        public void SetFps() => Application.targetFrameRate = Fps;

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

        private void Awake()
        {
            Fps = Application.targetFrameRate;
        }
    }
}
