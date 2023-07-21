using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public static Boss instance;
    public GameObject clearUI;
    public GameObject bgm;

    private void Awake()
    {
        instance = this;
    }
    // 보스 체력
    public float bossHP = 9;
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
    // Start is called before the first frame update
    void Start()
    {
        act = true;
        bossHP = 9;
    }

    // Update is called once per frame
    void Update()
    {
        dir = transform.position - center.transform.position;
        // 보스 체력이 일정 체력으로 낮아지면 패턴이 넘어간다.
        if (bossmoving == true)
        {
            if (bossHP == 6 || bossHP == 3)
            {
                BossMoveOn();
                bossmoving = false;
            }
        }
        else BossMoveOff();
        
        if (bossHP == 7 && act == true)
        {
            MakeBoss();
            bossmoving = true;
            BossAttack.instance.currentTime = 0;
        }
        else if (bossHP < 7)
        {
            BossAttack.instance.pattern1 = false;
            BossAttack.instance.pattern2 = true;
            if (bossHP == 4 && act == true)
            {
                MakeBoss();
                bossmoving = true;
                BossAttack.instance.currentTime = 0;
            }
            if (bossHP < 4)
            {
                BossAttack.instance.pattern2 = false;
                BossAttack.instance.pattern3 = true;
                if (bossHP == 1 && act == true)
                {
                    MakeBoss();
                    BossAttack.instance.currentTime = 0;
                }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("to"))
        {
            // 바라보는 방향을 중앙을보게
            transform.forward = -dir.normalized;
            Destroy(collision.gameObject);
            // 이동 스크립트 끄기
            BossMoveOff();
        }
    }

}
