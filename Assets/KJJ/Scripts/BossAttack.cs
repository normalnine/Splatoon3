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
    public GameObject handFactory;
    public GameObject fistFactory;
    public float currentTime;
    public float attackTime = 5;

    public bool pattern1 = false;
    public bool pattern2 = false;

    public Transform LFirepos;
    public Transform RFirepos;
    public bool didths;

    //Hand attack;
    // Start is called before the first frame update
    void Start()
    {
        pattern1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (pattern1 == true)
        {
            Page1();
        }
        else if (pattern2 == true)
        {
            print("2페이지 시작");
            Page2();
        }
    }

    void Page1()
    {
        if (currentTime > attackTime)
        {
            if (didths == false)
            {
                LFirePos();
            }
            else
            {
                RFirePos();
            }
        }
    }

    void Page2()
    {
        if (currentTime > attackTime)
        {
            int rValue = Random.Range(0, 9); // 랜덤한 0~9까지의 수를 만든다.
            if (rValue < 5)
            {
                GameObject hand = Instantiate(handFactory);
                hand.transform.position = transform.position;
                currentTime = 0;
            }
            else
            {
                GameObject fist = Instantiate(fistFactory);
                fist.transform.position = transform.position;
                currentTime = 0;
            }
        }
    }
    void LFirePos()
    {
        GameObject fist = Instantiate(fistFactory);
        fist.transform.position = LFirepos.position;
        currentTime = 0;
        didths = true;
    }

    void RFirePos()
    {
        GameObject fist = Instantiate(fistFactory);
        fist.transform.position = RFirepos.position;
        currentTime = 0;
        didths = false;
    }
}
