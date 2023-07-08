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
    public float bossHP = 4;
    public bool bossmoving = false;
    // Start is called before the first frame update
    void Start()
    {
        bossHP = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (1 < bossHP && bossHP < 3)
        {
            // 공격 중지
            BossAttack.instance.pattern1 = false;
            //bossmoving = true;
            //if (bossmoving == true)
            //{
               // BossAttack.instance.pattern2 = false;
                //gameObject.GetComponent<BossMove>().enabled = true;
            //}
            //if (bossmoving == false)
            //{
                //gameObject.GetComponent<BossMove>().enabled = false;
                BossAttack.instance.pattern2 = true;
            //}
        }
        else if (bossHP < 2)
        {
            BossAttack.instance.pattern2 = false;
            //bossmoving = true;
            //if (bossmoving == true)
            //{
            //    BossAttack.instance.pattern3 = false;
            //    gameObject.GetComponent<BossMove>().enabled = true;
            //}
            //if (bossmoving == false)
            //{
            //    gameObject.GetComponent<BossMove>().enabled = false;
            BossAttack.instance.pattern3 = true;
            //}
        }
    }
}
