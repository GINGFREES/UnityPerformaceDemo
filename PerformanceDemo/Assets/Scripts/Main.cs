using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    public TMP_Text fpsShow;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        Debug.LogWarning($"[Lucian :] screen refresh rate : {Screen.currentResolution.refreshRate}");
        Debug.LogWarning($"[Lucian :] current frame rate : {Application.targetFrameRate}");
    }

    // Update is called once per frame
    void Update()
    {
        fpsShow.text = $"FPS: {1f / Time.deltaTime}";
    }
}
