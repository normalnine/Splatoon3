using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimPosition : MonoBehaviour
{
    public Transform Aim;
    public float dist;
    public float height;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //Vector3 tmp = transform.localPosition;
    //    //tmp = (Camera.main.transform.up * height + Camera.main.transform.forward * dist);
    //    //Aim.localPosition = new Vector3(0, tmp.y, dist);
    //    //Aim.localPosition = (Camera.main.transform.up * height + Camera.main.transform.forward * dist);
    //    //Aim.localPosition = (Camera.main.transform.up * height + Camera.main.transform.forward * dist);

    //}
    public Camera playerCamera; // 플레이어의 카메라를 참조하기 위한 변수
    public float aimDistance = 10f; // 에임 이미지를 카메라로부터 멀리 떨어진 거리에 표시하려면 이 값을 조정해주세요.

    private void FixedUpdate()
    {
        // 에임 이미지를 카메라 각도에 따라 회전시킵니다.
        // 에임 이미지는 카메라를 향해 바라보도록 설정합니다.
        //transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
                        //playerCamera.transform.rotation * Vector3.up);
        transform.LookAt(playerCamera.transform.position);
        // 에임 이미지를 플레이어에서 aimDistance만큼 떨어진 위치에 표시합니다.
        //transform.position = playerCamera.transform.position + playerCamera.transform.rotation * Vector3.forward * aimDistance * Time.deltaTime + Vector3.up *height * Time.deltaTime;
    }
}
