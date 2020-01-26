using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

public class Screenshot : MonoBehaviour
{

    public RawImage QR_code;
    private string path_img;
    public string img_name;
    public byte[] imageBytes;
    public Camera Camera;

    private string QRCodeString; // store the string of the QR Code

    string url = "http://52.231.157.102/Upload.php";

    // Start is called before the first frame update
    void Start() {
        // QR_code.enabled = false;
    }
    // public Camera mainCamera;

    Texture2D Capture_and_upload(Camera camera, Rect rect) {
        // Create a RenderTexture object
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, -1);
        // add the image of other camera
        camera.targetTexture = rt;
        camera.Render();
        // read image bytes
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0); // read pixel from RenderTexture.active 
        screenShot.Apply();
        // reset to continue showing the frames
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);

        // Save as a png picture
        imageBytes = screenShot.EncodeToPNG();
        img_name = DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".png";
        path_img = Application.dataPath + "/Screenshot.png";
        System.IO.File.WriteAllBytes(path_img, imageBytes);

        Debug.Log(string.Format("picture: {0}", path_img));
        StartCoroutine(Upload(img_name));

        return screenShot;
    }

    IEnumerator Upload(string filename) {
        byte[] bytes = imageBytes;
        WWWForm form = new WWWForm();
        form.AddField("folder", "images/");
        form.AddBinaryData("file", bytes, filename, "image/png");
        WWW w = new WWW(url, form);
        yield return w;
        if (w.isDone) {
            Debug.Log("upload done!");
            QRCodeString = "http://52.231.157.102/images/"+img_name;
            Debug.Log(QRCodeString);
            GenerateQRImage(QRCodeString, 256, 256);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("press the Q key");
            Capture_and_upload(Camera, new Rect(0, 0, Screen.width, Screen.height));
        }
    }

    void GenerateQRImage(string content, int width, int height) {
        // encode color32
        EncodingOptions options = null;
        BarcodeWriter writer = new BarcodeWriter();
        options = new EncodingOptions {
            Width = width,
            Height = height,
            Margin = 1,
        };
        options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
        writer.Format = BarcodeFormat.QR_CODE;
        writer.Options = options;
        Color32[] colors = writer.Write(content);

        // to texture2d
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels32(colors);
        texture.Apply();

        QR_code.texture = texture;

    }
}
