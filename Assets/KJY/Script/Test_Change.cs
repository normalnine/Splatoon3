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
    }

    // Update is called once per frame
    void Update()
    {
        SamePosition();
        ChangeHuman();
        ChangeSquid();
    }

    void ChangeHuman()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (humanBody.transform.position.y <= 0)
            {
                isHuman = true;
                TurnBody();
                //StartCoroutine(MoveBodyUp());
                StartCoroutine(Up());
            }
        }
    }

    void ChangeSquid()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isHuman = false;
            if (Test_Move.instance.jumping == false)
            {
                Vector3 tmp = humanBody.transform.position;
                tmp.y = -2f;
                humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, tmp, 0.1f);
            }
            if (humanBody.transform.position.y <= -1f)
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
            otherBody.SetActive(false);
        }
        else
        {
            squidBody.GetComponent<MeshRenderer>().enabled = false;
            humanBody.GetComponent<MeshRenderer>().enabled = true;
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
