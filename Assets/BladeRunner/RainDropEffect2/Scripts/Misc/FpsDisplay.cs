using UnityEngine;
using System.Collections;

public class FpsDisplay : MonoBehaviour
{
    float interval = 0.2f;
    float startTime = 0f;
    float dt = 0f;
    int flameCnt = 0;
    int fps = 0;

    private void Awake()
    {
       Application.targetFrameRate = 60;
    }
    void LateUpdate()
    {
        dt = Time.time - startTime;
        flameCnt += 1;
        if (dt >= interval)
        {
            fps = (int)(flameCnt / dt);
            flameCnt = 0;
            startTime = Time.time;
        }
    }

    void OnGUI()
    {
        int w = Screen.width;
        int h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, h - h+5 / 15, w, h / 15);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h / 15;
        style.normal.textColor = Color.green;
        string text = string.Format("FPS:{0}", fps);
        GUI.Label(rect, text, style);
    }
}
