using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBoom : MonoBehaviour
{
    public static InkBoom instance;
    private void Awake()
    {
        instance = this;
    }

    public float speed = 10;
    Rigidbody rb;

    public float currentTime;
    bool isForword;
    // 추적시간
    public float trackingTime = 0.3f;
    public float boomTime = 2.4f;
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
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 바닥에 닿았다면
        if (other.gameObject.CompareTag("Ground"))
        {
            currentTime = 0;
            isForword = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            // 내 공격도 파괴하고싶다.
            Destroy(gameObject, 2.4f);
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Inkboom();
        }
    }

    void Inkboom()
    {
        //시간누적 2초
        if (currentTime > boomTime)
        {
            //터지는 이펙트

            // 반경 5M 안의 충돌체중에 적이있다면
            int layer = 1 << LayerMask.NameToLayer("Player");
            Collider[] cols = Physics.OverlapSphere(transform.position, 5, layer);
            for (int i = 0; i < cols.Length; i++)
            {
                // 데미지를 주고싶다
                cols[i].GetComponent<PlayerHP>().PlayerStrongDamageProcess();
            }
        }
    }


}
