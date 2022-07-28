using UnityEngine;

namespace PerformanceDemo.IModels
{
    public interface IView
    {
        public int Fps { get; set; }
        public void EffectCall();
    }
}