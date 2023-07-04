using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject handFactory;
    public GameObject fistFactory;
    public float currentTime;
    public float attackTime = 5;

    //Hand attack;
    // Start is called before the first frame update
    void Start()
    {
        //attack = GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > attackTime)
        {
            int rValue = Random.Range(0, 9); // 랜덤한 0~9까지의 수를 만든다.
            if (rValue < 3)
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
}
