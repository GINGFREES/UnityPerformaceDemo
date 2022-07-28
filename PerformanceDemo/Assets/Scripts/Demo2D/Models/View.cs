namespace PerformanceDemo.Demo2D.Models
{
    using UnityEngine;
    using TMPro;

    public abstract class View : MonoBehaviour
    {
        [SerializeField] protected int fps;
        [SerializeField] protected TMP_Text fpsText;
        [SerializeField] protected Camera renderCamera;

        protected int fpsCount;

        protected void RenderCall()
        {
            renderCamera.Render();
            fpsCount++;
        }
    }
}