using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public static Boss instance;
    private void Awake()
    {
        instance = this;
    }
    // 보스 체력
    public float bossHP =6;
    public bool bossmoving = false;
    // 문추냉이 소환위치
    public Transform moonPosition;
    // 문추냉이 소환공장
    public GameObject bossFactory;
    // 문추냉이 소환 on/off
    public bool act;
    // Start is called before the first frame update
    void Start()
    {
        act = true;
        bossHP = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHP == 4 && act == false)
        {
            // 공격 중지
            BossAttack.instance.pattern1 = false;
            BossAttack.instance.pattern2 = true;
        }
        else if(bossHP == 4 && act == true)
        {
            MakeBoss();
            BossAttack.instance.currentTime = 0;
        }
        if(bossHP == 3 && act == false)
        {
            BossAttack.instance.pattern2 = false;
            BossAttack.instance.pattern3 = true;
        }
        else if (bossHP == 1 && act == true)
        {
            MakeBoss();
        }
        if(bossHP < 2)
        {
            BossAttack.instance.currentTime = 0;
        }
    }

    void MakeBoss()
    {
        Instantiate(bossFactory);
        bossFactory.transform.position = moonPosition.position;
        bossFactory.transform.up = moonPosition.up;
        act = false;
    }
}
