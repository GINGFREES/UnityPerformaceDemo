using UnityEngine;
using UnityEditor;

namespace PerformanceDemo.HDRP.Inspectors
{
    [CustomEditor(typeof(HDRPEffectView))]
    public class HDRPEffectViewInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            HDRPEffectView wnd = target as HDRPEffectView;

            if (GUILayout.Button("Start"))
            {
                wnd.InvokeRepeating("RenderCall", 0f, (1f / (float)wnd.Fps));
            }

            if (GUILayout.Button("Stop"))
            {
                wnd.CancelInvoke();
            }
        }
    }
}
