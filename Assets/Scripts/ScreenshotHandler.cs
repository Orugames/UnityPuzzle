using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    static ScreenshotHandler instance;

    Camera myCamera;
    bool takeScreenShot;
    public bool ScreenShotComplete;
    public static int currentLevel;

    void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    private void OnPostRender()
    {
        if (takeScreenShot)
        {
            takeScreenShot = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);
            Debug.Log(currentLevel);
            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot" + currentLevel + ".png", byteArray);

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }
    void TakeScreenshot(int width, int height, int level)
    {
        currentLevel = level;
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 1);
        takeScreenShot = true;
    }
    public static void TakeScreenshot_Static(int width, int height, int level)
    {
        currentLevel = level;
        instance.TakeScreenshot(width, height, level);
    }
 
}
