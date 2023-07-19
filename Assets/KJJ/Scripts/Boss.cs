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
    public float bossHP = 6;
    public bool bossmoving = false;
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
    public float moveTiem;
    // Start is called before the first frame update
    void Start()
    {
        act = true;
        bossHP = 4;
    }

    // Update is called once per frame
    void Update()
    {
        moveTiem += Time.deltaTime;
        dir = transform.position - center.transform.position;
        if (bossHP == 3 && act == false)
        {
            // 공격 중지
            BossAttack.instance.pattern1 = false;
            BossAttack.instance.pattern2 = true;
        }
        else if (bossHP == 4 && act == true)
        {
            moveTiem = 0;
            MakeBoss();
            BossAttack.instance.currentTime = 0;
        }
        //if(bossHP == 1 && act == false)
        //{
        //    movePositionCount = 1;
        //    BossAttack.instance.pattern2 = false;
        //    BossAttack.instance.pattern3 = true;
        //}
        //else
        if (bossHP == 1 && act == true)
        {
            MakeBoss();
        }
        if (bossHP < 2)
        {
            BossAttack.instance.currentTime = 0;
        }
        if (bossHP == 3)
        {
            movePositionCount = 0;
            BossMoveOn();
            // 바라보는 방향을 중앙을보게
            if (moveTiem > 10 && moveTiem < 10.5f) transform.forward = -dir.normalized;
        }
        else BossMoveOff();
    }

    void MakeBoss()
    {
        bossFactory.transform.position = moonPosition.position;
        bossFactory.transform.forward = moonPosition.forward;
        Instantiate(bossFactory);
        act = false;
    }

    void BossMoveOn()
    {
        bossMove.GetComponent<BossMove>().enabled = true;
    }

    public void BossMoveOff()
    {
        bossMove.GetComponent<BossMove>().enabled = false;
    }

    public void ClearUI()
    {
        clearUI.SetActive(true);
    }

}
