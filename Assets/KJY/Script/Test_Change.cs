using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Change : MonoBehaviour
{
    public static Test_Change instance;

    //data
    public bool isHuman;
    public GameObject humanBody;
    public GameObject squidBody;
    private GameObject otherBody;
    //public Transform cameraArm_tmp;
    public Collider humanBodyCollider;
    public bool changeFormNow;
    private Vector3 desiredMoveDirection;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isHuman = true;
        squidBody.GetComponent<MeshRenderer>().enabled = false;
        otherBody = GameObject.Find("otherBody");
        changeFormNow = false;
    }

    // Update is called once per frame
    void Update()
    {
        SamePosition();
        ChangeHuman();
        ChangeSquid();
        if (changeFormNow == true)
        {
            ChangeNow();
        }
    }

    void ChangeHuman()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //cameraArm_tmp = cameraArm_tmp.transform;
            isHuman = true;
            //humanBodyCollider.transform.position = humanBody.transform.position;
            TurnBody();
            if (humanBody.transform.position.y < 0f)
            {
                //StartCoroutine(MoveBodyUp());
                StartCoroutine(Up());
            }
            else
            {
                changeFormNow = true;
            }
        }
    }

    void ChangeNow()
    {
        humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, humanBodyCollider.transform.position, 0.02f);
    }

    void ChangeSquid()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isHuman = false;
            if (Test_Move.instance.jumping == false)
            {
                Vector3 tmp = humanBody.transform.position;
                tmp.y = -2.5f;
                humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, tmp, 0.2f);
            }
            if (humanBody.transform.position.y <= 0f)
            {
                TurnBody();
            }
        }
    }

    void TurnBody()
    {
        if (isHuman == false)
        {
            humanBody.GetComponent<MeshRenderer>().enabled = false;
            squidBody.GetComponent<MeshRenderer>().enabled = true;
            //humanBodyCollider.enabled = false;
            //squidBodyCollider.enabled = true;
            otherBody.SetActive(false);
        }
        else
        {
            squidBody.GetComponent<MeshRenderer>().enabled = false;
            humanBody.GetComponent<MeshRenderer>().enabled = true;
            //squidBodyCollider.enabled = false;
           // humanBodyCollider.enabled= true;
            otherBody.SetActive(true);
        }
    }

    private void SamePosition()
    {
        squidBody.transform.position = humanBody.transform.position;
        squidBody.transform.rotation = humanBody.transform.rotation;
    }

    IEnumerator Up()
    {
        while (humanBody.transform.position.y <= 0)
        {
            Vector3 tmp = humanBody.transform.position;
            tmp.y = 0.1f;
            humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, tmp, 5f * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveBodyUp()
    {
        Vector3 startPos = humanBody.transform.position;
        Vector3 targetPos = new Vector3(startPos.x, 0f, startPos.z);
        float elapsedTime = 0f;
        float duration = 1f; // 변경에 걸리는 시간

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 보간 계수

            // 보간된 위치 계산
            Vector3 newPos = Vector3.Lerp(startPos, targetPos, t);
            humanBody.transform.position = newPos;

            yield return null;
        }
    }
}
