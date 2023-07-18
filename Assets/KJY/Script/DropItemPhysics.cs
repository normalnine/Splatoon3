using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.ProBuilder;

public class DropItemPhysics : MonoBehaviour
{
    Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position; //어디에서 쏠건지
        Vector3 velocity = transform.forward; // 어느 방향으로 힘을 줄건지
        pos += gravity * 0.5f * Time.deltaTime * Time.deltaTime + velocity * Time.deltaTime; // 자유 낙하 운동
        velocity += gravity * Time.deltaTime; // 중력힘이 계속 작용
    }
}
