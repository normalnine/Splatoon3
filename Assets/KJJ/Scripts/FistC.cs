using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistC : MonoBehaviour
{
    public static FistC instance;
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

    public float trackingTime = 2.5f;
    float trackingBossTime = 5f;

    public float bossFistHP = 50;
    public bool bhpF;
    Vector3 dir; //방향을 담을 변수
    // Start is called before the first frame update
    void Start()
    {
        isForword = true;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        // 바닥에 닿기전까지만
        if (isForword)
        {
            // 일정시간을 추적
            if (1.8f < currentTime && currentTime < trackingTime)
            {
                // Player게임오브젝트를 찾아줘
                GameObject target = GameObject.Find("Player");
                // 플레이어와의 방향을 구하고
                dir = target.transform.position - transform.position;
                // 플레이어 방향으로 날아간다.
                rb.velocity = dir.normalized * speed;
                // 앞방향을 rb.velocity의 방향과 같게
                transform.forward = rb.velocity.normalized;
            }
            // 추적하지 않는다면
            else if (currentTime < 1.8f)
            {
                // 앞방향을 rb.velocity의 방향과 같게
                transform.forward = rb.velocity.normalized;
            }
        }
        // 뒤로 날아갈 때
        if (isBack == true)
        {
            // 날아가는방향과 주먹을 일치
            transform.forward = -rb.velocity.normalized;
            // 1.2초뒤 삭제
            Destroy(gameObject, 1.2f);
            // 추적 시간이 넘으면 
            if (currentTime > trackingBossTime)
            {
                // 손으로 돌아갈때 손의 좌우 체크
                if (BossAttack.instance.didths == false)
                {
                    // Player게임오브젝트를 찾아줘 
                    GameObject boss = GameObject.Find("RFirePos").gameObject;
                    // 방향을 구하고
                    dir = boss.transform.position - transform.position;
                    // 날린다.
                    rb.velocity = dir.normalized * speed;
                }
                else
                {
                    // Player게임오브젝트를 찾아줘 
                    GameObject boss = GameObject.Find("LFirePos").gameObject;
                    // 방향을 구하고
                    dir = boss.transform.position - transform.position;
                    // 날린다
                    rb.velocity = dir.normalized * speed;
                }
            }
        }

        if (bossDie == true)
        {
            // boss게임오브젝트를 찾아줘
            GameObject boss = GameObject.Find("Boss(C)");
            // 방향을 구한다
            dir = boss.transform.position - transform.position;
            // 날아간다
            rb.velocity = dir.normalized * speed * 2.5f;
            transform.forward = -rb.velocity.normalized;
            bhpF = true;
        }
        // 체력이 1보다 낮아지면 
        if (bossFistHP < 1)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = -transform.forward * speed * 2f;
            bossDie = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 바닥에 닿았다면
        // 고정하고 시간을 초기화
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 고정시키고
            isForword = false;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.velocity = Vector3.zero;
            // 화면 흔들림
            PlayerHP.instance.Shake();
            // 시간을 초기화
            currentTime = 0;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHP.instance.PlayerStrongDamageProcess();
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss.instance.bossHP--;
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 바닥에 닿는동안
            // 시간을 누적하고
            currentTime += Time.deltaTime;
            // 일정시간이 지나면 공격을 뒤로 돌아가게함
            if (currentTime > attackDelayTime)
            {
                // 고정을 풀고
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                // 뒤로 날아가라
                rb.velocity = -transform.forward * speed * 1.5f;
                isBack = true;
            }
        }
    }

}
