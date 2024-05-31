using UnityEngine.Rendering.Universal;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D fovLight; // FOV 라이트
    public float dayDuration = 120f; // 하루의 길이 (초 단위)
    public float minIntensity = 0.2f; // 밤의 최소 밝기
    public float maxIntensity = 1.0f; // 낮의 최대 밝기

    private float timeElapsed;

    void Update()
    {
        // 시간 경과 계산
        timeElapsed += Time.deltaTime;
        float normalizedTime = (timeElapsed % dayDuration) / dayDuration; // 0에서 1 사이의 값

        // 라이트 인텐시티 계산 (단순 사인 곡선을 사용하여 낮과 밤을 전환)
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Sin(normalizedTime * Mathf.PI * 2));
        globalLight.intensity = intensity;
        fovLight.intensity = intensity; // FOV 라이트 인텐시티도 동일하게 조절
    }
}
