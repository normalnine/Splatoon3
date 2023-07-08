using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float speed = 10;
    Rigidbody rb;

    public float currentTime;
    public float attackDelayTime = 5f;
    bool isForword;
    bool isBack;
    public Transform body;

    public float trackingTime = 1f;
    float trackingBossTime = 5.7f;
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
            body.transform.Rotate(body.transform.forward, 360 * Time.deltaTime, Space.World);
            if (currentTime < trackingTime)
            {
                GameObject target = GameObject.Find("Player");//게임오브젝트를 찾아줘(Find) Player
                dir = target.transform.position - transform.position;
                rb.velocity = dir.normalized * speed;
                // 앞방향을 rb.velocity의 방향과 같게
                transform.forward = rb.velocity.normalized;
            }
            else
            {
                // 앞방향을 rb.velocity의 방향과 같게
                transform.forward = rb.velocity.normalized;
            }
        }

        if (isBack == true)
        {
            transform.forward = -rb.velocity.normalized;
            Destroy(gameObject, 1.2f);
            if (currentTime > trackingBossTime)
            {
                GameObject boss = GameObject.Find("LFirePos").gameObject;//게임오브젝트를 찾아줘(Find) Player
                dir = boss.transform.position - transform.position;
                rb.velocity = dir.normalized * speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 바닥에 닿았다면
        if (other.gameObject.CompareTag("Ground"))
        {
            PlayerHP.instance.Shake();
            isForword = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            currentTime = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            currentTime += Time.deltaTime;
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
