using System;
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

    public float speed = 15;
    Rigidbody rb;

    public float currentTime;
    public float boomTime = 2.4f;
    Vector3 dir; //방향을 담을 변수

    Vector3 playerTarget;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerTarget = player.transform.position;
        rb = GetComponent<Rigidbody>();
        myTransform = transform;
        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile()
    {
        // 발사체를 던지는 물체의 위치로 이동 + 필요한 경우 일부 오프셋을 추가합니다.
        Projectile.position = myTransform.position; // + new Vector3(0, 0, 0);
        // 타겟까지의 거리 계산
        float target_Distance = Vector3.Distance(Projectile.position, playerTarget);

        // 지정된 각도에서 물체를 대상에 던지는 데 필요한 속도를 계산합니다.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // 속도의 X Y 성분 추출
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // 비행 시간을 계산합니다.
        float flightDuration = target_Distance / Vx;

        // 대상을 향하도록 발사체를 회전합니다.
        Projectile.rotation = Quaternion.LookRotation(playerTarget - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 바닥에 닿았다면
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentTime = 0;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            // 내 공격도 파괴하고싶다.
            Destroy(gameObject, 2.4f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
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
