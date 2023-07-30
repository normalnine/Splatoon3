using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBoom1 : MonoBehaviour
{
    public static InkBoom1 instance;
    private void Awake()
    {
        instance = this;
    }

    public float speed = 15;
    Rigidbody rb;

    public float currentTime;
    public float boomTime = 2.4f;
    Vector3 dir; //방향을 담을 변수

    public Transform Projectile;

    public GameObject inkEffectFactory;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        dir = player.transform.position - transform.position;
        rb = GetComponent<Rigidbody>();
        rb.velocity = dir.normalized * speed;
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 바닥에 닿았다면
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentTime = 0;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            // 내 공격도 파괴하고싶다.
            Destroy(gameObject, 2.5f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Inkboom();
        }
    }

    void Inkboom()
    {
        //시간누적 2초
        if (currentTime > boomTime)
        {
            //터지는 이펙트
            GameObject inkEffect = Instantiate(inkEffectFactory);
            inkEffect.transform.position = transform.position;
            Destroy(inkEffect, 1f);
            // 반경 5M 안의 충돌체중에 적이있다면
            int layer = 1 << LayerMask.NameToLayer("Player");
            Collider[] cols = Physics.OverlapSphere(transform.position, 4, layer);
            for (int i = 0; i < cols.Length; i++)
            {
                print("플레이어 데미지");
                // 데미지를 주고싶다
                cols[i].GetComponent<PlayerHP>().PlayerStrongDamageProcess();
            }
        }
    }


}
