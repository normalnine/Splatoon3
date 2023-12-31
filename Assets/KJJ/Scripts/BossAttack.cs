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
    public Transform playerTerget;
    public GameObject floor;
    public GameObject lhandFactory;
    public GameObject rhandFactory;
    public GameObject lfistFactory;
    public GameObject rfistFactory;
    public GameObject inkboomFactory;
    public GameObject swallowFactory;
    public float currentTime;
    public float attackTime = 7;

    public AudioSource audioSource3;

    public bool pattern1 = false;
    public bool pattern2 = false;
    public bool pattern3 = false;

    public Transform LFirepos;
    public Transform RFirepos;
    public Transform Firepos;
    public bool didths;


    public int rValue = 1;
    public int lValue = 1;
    public int r2Value = 1;
    public int l2Value = 1;

    public float swallowTime;
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
        if (pattern2 == true) Page2();
        if (pattern3 == true) Page3();

    }
    // 1페이지 공격패턴
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
    // 1페이지 왼손
    void LFirePos1P()
    {
        LFist();
    }
    // 1페이지 오른손
    void RFirePos1P()
    {
       RFist();
    }

    // 2페이지 공격패턴
    void Page2()
    {
        if (currentTime > attackTime)
        {
            if (didths == false) LFirePos2P();
            else RFirePos2P();
        }
    }
    // 2페이지 왼손
    void LFirePos2P()
    {
        if (lValue%2 == 1)
        {
            LFist();
            lValue++;
            didths = true;
        }
        else if (lValue%2 == 0)
        {
            LHand();
            lValue++;
            didths = true;
        }
    }
    // 2페이지 오른손
    void RFirePos2P()
    {
        if (rValue%2 == 1)
        {
            RFist();
            rValue++;
            didths = false;
        }
        else if (rValue%2 == 0)
        {
            RHand();
            rValue++;
            didths = false;
        }
    }

    // 3페이지 공격패턴
    void Page3()
    {
        if (currentTime > attackTime)
        {
            if (didths == false) LFirePos3P();
            else RFirePos3P();
        }
    }

    private void LFirePos3P()
    {
        if (l2Value%4 == 1)
        {
            swallow();
            l2Value++;
        }
        else if(l2Value%4 == 2)
        {
            Inkboom();
            l2Value++;
        }
        else if (l2Value%4 == 3)
        {
            LFist();
            l2Value++;
            didths = true;
        }
        else if (l2Value % 4 == 0)
        {
            LHand();
            l2Value++;
            didths = true;
        }
    }

    private void RFirePos3P()
    {
        if (r2Value%4 == 1)
        {
            swallow();
            r2Value++;
        }
        else if (r2Value%4 == 2)
        {
            Inkboom();
            r2Value++;
        }
        else if (r2Value%4 == 3)
        {
            RFist();
            r2Value++;
            didths = false;
        }
        else if (r2Value % 4 == 0)
        {
            RHand();
            r2Value++;
            didths = false;
        }
    }

    void LHand()
    {
        GameObject hand = Instantiate(lhandFactory);
        hand.transform.position = LFirepos.position;
        hand.transform.forward = LFirepos.forward;
        audioSource3.Play();
        currentTime = 0;
    }

    void RHand()
    {
        GameObject hand = Instantiate(rhandFactory);
        hand.transform.position = RFirepos.position;
        hand.transform.forward = RFirepos.forward;
        audioSource3.Play();
        currentTime = 0;
    }

    void LFist()
    {
        GameObject fist = Instantiate(lfistFactory);
        fist.transform.position = LFirepos.position;
        fist.transform.forward = LFirepos.forward;
        audioSource3.Play();
        currentTime = 0;
    }

    void RFist()
    {
        GameObject fist = Instantiate(rfistFactory);
        fist.transform.position = RFirepos.position;
        fist.transform.forward = RFirepos.forward;
        audioSource3.Play();
        currentTime = 0;
    }

    void Inkboom()
    {
        GameObject inkboom = Instantiate(inkboomFactory);
        inkboom.transform.position = Firepos.position;
        inkboom.transform.forward = Firepos.forward;
        currentTime = 0;
    }
    void swallow()
    {
        GameObject swallow = Instantiate(swallowFactory);
        swallow.transform.position = Firepos.position;
        swallow.transform.forward = Firepos.forward;
        currentTime = 0;
    }
}
