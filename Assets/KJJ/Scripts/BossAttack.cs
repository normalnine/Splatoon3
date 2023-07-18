﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public static BossAttack instance;
    private void Awake()
    {
        instance = this;
    }
    public Transform playerTarget;
    public GameObject floor;
    public GameObject handFactory;
    public GameObject fistFactory;
    public GameObject inkboomFactory;
    public float currentTime;
    public float attackTime = 7;

    public bool pattern1 = false;
    public bool pattern2 = false;
    public bool pattern3 = false;

    public Transform LFirepos;
    public Transform RFirepos;
    public Transform Firepos;
    public bool didths;

    // Start is called before the first frame update
    void Start()
    {
        pattern1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (pattern1 == true) Page1();
        else if (pattern2 == true) Page2();
        //else if (pattern3 == true) Page3();
    }

    // 2페이지 공격패턴
    void Page1()
    {
        if (currentTime > attackTime)
        {
            if (didths == false)
            {
                LFirePos1P();
                didths = true;
            }
            else
            {
                RFirePos1P();
                didths = false;
            }
        }
    }

    void LFirePos1P()
    {
        int rValue = Random.Range(0, 9); // 랜덤한 0~9까지의 수를 만든다.
        if (rValue < 5)
        {
            GameObject hand = Instantiate(handFactory);
            hand.transform.position = LFirepos.position;
            currentTime = 0;
        }
        else
        {
            GameObject fist = Instantiate(fistFactory);
            fist.transform.position = LFirepos.position;
            currentTime = 0;
        }
    }

    void RFirePos1P()
    {
        int rValue = Random.Range(0, 9); // 랜덤한 0~9까지의 수를 만든다.
        if (rValue < 5)
        {
            GameObject hand = Instantiate(handFactory);
            hand.transform.position = RFirepos.position;
            currentTime = 0;
        }
        else
        {
            GameObject fist = Instantiate(fistFactory);
            fist.transform.position = RFirepos.position;
            currentTime = 0;
        }
    }

    // 3페이지 공격패턴
    void Page2()
    {
        if (currentTime > attackTime)
        {
            GameObject inkboom = Instantiate(inkboomFactory);
            inkboom.transform.position = Firepos.position;
            currentTime = 0;
            //if (didths == false)
            //{
            //    LFirePos2P();
            //}
            //else
            //{
            //    RFirePos2P();
            //}
        }
    }
    void LFirePos2P()
    {
        int rValue = Random.Range(0, 8); // 랜덤한 0~8까지의 수를 만든다.
        if (rValue < 3)
        {
            GameObject hand = Instantiate(handFactory);
            hand.transform.position = LFirepos.position;
            didths = true;
            currentTime = 0;
        }
        else if (2 < rValue && rValue < 6)
        {
            GameObject fist = Instantiate(fistFactory);
            fist.transform.position = LFirepos.position;
            didths = true;
            currentTime = 0;
        }
        else
        {
            GameObject inkboom = Instantiate(inkboomFactory);
            inkboom.transform.position = Firepos.position;
            currentTime = 0;
        }
    }

    void RFirePos2P()
    {
        int rValue = Random.Range(0, 8); // 랜덤한 0~8까지의 수를 만든다.
        if (rValue < 3)
        {
            GameObject hand = Instantiate(handFactory);
            hand.transform.position = RFirepos.position;
            didths = false;
            currentTime = 0;
        }
        else if (2 < rValue && rValue < 6)
        {
            GameObject fist = Instantiate(fistFactory);
            fist.transform.position = RFirepos.position;
            didths = false;
            currentTime = 0;
        }
        else
        {
            GameObject inkboom = Instantiate(inkboomFactory);
            inkboom.transform.position = Firepos.position;
            currentTime = 0;
        }
    }
}
