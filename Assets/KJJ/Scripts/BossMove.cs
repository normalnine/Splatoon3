using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public static BossMove instance;
    private void Awake()
    {
        instance = this;
    }
    public Transform[] Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;

    public void Start()
    {
        myTransform = transform;
        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile()
    {
        // 발사체를 던지는 물체의 위치로 이동 + 필요한 경우 일부 오프셋을 추가합니다.
        Projectile.position = myTransform.position;
        // 타겟까지의 거리 계산
        float target_Distance = Vector3.Distance(Projectile.position, Target[Boss.instance.movePositionCount].position);

        // 지정된 각도에서 물체를 대상에 던지는 데 필요한 속도를 계산합니다.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // 속도의 X Y 성분 추출
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // 비행 시간을 계산합니다.
        float flightDuration = target_Distance / Vx;

        // 대상을 향하도록 발사체를 회전합니다.
        Projectile.rotation = Quaternion.LookRotation(Target[Boss.instance.movePositionCount].position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;
            yield return null;
        }
    }
}