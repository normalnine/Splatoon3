using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTest : MonoBehaviour
{
    //private Camera mainCamera; // 메인 카메라
    public Transform cameraArm;
    public float height;
    public float velocity;
    float rotX;
    public float limit;
    void Start()
    {
        //mainCamera = Camera.main;
    }
        public float aimDistance = 10f; // 에임 이미지를 카메라로부터 멀리 떨어진 거리에 표시하려면 이 값을 조정해주세요.
        public Camera playerCamera; // 플레이어의 카메라를 참조하기 위한 변수
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Mouse Y");
        rotX += x;
        rotX = Mathf.Clamp(rotX, -limit, limit);
        transform.eulerAngles = new Vector3(-rotX, 0, 0);

    }
}
