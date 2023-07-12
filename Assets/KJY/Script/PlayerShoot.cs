﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;
    public LineRenderer lr;
    Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    public Transform muzzle;
    public int count;
    public GameObject SalmonFactory;
    public bool isShoot;
    GameObject salmon;

    float rotX;
    float rotY;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_CameraAndMove.instance.isZoom == true)
        {
            lr.enabled = true;
            DrawLine();
        }
        else
        {
            lr.enabled = false;
        }
        if (Player_CameraAndMove.instance.isZoom == false && isShoot == false)
        {
            Destroy(salmon);
        }
        if (Input.GetMouseButtonDown(1))
        {
            isShoot = false;
            if (salmon == null)
            {
                salmon = Instantiate(SalmonFactory);
            }
            salmon.transform.position = muzzle.transform.position;
        }
        else if (Input.GetMouseButton(1) == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                isShoot = true;
            }
            if (isShoot == false)
            {
                salmon.transform.position = muzzle.transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Destroy(salmon);
                isShoot = true;
                salmon = Instantiate(SalmonFactory);
                Rigidbody rb = salmon.GetComponent<Rigidbody>();
                salmon.transform.position = muzzle.transform.position;
                rb.AddForce(muzzle.transform.forward * 10f, ForceMode.Impulse);
            }
        }

        if (Player_CameraAndMove.instance.isZoom == true)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            rotX += y * 200 * Time.deltaTime;
            rotY += x * 200 * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, -90, 90);
            transform.eulerAngles = new Vector3(0, rotY, 0);
            muzzle.localEulerAngles = new Vector3(-rotX, 0, 0);

        }
    }

    void DrawLine()
    {
        lr.positionCount = count;
        Vector3 pos = muzzle.transform.position; //어디에서 쏠건지
        Vector3 velocity = muzzle.transform.forward * 10f; // 어느 방향으로 힘을 줄건지
        for (int i = 0; i < count; i++)
        {
            pos += gravity * 0.5f * Time.deltaTime * Time.deltaTime + velocity * Time.deltaTime; // 자유 낙하 운동
            velocity += gravity * Time.deltaTime; // 중력힘이 계속 작용
            lr.SetPosition(i, pos);
        }
    }
}
