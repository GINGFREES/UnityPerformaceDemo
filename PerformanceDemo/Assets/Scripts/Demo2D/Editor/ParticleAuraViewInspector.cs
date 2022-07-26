using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace PerformanceDemo.Demo2D.Inspector
{
    [CustomEditor(typeof(ParticleAuraView))]
    public class ParticleAuraViewInspector : Editor
    {
        private int fps;
        private int particleCount;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ParticleAuraView wnd = target as ParticleAuraView;
            if (fps == 0)
            {
                fps = Application.targetFrameRate;
            }
            fps = EditorGUILayout.IntField("Fps:", fps);
            particleCount = EditorGUILayout.IntField("Particle Count :", particleCount);

            wnd.Fps = fps;
            wnd.ParticleCount = particleCount;

            if (GUILayout.Button("Set Fps"))
            {
                wnd.SetFps();
            }

            if (GUILayout.Button("InstantiateAuraView"))
            {
                wnd.CreateAuraParticeObj();
            }
        }
    }
}
