namespace PerformanceDemo
{
    using UnityEngine;
    using IModels;
    using Demo2D;

    public class Main : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private int viewCount = 0;

        private void EffectCall()
        {
            if (Demo2DManager.Instance.viewCount < viewCount) return;
            Demo2DManager.Instance.EffectCallAll();
        }

        private void Start()
        {
            if (viewCount > 0)
            {
                InvokeRepeating("EffectCall", 0f, (1f / 360f));
            }
        }

        private void OnDestroy()
        {
            Demo2DManager.CleanUp();
        }
    }
}
