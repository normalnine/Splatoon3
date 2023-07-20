using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swallow : MonoBehaviour
{
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 7);
    }

    // Update is called once per frame
    void Update()
    {
        // Player게임오브젝트를 찾아줘
        GameObject playertarget = GameObject.Find("Player");
        // 플레이어와의 방향을 구하고
        dir = transform.position - playertarget.transform.position;
        transform.up = dir;
        Paintable.instance.OnDisable();
    }
}