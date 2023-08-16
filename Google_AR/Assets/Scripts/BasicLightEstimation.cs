using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// A component that can be used to access the most recently received basic light estimation information
/// for the physical environment as observed by an AR device.
/// </summary>
[RequireComponent(typeof(Light))]
public class BasicLightEstimation : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The ARCameraManager which will produce frame events containing light estimation information.")]
    ARCameraManager m_CameraManager;

    /// <summary>
    /// Get or set the <c>ARCameraManager</c>.
    /// </summary>
    public ARCameraManager cameraManager
    {
        get { return m_CameraManager; }
        set
        {
            print("a");
            if (m_CameraManager == value)
                return;

            if (m_CameraManager != null)
                m_CameraManager.frameReceived -= FrameChanged;

            m_CameraManager = value;

            if (m_CameraManager != null & enabled)
                m_CameraManager.frameReceived += FrameChanged;
        }
    }

    /// <summary>
    /// The estimated brightness of the physical environment, if available.
    /// </summary>
    public float? brightness { get; private set; }

    /// <summary>
    /// The estimated color temperature of the physical environment, if available.
    /// </summary>
    public float? colorTemperature { get; private set; }

    /// <summary>
    /// The estimated color correction value of the physical environment, if available.
    /// </summary>
    public Color? colorCorrection { get; private set; }

    public Vector3? mainDir { get; private set; }

    public Text[] text;

    void Awake()
    {
        m_Light = GetComponent<Light>();
    }

    void OnEnable()
    {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived += FrameChanged;
    }

    void OnDisable()
    {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived -= FrameChanged;
    }

    void FrameChanged(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
            m_Light.intensity = args.lightEstimation.averageBrightness.Value;

        if (args.lightEstimation.averageColorTemperature.HasValue)
            m_Light.colorTemperature = args.lightEstimation.averageColorTemperature.Value;

        if (args.lightEstimation.colorCorrection.HasValue)
            m_Light.color = args.lightEstimation.colorCorrection.Value;

        if (args.lightEstimation.mainLightDirection.HasValue)
            m_Light.transform.rotation = Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);

        if (args.lightEstimation.mainLightIntensityLumens.HasValue)
            m_Light.intensity = args.lightEstimation.mainLightIntensityLumens.Value;

        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
            RenderSettings.ambientProbe = args.lightEstimation.ambientSphericalHarmonics.Value;
        }

        text[1].text = m_Light.intensity.ToString();
        text[0].text = m_Light.color.ToString();
        text[2].text = m_Light.transform.eulerAngles.ToString();
    }

    Light m_Light;
}
