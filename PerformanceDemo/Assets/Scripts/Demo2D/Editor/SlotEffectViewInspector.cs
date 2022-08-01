
namespace PerformanceDemo.Demo2D.Inspector
{
    using UnityEngine;
    using UnityEditor;
    [CustomEditor(typeof(SlotEffectView))]
    public class SlotEffectViewInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SlotEffectView wnd = target as SlotEffectView;

            if (GUILayout.Button("Start"))
            {
                wnd.StartSlotEffect();
            }

            if (GUILayout.Button("Stop"))
            {
                wnd.StopSlotEffect();
            }

            if (GUILayout.Button("StartAll"))
            {
                for (int i = 0; i < 4; i++)
                {
                    SlotEffectView v = Demo2DManager.Instance.GetViewByIndex(i) as SlotEffectView;
                    v.StartSlotEffect();
                }
            }

            if (GUILayout.Button("StopAll"))
            {
                for (int i = 0; i < 4; i++)
                {
                    SlotEffectView v = Demo2DManager.Instance.GetViewByIndex(i) as SlotEffectView;
                    v.StopSlotEffect();
                }
            }
        }
    }
}