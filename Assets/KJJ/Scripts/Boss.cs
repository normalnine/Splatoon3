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
    public float bossHP = 2;

    // Start is called before the first frame update
    void Start()
    {
        bossHP = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHP < 1)
        {
            BossAttack.instance.pattern1 = false;
            BossAttack.instance.pattern2 = true;
            return;
        }
    }
}
