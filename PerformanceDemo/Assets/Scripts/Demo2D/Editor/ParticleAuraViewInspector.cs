using UnityEngine;
using UnityEditor;


namespace PerformanceDemo.Demo2D.Inspector
{
    [CustomEditor(typeof(ParticleAuraView))]
    public class ParticleAuraViewInspector : Editor
    {
        private int fps;
        private int particleCount;
        private int particleSpeedRate;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ParticleAuraView wnd = target as ParticleAuraView;
            if (fps == 0)
            {
                fps = wnd.Fps;
            }

            fps = wnd.Fps;
            particleCount = wnd.ParticleCount;
            particleSpeedRate = wnd.ParticleSpeedRate;

            fps = EditorGUILayout.IntField("Fps:", fps);
            particleCount = EditorGUILayout.IntField("Particle Count :", particleCount);
            particleSpeedRate = EditorGUILayout.IntField("Particle Speed Rate :", particleSpeedRate);

            wnd.Fps = fps;
            wnd.ParticleCount = particleCount;
            wnd.ParticleSpeedRate = particleSpeedRate;

            if (GUILayout.Button("InstantiateAuraView"))
            {
                wnd.CreateAuraParticeObj();
            }
        }
    }
}
