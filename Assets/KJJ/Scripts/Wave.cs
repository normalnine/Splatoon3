using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public static Wave instance;
    private void Awake()
    {
        instance = this;
    }
    Vector3 target;

    public Transform target1, target2;
    public GameObject waveFactory;
    public Transform wavePos;

    bool waveon;
    Vector3 origin;

    public float bossFistHP = 50;
    Rigidbody rb;
    Vector3 dir; //방향을 담을 변수
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        target = player.transform.position;
        origin = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    float currentTime;
    void Update()
    {
        if (currentTime > 0.5)
        {
            if (waveon == false)
                WaveEffect();
        }

        currentTime += Time.deltaTime;
        if (currentTime > 1)
            currentTime = 1;

        transform.position = GetCurvePoint4(origin, target1.position, target2.position, target, currentTime);


        // 날아가는방향과 주먹을 일치
        transform.forward = -rb.velocity.normalized;
        // 1.2초뒤 삭제
        //Destroy(gameObject, 1.2f);

        // 손으로 돌아갈때 손의 좌우 체크
        if (BossAttack.instance.didths == false)
        {
            // Player게임오브젝트를 찾아줘 
            GameObject boss = GameObject.Find("RFirePos").gameObject;
            // 방향을 구하고
            dir = boss.transform.position - transform.position;
            // 날린다.
            rb.velocity = dir.normalized * 15;
        }
        else
        {
            // Player게임오브젝트를 찾아줘 
            GameObject boss = GameObject.Find("LFirePos").gameObject;
            // 방향을 구하고
            dir = boss.transform.position - transform.position;
            // 날린다
            rb.velocity = dir.normalized * 15;
        }


    }

    Vector3 GetCurvePoint4(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 cd = Vector3.Lerp(c, d, t);

        Vector3 abbc = Vector3.Lerp(ab, bc, t);
        Vector3 bccd = Vector3.Lerp(bc, cd, t);

        return Vector3.Lerp(abbc, bccd, t);
    }

    void WaveEffect()
    {
        GameObject wave = Instantiate(waveFactory);
        wave.transform.parent = wavePos;
        wave.transform.position = wavePos.position;
        
        waveon = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        // 바닥에 닿았다면
        // 고정하고 시간을 초기화
        if (other.gameObject.CompareTag("Ground"))
        {
            // 고정시키고
            rb.constraints = RigidbodyConstraints.FreezeAll;
            // 화면 흔들림
            PlayerHP.instance.Shake();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHP.instance.PlayerStrongDamageProcess();
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            Boss.instance.bossHP--;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 바닥에 닿았다면
        // 고정하고 시간을 초기화
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 고정시키고
            rb.constraints = RigidbodyConstraints.FreezeAll;
            // 화면 흔들림
            PlayerHP.instance.Shake();
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
}
