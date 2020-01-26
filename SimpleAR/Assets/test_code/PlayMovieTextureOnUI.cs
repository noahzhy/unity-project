using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.Azure.CognitiveServices.Vision.Face;


public class PlayMovieTextureOnUI : MonoBehaviour {
    public RawImage rawimage;
    WebCamTexture webcamTexture = null;
    Color32[] data;
    void Start() {
        //Save get the camera devices, in case you have more than 1 camera.
        WebCamDevice[] camDevices = WebCamTexture.devices;

        //Get the used camera name for the WebCamTexture initialization.
        string camName = camDevices[0].name;
        webcamTexture = new WebCamTexture(camName);

        //Render the image in the screen.
        rawimage.texture = webcamTexture;
        rawimage.material.mainTexture = webcamTexture;
        webcamTexture.Play();
        data = new Color32[webcamTexture.width * webcamTexture.height];
    }
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log(">>>TakeSnapshot()");
            webcamTexture.GetPixels32(data);
            // TakeSnapshot();
            // webcamTexture.Stop();
            StartCoroutine(getTexture2d());

        }



    }

    void TakeSnapshot() {
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(rawimage.texture.width, rawimage.texture.height, TextureFormat.ARGB32, false);

        //Save the image to the Texture2D
        texture.SetPixels32(data);
        texture.Apply();

        //Encode it as a PNG.
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/only_webcam.png", bytes);
    }

    IEnumerator getTexture2d() {
        yield return new WaitForEndOfFrame();
        Texture2D t = new Texture2D(Screen.width, Screen.height);
        //截取的区域
        t.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        // t.SetPixels32(data);
        t.Apply();

        //Encode it as a PNG.
        byte[] bytes = t.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/only_webcam.png", bytes);
    }
}


