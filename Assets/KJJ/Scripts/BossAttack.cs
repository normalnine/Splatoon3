using System.Collections;
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
    public GameObject handFactory;
    public GameObject fistFactory;
    public GameObject inkboomFactory;
    public GameObject swallowFactory;
    public GameObject waveFactory;
    public float currentTime;
    public float attackTime = 7;

    public bool pattern1 = false;
    public bool pattern2 = false;
    public bool pattern3 = false;

    public Transform LFirepos;
    public Transform RFirepos;
    public Transform Firepos;
    public bool didths;

    int prevChooseIndex = -1; // 인덱스값을 저장
    public GameObject[] spawnList;

    int rValue = 1;
    int lValue = 1;
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

        //if (HandC.instance.swallow == true)
        //{
        //    swallowTime += Time.deltaTime;
        //}
        //if (swallowTime > 0.4)
        //{
        //    GameObject swallow = Instantiate(swallowFactory);
        //    swallow.transform.position = Firepos.position;
        //    swallow.transform.forward = Firepos.forward;
        //}
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
        int rValue = Random.Range(0, 9); // 랜덤한 0~9까지의 수를 만든다.
        if (rValue < 5) LHand();
        else LFist();
    }
    // 1페이지 오른손
    void RFirePos1P()
    {
        int rValue = Random.Range(0, 9); // 랜덤한 0~9까지의 수를 만든다.
        if (rValue < 5) RHand();
        else RFist();
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
        if (lValue == 1)
        {
            swallow();
            lValue++;
        }
        else if (lValue == 2)
        {
            Inkboom();
            lValue++;
        }
        else if (lValue == 3)
        {
            LFist();
            lValue++;
            didths = true;
        }
        else if(lValue == 4)
        {
            LHand();
            lValue++;
            didths = true;
        }
    }
    // 2페이지 오른손
    void RFirePos2P()
    {
        if (rValue == 1)
        {
            RFist();
            rValue++;
            didths = false;
        }
        else if (rValue == 2)
        {
            RHand();
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
        if (lValue == 5)
        {
            Wave();
            lValue++;
            didths = true;
        }
        else if (lValue == 6)
        {
            int chooseIndex = Random.Range(0, spawnList.Length); //0부터 배열의 길이까지
                                                                 // 4.1 랜덤 인덱스가 직전 인덱스와 같다면 다시 정하고싶다.
            if (prevChooseIndex == chooseIndex)
            {
                // chooseIndex에 1을 더하고싶다.
                chooseIndex++;
                // 만약 chooseIndex가 배열의 범위를 벗어난다면 0으로 초기화하고싶다.
                if (chooseIndex >= spawnList.Length)
                {
                    chooseIndex = 0;
                }
            }
            // 직전인덱스에 현재 인덱스를 기억하고싶다.
            prevChooseIndex = chooseIndex;
            GameObject attack = (spawnList[chooseIndex]);
            attack.transform.position = LFirepos.position;
            attack.transform.forward = LFirepos.forward;
            currentTime = 0;
            didths = true;
        }
    }

    private void RFirePos3P()
    {
        if (rValue == 5)
        {
            Wave();
            rValue++;
            didths = true;
        }
        else if (rValue == 6)
        {
            int chooseIndex = Random.Range(0, spawnList.Length); //0부터 배열의 길이까지
                                                                 // 4.1 랜덤 인덱스가 직전 인덱스와 같다면 다시 정하고싶다.
            if (prevChooseIndex == chooseIndex)
            {
                // chooseIndex에 1을 더하고싶다.
                chooseIndex++;
                // 만약 chooseIndex가 배열의 범위를 벗어난다면 0으로 초기화하고싶다.
                if (chooseIndex >= spawnList.Length)
                {
                    chooseIndex = 0;
                }
            }
            // 직전인덱스에 현재 인덱스를 기억하고싶다.
            prevChooseIndex = chooseIndex;
            GameObject attack = (spawnList[chooseIndex]);
            attack.transform.position = LFirepos.position;
            attack.transform.forward = LFirepos.forward;
            currentTime = 0;
            didths = false;
        }
    }

    void LHand()
    {
        GameObject hand = Instantiate(handFactory);
        hand.transform.position = LFirepos.position;
        hand.transform.forward = LFirepos.forward;
        currentTime = 0;
    }

    void RHand()
    {
        GameObject hand = Instantiate(handFactory);
        hand.transform.position = RFirepos.position;
        hand.transform.forward = RFirepos.forward;
        currentTime = 0;
    }

    void LFist()
    {
        GameObject fist = Instantiate(fistFactory);
        fist.transform.position = LFirepos.position;
        fist.transform.forward = LFirepos.forward;
        currentTime = 0;
    }

    void RFist()
    {
        GameObject fist = Instantiate(fistFactory);
        fist.transform.position = RFirepos.position;
        fist.transform.forward = RFirepos.forward;
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

    void Wave()
    {
        GameObject wave = Instantiate(waveFactory);
        wave.transform.position = Firepos.position;
        wave.transform.forward = Firepos.forward;
        currentTime = 0;
    }
}
