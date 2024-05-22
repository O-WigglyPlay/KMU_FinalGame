using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상, 여기서는 플레이어
    public float smoothSpeed = 0.125f; // 카메라 이동을 부드럽게 만들기 위한 스무딩 값
    public Vector3 offset; // 카메라와 대상 사이의 거리를 조절하기 위한 오프셋

    void FixedUpdate()
    {
        // 플레이어의 현재 위치에 오프셋을 더하여 카메라의 목표 위치를 설정
        Vector3 desiredPosition = target.position + offset;
        // Z 축을 움직이지 않도록 하기 위해 desiredPosition의 Z 값을 현재 카메라의 Z 값으로 설정
        desiredPosition.z = transform.position.z;
        // 부드럽게 이동하기 위해 현재 카메라 위치와 목표 위치 사이를 보간
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // 보간된 위치를 카메라의 새로운 위치로 설정
        transform.position = smoothedPosition;
    }
}
