using UnityEngine.Rendering.Universal;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D fovLight; // FOV ����Ʈ
    public float dayDuration = 120f; // �Ϸ��� ���� (�� ����)
    public float minIntensity = 0.2f; // ���� �ּ� ���
    public float maxIntensity = 1.0f; // ���� �ִ� ���

    private float timeElapsed;

    void Update()
    {
        // �ð� ��� ���
        timeElapsed += Time.deltaTime;
        float normalizedTime = (timeElapsed % dayDuration) / dayDuration; // 0���� 1 ������ ��

        // ����Ʈ ���ٽ�Ƽ ��� (�ܼ� ���� ��� ����Ͽ� ���� ���� ��ȯ)
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Sin(normalizedTime * Mathf.PI * 2));
        globalLight.intensity = intensity;
        fovLight.intensity = intensity; // FOV ����Ʈ ���ٽ�Ƽ�� �����ϰ� ����
    }
}
