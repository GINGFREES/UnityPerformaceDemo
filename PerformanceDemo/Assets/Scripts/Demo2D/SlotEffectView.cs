namespace PerformanceDemo.Demo2D
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using IModels;
    using Models;

    public class SlotEffectView : View, IView
    {
        [SerializeField] private RectTransform[] imageRootList;
        [SerializeField] private float upperEdge;
        [SerializeField] private float downEdge;
        [SerializeField, Range(0f, 10f)] private float speed;


        private float lastUpdateFpsTime = 0f;
        private float updateInterval = 1f;

        private bool isReady = false;
        private bool isStop = false;
        private Lazy<float> lazyImageHeight;
        private Dictionary<int, int> nextIndexDics = new Dictionary<int, int>();
        private List<Vector3> indexPosList = new List<Vector3>();

        private Lazy<int> lazyImageCount = null;

        public int Fps
        {
            get => fps;
            set => fps = value;
        }

        private void UpdateMove()
        {
            float move = speed;

            for (int i = 0; i < imageRootList.Length; ++i)
            {
                imageRootList[i].localPosition -= new Vector3(0f, move, 0f);
            }

            int[] randomIndexes = SlotManager.Instance.GetRandomIndex(lazyImageCount.Value);

            for (int i = 0; i < imageRootList.Length; ++i)
            {
                bool needAddIndex = false;
                if (imageRootList[i].localPosition.y < downEdge)
                {
                    imageRootList[i].localPosition = new Vector3(0f, upperEdge + lazyImageHeight.Value - move, 0f);
                    SlotEffectImage effectImage = imageRootList[i].GetComponent<SlotEffectImage>();
                    effectImage.UpdateImage(randomIndexes);
                    needAddIndex = true;
                }

                float nextY = indexPosList[nextIndexDics[i]].y;

                if (imageRootList[i].localPosition.y < nextY || needAddIndex)
                {
                    nextIndexDics[i]++;
                    if (nextIndexDics[i] >= imageRootList.Length)
                    {
                        nextIndexDics[i] = 0;
                    }
                }
            }
        }

        private void StopToFinialPos()
        {
            for (int i = 0; i < imageRootList.Length; ++i)
            {
                imageRootList[i].localPosition -= new Vector3(0f, speed, 0f);
            }

            for (int i = 0; i < imageRootList.Length; ++i)
            {
                if (imageRootList[i].localPosition.y <= indexPosList[nextIndexDics[i]].y)
                {
                    for (int j = 0; j < imageRootList.Length; j++)
                    {
                        imageRootList[j].localPosition = indexPosList[nextIndexDics[j]];
                    }
                    isStop = false;
                    break;
                }
            }
        }

        private void StartSlotEffect()
        {
            isReady = true;
        }

        private void StopSlotEffect()
        {
            if (!isReady) return;
            isStop = true;
            isReady = false;
        }

        public void EffectCall()
        {
            if (isStop)
            {
                StopToFinialPos();
                return;
            }

            if (!isReady) return;
            UpdateMove();
        }

        public void RenderInvoke()
        {
            InvokeRepeating("RenderCall", 0f, (1f / (float)Fps));
        }

        public void StopRender() => Stop();

        public void StartEffect() => StartSlotEffect();

        public void StopEffect() => StopSlotEffect();

        private void Start()
        {
            lazyImageHeight = new Lazy<float>(() => imageRootList[0].rect.height);
            lazyImageCount = new Lazy<int>(() => imageRootList[0].GetComponent<SlotEffectImage>().ImagesCount);
            EffectManager.Instance.AddView(this);

            Vector3 pos = new Vector3(0f, 340f, 0f);
            for (int i = 0; i < imageRootList.Length; ++i)
            {
                if (i == imageRootList.Length - 1)
                {
                    nextIndexDics.Add(i, 0);
                }
                else
                {
                    nextIndexDics.Add(i, i + 1);
                }
            }

            int[] randomIndexes = SlotManager.Instance.GetRandomIndex(lazyImageCount.Value);

            for (int i = 0; i < imageRootList.Length; ++i)
            {
                indexPosList.Add(pos);
                SlotEffectImage effectImage = imageRootList[i].GetComponent<SlotEffectImage>();
                effectImage.UpdateImage(randomIndexes);
                pos -= new Vector3(0f, 170f, 0f);
            }

            //SlotManager.Instance.CleanUpUpdateTime();
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

        private void OnApplicationQuit()
        {
            SlotManager.CleanUp();
        }
    }
}