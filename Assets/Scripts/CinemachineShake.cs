using UnityEngine;
using Unity.Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance;

    private CinemachineCamera cinemachineCam;
    private CinemachineBasicMultiChannelPerlin noise;

    [Header("Shake Ayarlarý")]
    [SerializeField] private float shakeDuration = 0.1f;
    [SerializeField] private float shakeAmplitude = 1f;
    [SerializeField] private float shakeFrequency = 10f;

    private float shakeTimer;
    private float currentAmplitude;

    void Awake()
    {
        Instance = this;
        cinemachineCam = GetComponent<CinemachineCamera>();
        noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            noise.AmplitudeGain = currentAmplitude;
            noise.FrequencyGain = shakeFrequency;
        }
        else
        {
            noise.AmplitudeGain = 0f;
            noise.FrequencyGain = 0f;
        }
    }

    public void Shake(float duration, float amplitude)
    {
        shakeTimer = duration;
        currentAmplitude = amplitude;
    }
}