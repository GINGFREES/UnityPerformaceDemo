namespace PerformanceDemo
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using IModels;

    public class Main : MonoBehaviour
    {
        private enum RenderType : int
        {
            FPS_30 = 0,
            FPS_60,
            FPS_90,
            FPS_120
        }

        // Start is called before the first frame update
        [SerializeField] private int viewCount = 0;
        [SerializeField, Min(1f)] private float duration = 1f;
        [SerializeField] private RectTransform[] fpsRects;
        [SerializeField] private Button btnStart;
        [SerializeField] private Button btnStop;
        [SerializeField] private Button btnClose;
        [SerializeField] private Button btnFpsAll;
        [SerializeField] private Button btnFps30;
        [SerializeField] private Button btnFps60;
        [SerializeField] private Button btnFps90;
        [SerializeField] private Button btnFps120;

        private Lazy<Vector2> LazySmallSize => new Lazy<Vector2>(() => new Vector2(480f, 270f));
        private Lazy<Vector2> LazyFullSize => new Lazy<Vector2>(() => new Vector2(960f, 540f));

        private Action effectAction;

        private void EffectCall()
        {
            if (EffectManager.Instance.viewCount < viewCount) return;
            EffectManager.Instance.EffectCallAll();
        }

        private void EffectCallFps() => effectAction?.Invoke();

        private void Start()
        {
            Application.targetFrameRate = 120;
            Debug.LogWarning($"[Lucian :] current frame rate : {Application.targetFrameRate}");
            btnStart.onClick.AddListener(OnBtnStartClick);
            btnStop.onClick.AddListener(OnBtnStopClick);
            btnClose.onClick.AddListener(OnBtnCloseClick);
            btnFpsAll.onClick.AddListener(OnBtnFpsAllClick);
            btnFps30.onClick.AddListener(OnBtnFps30Click);
            btnFps60.onClick.AddListener(OnBtnFps60Click);
            btnFps90.onClick.AddListener(OnBtnFps90Click);
            btnFps120.onClick.AddListener(OnBtnFps120Click);
        }

        private void OnBtnStartClick()
        {
            for (int i = 0; i < EffectManager.Instance.viewCount; i++)
            {
                IView view = EffectManager.Instance.GetViewByIndex(i);
                view.StartEffect();
            }
        }

        private void OnBtnStopClick()
        {
            for (int i = 0; i < EffectManager.Instance.viewCount; i++)
            {
                IView view = EffectManager.Instance.GetViewByIndex(i);
                view.StopEffect();
            }
        }

        private void OnBtnCloseClick()
        {
            CancelInvoke();
            StopAllRender();
        }

        private void StopAllRender()
        {
            for (int i = 0; i < EffectManager.Instance.viewCount; i++)
            {
                IView view = EffectManager.Instance.GetViewByIndex(i);
                view.StopRender();
            }
        }

        private void RenderInvoke(int index = -1)
        {
            for (int i = 0; i < EffectManager.Instance.viewCount; i++)
            {
                if (index != -1 && i != index) continue;
                IView view = EffectManager.Instance.GetViewByIndex(i);
                view.RenderInvoke();
            }
        }

        private void SetBtnAnchorPosition(GameObject btn, Vector2 anchor)
        {
            RectTransform rect = btn.GetComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.pivot = anchor;
            Vector3 pos = rect.anchoredPosition;
            pos.x = 0f;
            rect.anchoredPosition = pos;
        }

        private void ResetRect(int index = -1)
        {
            for (int i = 0; i < EffectManager.Instance.viewCount; i++)
            {
                fpsRects[i].gameObject.SetActive(false);
                MonoBehaviour mono = fpsRects[i].GetComponent<MonoBehaviour>();
                mono.CancelInvoke();
                if (index != -1 && index != i) continue;
                fpsRects[i].sizeDelta = (index == -1)
                    ? LazySmallSize.Value
                    : LazyFullSize.Value;

                if (index == -1)
                {
                    if (i == 0) fpsRects[i].pivot = new Vector2(1f, 0f);
                    else if (i == 1) fpsRects[i].pivot = Vector2.one;
                    else if (i == 2) fpsRects[i].pivot = Vector2.zero;
                    else if (i == 3) fpsRects[i].pivot = new Vector2(0f, 1f);

                }
                else
                {
                    fpsRects[i].pivot = new Vector2(0.5f, 0.5f);
                }

                fpsRects[i].localPosition = Vector2.zero;
                fpsRects[i].gameObject.SetActive(true);
            }

            if (index == -1)
            {
                SetBtnAnchorPosition(btnStart.gameObject, new Vector2(0.5f, 1f));
                SetBtnAnchorPosition(btnStop.gameObject, new Vector2(0.5f, 1f));
                SetBtnAnchorPosition(btnClose.gameObject, new Vector2(0.5f, 1f));

                SetBtnAnchorPosition(btnFpsAll.gameObject, new Vector2(0.5f, 0f));
                SetBtnAnchorPosition(btnFps30.gameObject, new Vector2(0.5f, 0f));
                SetBtnAnchorPosition(btnFps60.gameObject, new Vector2(0.5f, 0f));
                SetBtnAnchorPosition(btnFps90.gameObject, new Vector2(0.5f, 0f));
                SetBtnAnchorPosition(btnFps120.gameObject, new Vector2(0.5f, 0f));
            }
            else
            {
                SetBtnAnchorPosition(btnStart.gameObject, new Vector2(0f, 1f));
                SetBtnAnchorPosition(btnStop.gameObject, new Vector2(0f, 1f));
                SetBtnAnchorPosition(btnClose.gameObject, new Vector2(0f, 1f));

                SetBtnAnchorPosition(btnFpsAll.gameObject, Vector2.zero);
                SetBtnAnchorPosition(btnFps30.gameObject, Vector2.zero);
                SetBtnAnchorPosition(btnFps60.gameObject, Vector2.zero);
                SetBtnAnchorPosition(btnFps90.gameObject, Vector2.zero);
                SetBtnAnchorPosition(btnFps120.gameObject, Vector2.zero);
            }
        }

        private void OnBtnFpsAllClick()
        {
            Application.targetFrameRate = 120;
            CancelInvoke();
            if (EffectManager.Instance.viewCount <= 3) return;
            ResetRect();
            InvokeRepeating("EffectCall", 0f, (duration / 360f));
            StopAllRender();
            RenderInvoke();
        }

        private void OnBtnFps30Click()
        {
            Application.targetFrameRate = 30;
            CancelInvoke();
            if (EffectManager.Instance.viewCount <= 0) return;
            int index = (int)RenderType.FPS_30;
            IView view = EffectManager.Instance.GetViewByIndex(index);
            ResetRect(index);
            effectAction = view.EffectCall;
            InvokeRepeating("EffectCallFps", 0f, (duration / 360f));
            StopAllRender();
            RenderInvoke(index);
        }

        private void OnBtnFps60Click()
        {
            Application.targetFrameRate = 60;
            CancelInvoke();
            if (EffectManager.Instance.viewCount <= 1) return;
            int index = (int)RenderType.FPS_60;
            IView view = EffectManager.Instance.GetViewByIndex(index);
            ResetRect(index);
            effectAction = view.EffectCall;
            InvokeRepeating("EffectCallFps", 0f, (duration / 360f));
            StopAllRender();
            RenderInvoke(index);
        }

        private void OnBtnFps90Click()
        {
            Application.targetFrameRate = 90;
            CancelInvoke();
            if (EffectManager.Instance.viewCount <= 2) return;
            int index = (int)RenderType.FPS_90;
            IView view = EffectManager.Instance.GetViewByIndex(index);
            ResetRect(index);
            effectAction = view.EffectCall;
            InvokeRepeating("EffectCallFps", 0f, (duration / 360f));
            StopAllRender();
            RenderInvoke(index);
        }

        private void OnBtnFps120Click()
        {
            Application.targetFrameRate = 30;
            CancelInvoke();
            if (EffectManager.Instance.viewCount <= 3) return;
            int index = (int)RenderType.FPS_120;
            IView view = EffectManager.Instance.GetViewByIndex(index);
            ResetRect(index);
            effectAction = view.EffectCall;
            InvokeRepeating("EffectCallFps", 0f, (duration / 360f));
            StopAllRender();
            RenderInvoke(index);
        }

        private void OnApplicationQuit()
        {
            EffectManager.CleanUp();
        }
    }
}
