using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // public Camera mainCamera;
    public Camera Camera;
    Texture2D CaptureCamera(Camera camera2, Rect rect) {
        // Create a RenderTexture object
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, -1);
        // Set the targetTexture parameter of camera as rt, 并手动渲染相关相机  

        //ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
        camera2.targetTexture = rt;
        camera2.Render();

        // 激活这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();

        // reset to continue showing the frames
        camera2.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors  
        GameObject.Destroy(rt);
        // Save as a png picture  
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("picture: {0}", filename));

        return screenShot;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("press the Q key");
            CaptureCamera(Camera, new Rect(0, 0, Screen.width, Screen.height));
        }
    }
}
