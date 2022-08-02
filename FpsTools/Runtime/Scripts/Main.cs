namespace PerformanceDemo
{
    using UnityEngine;
    using IModels;

    public class Main : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private int viewCount = 0;
        [SerializeField, Min(1f)] private float duration = 1f;

        private void EffectCall()
        {
            if (EffectManager.Instance.viewCount < viewCount) return;
            EffectManager.Instance.EffectCallAll();
        }

        private void Start()
        {
            if (viewCount > 0)
            {
                InvokeRepeating("EffectCall", 0f, (duration / 360f));
            }
        }

        private void OnDestroy()
        {
            EffectManager.CleanUp();
        }
    }
}
