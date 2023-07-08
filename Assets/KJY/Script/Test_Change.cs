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

    public bool sameColor;
    public float currentTime;
    public float MaxlimitTime;
    //bool changeImm;
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
        sameColor = true;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SamePosition();
        ChangeHuman();
        ChangeSquid();
        ChangeNow();
        //if (changeFormNow == true)
        //{
        //}
    }

    void ChangeHuman()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sameColor = true;
            //cameraArm_tmp = cameraArm_tmp.transform;
            isHuman = true;
            //humanBodyCollider.transform.position = humanBody.transform.position;
            TurnBody();
            if (humanBody.transform.position.y < 0f)
            {
                //StartCoroutine(MoveBodyUp());
                StartCoroutine(Up());
            }
            if (transform.position.y > 0f)
            {
                changeFormNow = true;
            }
        }
    }

    void ChangeHumanImm()
    {
        isHuman = true;
        sameColor = true;
        TurnBody();
        if (humanBody.transform.position.y < 0f)
        {
            StartCoroutine(Up());
        }
    }


    void ChangeNow()
    {
        humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, humanBodyCollider.transform.position, 0.02f);
    }

    void ChangeSquid()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && KDH_ColorCheck.instance.ColorCheck() == 3)
        {
            isHuman = false;
            sameColor = false;
            TurnBody();
        }
        else if (Input.GetKey(KeyCode.LeftShift) && sameColor == true)
        {
            isHuman = false;
            if (KDH_ColorCheck.instance.ColorCheck() == 3)
            {
                currentTime += Time.deltaTime;
                if (currentTime > MaxlimitTime)
                {
                    sameColor = false;
                    currentTime = 0;
                    //ChangeHumanImm();
                }
            }
            else
            {
                currentTime = 0;
            }
            if (Test_Move.instance.jumping == false)
            {
                Vector3 tmp = humanBody.transform.position;
                tmp.y = -1f;
                humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, tmp, 0.2f);
                if (humanBody.transform.position.y <= -0.5f)
                {
                    TurnBody();
                }
            }
        }
    }

    void TurnBody()
    {
        if (isHuman == false)
        {
            humanBody.GetComponent<MeshRenderer>().enabled = false;
            if (KDH_ColorCheck.instance.ColorCheck() == 3)
            {
                squidBody.GetComponent<MeshRenderer>().enabled = true;
            }
            else if(KDH_ColorCheck.instance.ColorCheck() == 1)
            {
                squidBody.GetComponent<MeshRenderer>().enabled = false;

            }
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
            if (isHuman == false)
                break;
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
