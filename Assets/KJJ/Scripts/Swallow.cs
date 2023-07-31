using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swallow : MonoBehaviour
{
    Vector3 dir;
    public float speed = 10f;
    bool a;
    GameObject salmon;
    bool b;

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
        if (a == true)
        {
            salmon.transform.position = Vector3.MoveTowards(salmon.transform.position, transform.position, 0.5f);
            if (Vector3.Distance(salmon.transform.position, transform.position) < 0.1f) b = true;
        }

        if(a == true && b == true)
        {
            Destroy(gameObject);
            Destroy(salmon.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Salmon(Clone)")
        {
            salmon = collision.gameObject;
            a = true;
        }
    }

    //IEnumerator Salmon(GameObject salmon)
    //{
    //    transform.position = Vector3.MoveTowards(salmon.transform.position, transform.position, 1);
    //    yield return 0;
    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.name == "Salmon(Clone)")
    //    {
    //        salmon = collision.gameObject;
    //        transform.position = Vector3.MoveTowards(salmon.transform.position, transform.position, 100);
    //    }
    //}
}