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
            BossAttack.instance.pattern1 = false;
            BossAttack.instance.pattern2 = true;
        }
        else if(bossHP < 2)
        {
            BossAttack.instance.pattern2 = false;
            BossAttack.instance.pattern3 = true;
            return;
        }
    }
}
