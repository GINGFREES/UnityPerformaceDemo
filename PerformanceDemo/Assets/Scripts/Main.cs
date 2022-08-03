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
        private Lazy<Vector2> LazyLeftTopAnchor = new Lazy<Vector2>(() => new Vector2(0f, 1f));
        private Lazy<Vector2> LazyLeftBottomAnchor = new Lazy<Vector2>(() => new Vector2(0f, 0f));
        private Lazy<Vector2> LazyCenterTopAnchor = new Lazy<Vector2>(() => new Vector2(0.5f, 1f));
        private Lazy<Vector2> LazyCenterBottomAnchor = new Lazy<Vector2>(() => new Vector2(0.5f, 0f));

        private Action effectAction;

        private void EffectCall()
        {
            if (EffectManager.Instance.viewCount < viewCount) return;
            EffectManager.Instance.EffectCallAll();
        }

        private void EffectCallFps() => effectAction?.Invoke();

        private void Start()
        {
            //if (viewCount > 0)
            //{
            //    InvokeRepeating("EffectCall", 0f, (duration / 360f));
            //}

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

        private void SetBtnAnchorPosition(GameObject btn, Vector2 anchorMin)
        {
            RectTransform rect = btn.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMin;
            rect.pivot = anchorMin;
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
                fpsRects[i].gameObject.SetActive(true);
            }

            if (index != -1)
            {
                SetBtnAnchorPosition(btnStart.gameObject, LazyLeftTopAnchor.Value);
                SetBtnAnchorPosition(btnStop.gameObject, LazyLeftTopAnchor.Value);
                SetBtnAnchorPosition(btnClose.gameObject, LazyLeftTopAnchor.Value);

                SetBtnAnchorPosition(btnFpsAll.gameObject, LazyLeftBottomAnchor.Value);
                SetBtnAnchorPosition(btnFps30.gameObject, LazyLeftBottomAnchor.Value);
                SetBtnAnchorPosition(btnFps60.gameObject, LazyLeftBottomAnchor.Value);
                SetBtnAnchorPosition(btnFps90.gameObject, LazyLeftBottomAnchor.Value);
                SetBtnAnchorPosition(btnFps120.gameObject, LazyLeftBottomAnchor.Value);
            }
            else
            {
                SetBtnAnchorPosition(btnStart.gameObject, LazyCenterTopAnchor.Value);
                SetBtnAnchorPosition(btnStop.gameObject, LazyCenterTopAnchor.Value);
                SetBtnAnchorPosition(btnClose.gameObject, LazyCenterTopAnchor.Value);

                SetBtnAnchorPosition(btnFpsAll.gameObject, LazyCenterBottomAnchor.Value);
                SetBtnAnchorPosition(btnFps30.gameObject, LazyCenterBottomAnchor.Value);
                SetBtnAnchorPosition(btnFps60.gameObject, LazyCenterBottomAnchor.Value);
                SetBtnAnchorPosition(btnFps90.gameObject, LazyCenterBottomAnchor.Value);
                SetBtnAnchorPosition(btnFps120.gameObject, LazyCenterBottomAnchor.Value);
            }
        }

        private void OnBtnFpsAllClick()
        {
            CancelInvoke();
            if (EffectManager.Instance.viewCount <= 3) return;
            ResetRect();
            InvokeRepeating("EffectCall", 0f, (duration / 360f));
            StopAllRender();
            RenderInvoke();
        }

        private void OnBtnFps30Click()
        {
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
