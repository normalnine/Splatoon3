using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    public static Fist instance;
    private void Awake()
    {
        instance = this;
    }

    public float speed = 10;
    Rigidbody rb;

    public float currentTime;
    public float attackDelayTime = 5f;
    bool isForword;
    bool isBack;
    bool bossDie;

    public float trackingTime = 0.3f;
    float trackingBossTime = 5.7f;

    public float bossFistHP = 8;
    Vector3 dir; //방향을 담을 변수
    // Start is called before the first frame update
    void Start()
    {
        isForword = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        // 바닥에 닿기전까지만
        if (isForword)
        {
            // 일정시간을 추적
            if (currentTime < trackingTime)
            {
                GameObject target = GameObject.Find("Player");//게임오브젝트를 찾아줘(Find) Player
                dir = target.transform.position - transform.position;
                rb.velocity = dir.normalized * speed;
                // 앞방향을 rb.velocity의 방향과 같게
                transform.forward = rb.velocity.normalized;
            }
            else// 추적하지 않는다면
            {
                // 앞방향을 rb.velocity의 방향과 같게
                transform.forward = rb.velocity.normalized;
            }
        }

        if (isBack == true)
        {
            // 날아가는방향과 주먹을 일치
            transform.forward = -rb.velocity.normalized;
            Destroy(gameObject, 1.2f);
            if (currentTime > trackingBossTime)
            {
                if(BossAttack.instance.didths == false)
                {
                    GameObject boss = GameObject.Find("RFirePos").gameObject;//게임오브젝트를 찾아줘(Find) Player
                    dir = boss.transform.position - transform.position;
                    rb.velocity = dir.normalized * speed;
                }
                else
                {
                    GameObject boss = GameObject.Find("LFirePos").gameObject;//게임오브젝트를 찾아줘(Find) Player
                    dir = boss.transform.position - transform.position;
                    rb.velocity = dir.normalized * speed;
                }
            }
        }

        if (bossDie == true)
        {
            transform.forward = -rb.velocity.normalized;
            Destroy(gameObject, 0.6f);
            GameObject boss = GameObject.Find("Boss");//게임오브젝트를 찾아줘(Find) Player
            dir = boss.transform.position - transform.position;
            rb.velocity = dir.normalized * speed * 2.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 바닥에 닿았다면
        // 고정하고 시간을 초기화
        if (other.gameObject.CompareTag("Ground"))
        {
            PlayerHP.instance.Shake();
            isForword = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            currentTime = 0;
            if (bossFistHP < 1)
            {
                rb.useGravity = true;
                bossDie = true;
            }
        }
        if (other.gameObject.CompareTag("bullet"))
        {
            bossFistHP--;
            if (bossFistHP < 1)
            {
                rb.useGravity = true;
                bossDie = true;
                Boss.instance.bossHP--;
            }
        }
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerHP.instance.PlayerStrongDamageProcess();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 바닥에 닿는동안
        if (other.gameObject.CompareTag("Ground"))
        {
            // 시간을 누적하고
            currentTime += Time.deltaTime;
            // 일정시간이 지나면 공격을 뒤로 돌아가게함
            if (currentTime > attackDelayTime)
            {
                rb.useGravity = true;
                // 앞 방향을 전환
                rb.velocity = -transform.forward * speed * 1.5f;
                isBack = true;
            }
        }
    }
}
