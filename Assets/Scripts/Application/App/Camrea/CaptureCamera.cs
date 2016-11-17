using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaptureCamera
{
    /// <summary>
    /// 截屏，能看到的都会截屏下来，即整个显示
    /// </summary>
    public static void Capture()
    {
        string fileName = GetScreenShotName();
        Application.CaptureScreenshot(fileName, 0);
        Debug.Log(string.Format("截屏了一张照片: {0}", fileName));
    }

    /// <summary>
    /// 截屏，截取指定摄像机局域大小的显示
    /// </summary>
    public static Texture2D Capture(Camera camera, Rect rect)
    {
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
        camera.targetTexture = rt;
        camera.Render();

        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        camera.targetTexture = null;
        RenderTexture.active = null;
        GameObject.Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();

        string fileName = GetScreenShotName();
        System.IO.File.WriteAllBytes(fileName, bytes);
        Debug.Log(string.Format("截屏了一张照片: {0}", fileName));

        return screenShot;
    }

    /// <summary>
    /// 路径暂时写死，需调整请修改
    /// </summary>
    private static string GetScreenShotName()
    {
        string path = Application.dataPath + "/Screenshot/";
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        return path + string.Format("{0}.png", Util.GetTime());
    }


}
