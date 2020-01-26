using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;

public class Run_demo : MonoBehaviour
{
    public GameObject personImage;
    public GameObject actor;
    public Camera Camera;
    public RawImage QR_code;

    string[] actors = new string[5] { "actor1", "actor2", "actor1", "actor2", "actor1" };
    string path_actor = "";

    string path_img;
    string img_name;
    byte[] imageBytes;
    string QRCodeString; // store the string of the QR Code
    string url = "http://52.231.157.102/Upload.php";

    // Start is called before the first frame update
    void Start() {
        // default actor is 'actor2'
        path_actor = "actors/" + actors[1];
        actor.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(path_actor, typeof(Sprite)) as Sprite;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log(">>>> space pressed");
            loadPersonFromLocation();
            loadActor(0);
            Capture_and_upload(Camera, new Rect(0, 0, Screen.width, Screen.height));
        }
    }

    void loadPersonFromLocation() {
        // personImage.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("002.png", typeof(Sprite)) as Sprite;
        personImage.gameObject.GetComponent<SpriteRenderer>().sprite = PngToSprite("Assets/002.png", 500, 500);
    }

    void loadActor(int i) {
        path_actor = "actors/" + actors[i];
        actor.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(path_actor, typeof(Sprite)) as Sprite;
    }



    // Start is called before the first frame update
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
            QRCodeString = "http://52.231.157.102/images/" + img_name;
            Debug.Log(QRCodeString);
            GenerateQRImage(QRCodeString, 256, 256);
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


    Sprite PngToSprite(string fullPath, int x, int y) {
        using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
        {
            fs.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            Texture2D texture = new Texture2D(x, y);
            texture.LoadImage(bytes);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
        }
    }
}