﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    static public Moon instance;

    private void Awake()
    {
        instance = this;
    }


    Rigidbody rb;
    public float speed = 5;

    public float bossMoonHP = 8;
    bool bossDie;
    public GameObject grenade;

    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        bossMoonHP = 100;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        rb.AddTorque(Vector3.forward * 0.25f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossDie == true)
        {
            // boss게임오브젝트를 찾아줘
            GameObject boss = GameObject.Find("Boss(C)");
            // 방향을 구한다
            dir = boss.transform.position - transform.position;
            // 날아간다
            rb.velocity = dir.normalized * speed * 2.5f;
            transform.forward = -rb.velocity.normalized;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.velocity = Vector3.zero;
        }

        if (collision.gameObject.tag == "Boss")
        {
            bossDie = false;
            gameObject.SetActive(false);
            Destroy(gameObject);
            Boss.instance.act = true;
            Boss.instance.bossHP--;
            if (Boss.instance.bossHP > 4) BossMove.instance.a = true;
            if (Boss.instance.bossHP < 5) BossMove.instance.b = true;
            BossAttack.instance.currentTime = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (bossMoonHP < 1 && Boss.instance.bossHP > 1)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = -transform.forward * speed * 2f;
            bossDie = true;
        }
        else if (bossMoonHP < 1 && Boss.instance.bossHP < 2)
        {
            GameObject grenadeBoom = Instantiate(grenade);
            grenadeBoom.transform.position = transform.position;
            StartCoroutine(ClearUI(1f));
            Destroy(gameObject, 2f);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            BossAttack.instance.currentTime = 0;
        }
    }

    IEnumerator ClearUI(float time)
    {
        yield return new WaitForSeconds(time);
        Boss.instance.ClearUI();
    }
}
