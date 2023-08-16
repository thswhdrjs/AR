using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Azure.Kinect.Sensor;

public class KinectMgr : MonoBehaviour
{
    private Device kinect;
    private Texture2D kinectColorTexture;
    
    [SerializeField]
    private UnityEngine.UI.RawImage rawImg;

    // Start is called before the first frame update
    void Start()
    {
        InitKinect();
    }

    private void Update()
    {
        Capture capture = kinect.GetCapture();
        Image colorImg = capture.Color;
        Color32[] pixels = colorImg.GetPixels<Color32>().ToArray();

        kinectColorTexture.SetPixels32(pixels);
        kinectColorTexture.Apply();

        rawImg.texture = kinectColorTexture;
    }

    private void InitKinect()
    {
        kinect = Device.Open(0);

        kinect.StartCameras(new DeviceConfiguration
        {
            ColorFormat = ImageFormat.ColorBGRA32,
            ColorResolution = ColorResolution.R3072p,
            DepthMode = DepthMode.WFOV_Unbinned,
            SynchronizedImagesOnly = true,
            CameraFPS = FPS.FPS15
            //CameraFPS = FPS.FPS30
        });

        int width = kinect.GetCalibration().ColorCameraCalibration.ResolutionWidth;
        int height = kinect.GetCalibration().ColorCameraCalibration.ResolutionHeight;

        kinectColorTexture = new Texture2D(width, height);
    }

    private void OnDestroy()
    {
        kinect.StopCameras();
    }
}
