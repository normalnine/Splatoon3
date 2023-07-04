using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    public float speed = 10;
    Rigidbody rb;

    public float currentTime;
    public float attackDelayTime = 5f;
    bool isForword;
    bool isBack;

    public float trackingTime = 0.3f;
    float trackingBossTime = 5.7f;
    Vector3 dir; //������ ���� ����
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

        // �ٴڿ� �����������
        if (isForword)
        {
            if (currentTime < trackingTime)
            {
                GameObject target = GameObject.Find("Player");//���ӿ�����Ʈ�� ã����(Find) Player
                dir = target.transform.position - transform.position;
                rb.velocity = dir.normalized * speed;
                // �չ����� rb.velocity�� ����� ����
                transform.forward = rb.velocity.normalized;
            }
            else
            {
                // �չ����� rb.velocity�� ����� ����
                transform.forward = rb.velocity.normalized;
            }
        }

        if (isBack == true)
        {
            transform.forward = -rb.velocity.normalized;
            Destroy(gameObject, 1.2f);
            if (currentTime > trackingBossTime)
            {
                GameObject boss = GameObject.Find("LFirePos").gameObject;//���ӿ�����Ʈ�� ã����(Find) Player
                dir = boss.transform.position - transform.position;
                rb.velocity = dir.normalized * speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �ٴڿ� ��Ҵٸ�
        if (other.gameObject.CompareTag("Floor"))
        {
            isForword = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            currentTime = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackDelayTime)
            {
                rb.useGravity = true;
                // �� ������ ��ȯ
                rb.velocity = -transform.forward * speed * 1.5f;
                isBack = true;
            }
        }
    }
}
