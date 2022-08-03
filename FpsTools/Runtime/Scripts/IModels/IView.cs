using UnityEngine;

namespace PerformanceDemo.IModels
{
    public interface IView
    {
        public int Fps { get; set; }
        public void EffectCall();
        public void RenderInvoke();
        public void StopRender();
        public void StartEffect();
        public void StopEffect();
    }
}