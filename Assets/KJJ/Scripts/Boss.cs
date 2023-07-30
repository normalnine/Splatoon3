using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public static Boss instance;
    public GameObject clearUI;

    private void Awake()
    {
        instance = this;
    }
    // 보스 체력
    public float bossHP = 9;
    // 문추냉이 소환위치
    public Transform moonPosition;
    // 문추냉이 소환공장
    public GameObject bossFactory;
    // 문추냉이 소환 on/off
    public bool act;
    public int movePositionCount = 0;

    public GameObject bossMove;
    public GameObject center;
    public Vector3 dir;
    public AudioSource audioSource;
    public AudioSource audioSource2;

    public bool idle;
    public bool hit;
    public bool moon;

    float currentTime;

    public GameObject hitEFactory;
    // Start is called before the first frame update
    void Start()
    {
        act = true;
        bossHP = 9;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        dir = transform.position - center.transform.position;
        if (bossHP == 6)
        {
            movePositionCount = 0;
            // 공격 중지
            BossAttack.instance.pattern1 = false;
            BossAttack.instance.pattern2 = true;
        }
        else if (bossHP == 7 && act == true)
        {
            MakeBoss();
            BossAttack.instance.currentTime = 0;
        }
        if (bossHP == 1 && act == true)
        {
            MakeBoss();
            BossAttack.instance.currentTime = 0;
        }

        if (bossHP == 3)
        {
            movePositionCount = 1;
            // 공격 중지
            BossAttack.instance.pattern2 = false;
            BossAttack.instance.pattern3 = true;
        }
        else if (bossHP == 4 && act == true)
        {
            MakeBoss();
            BossAttack.instance.currentTime = 0;
        }
        if(hit == true)
        {
            if(currentTime > 1)
            {
                idle = true;
                hit = false;
            }
        }
    }

    void MakeBoss()
    {
        bossFactory.transform.position = moonPosition.position;
        bossFactory.transform.forward = moonPosition.forward;
        Instantiate(bossFactory);
        act = false;
    }


    public void ClearUI()
    {
        clearUI.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("to"))
        {
            moon = false;
            idle = true;
            // 바라보는 방향을 중앙을보게
            transform.forward = -dir.normalized;
            Destroy(collision.gameObject);
            audioSource2.Play();
            BossAttack.instance.currentTime = 0;
        }
        if(collision.gameObject.CompareTag("Fist"))
        {
            audioSource.Play();
        }
        if (collision.gameObject.CompareTag("Hand"))
        {
            audioSource.Play();
        }
        if(collision.gameObject.CompareTag("Hand") || collision.gameObject.CompareTag("Fist"))
        {
            GameObject hiteffect = Instantiate(hitEFactory);
            hiteffect.transform.position =transform.position;
            idle = false;
            hit = true;
            currentTime = 0;
        }
        if (collision.gameObject.CompareTag("Moon"))
        {
            GameObject hiteffect = Instantiate(hitEFactory);
            hiteffect.transform.position = transform.position;
            idle = false;
            moon = true;
            currentTime = 0;
        }
    }
}
