using UnityEngine;
using UnityEditor;

namespace PerformanceDemo.Demo2D.Inspector
{
    [CustomEditor(typeof(CircleMoveEffectView))]
    public class CircleMoveEffectViewInspector : Editor
    {
        private int fps;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CircleMoveEffectView wnd = target as CircleMoveEffectView;
            fps = wnd.Fps;

            fps = EditorGUILayout.IntField("Fps:", fps);
            wnd.Fps = fps;

            if (GUILayout.Button("Reset fps"))
            {
                wnd.CancelInvoke();
                wnd.InvokeRepeating("RenderCall", 0f, (1f / (float)fps));
            }
        }
    }
}