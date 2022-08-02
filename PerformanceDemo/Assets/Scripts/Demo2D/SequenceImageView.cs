namespace PerformanceDemo.Demo2D
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.U2D;
    using IModels;
    using Models;

    public class SequenceImageView : View, IView
    {
        [SerializeField] private SpriteAtlas imageAtlas;
        [SerializeField] private Image picture;
        [SerializeField] private string prefix;

        public int Fps
        {
            get => fps;
            set => fps = value;
        }

        private int index = 0;
        private float updateInterval = 1f;
        private float lastUpdateFpsTime = 0f;

        private void Start()
        {
            EffectManager.Instance.AddView(this);
            InvokeRepeating("RenderCall", 0f, (1f / (float)Fps));
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

        public void EffectCall()
        {
            string pictureName = prefix + index.ToString();
            picture.sprite = EffectManager.Instance.GetSprite(imageAtlas, pictureName);
            ++index;
            if (index >= 360) index = 0;
        }
    }
}