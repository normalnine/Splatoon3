using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Change : MonoBehaviour
{
    public static Player_Change instance;
    public enum State
    {
        Human,
        Squid,
        //SquidGround,
        //SquidAnotherInk
    }
    public State state;

    public GameObject humanBody;
    public GameObject squidBody;
    public GameObject otherBody;
    public Collider humanBodyCollider;

    SkinnedMeshRenderer[] humanMeshList;
    public SkinnedMeshRenderer[] squidMeshList;
    int humanCount;
    int squidCount;

    public float currentTime;
    public float MaxlimitTime;

    private bool changeImm;

    private void Awake()
    {
        instance = this;
        squidCount = 2;
        for (int i = 0; i < squidCount; i++)
        {
            squidMeshList[i].enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       state = State.Human;
       currentTime = 0;
       changeImm = false;
       humanCount = HumanBodyMeshManager.Instance.count;
       humanMeshList = HumanBodyMeshManager.Instance.MeshList;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHuman();
        ChangeSquid();
        SetBodyPosition();
        ChaneOnTheGround();
    }

    void ChangeHuman()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            state = State.Human;
            changeImm = false;
            TurnBody();
            if (humanBody.transform.position.y < 0f)
            {
                StartCoroutine(Up());
            }
        }
    }

    void ChangeHumanImm()
    {
        state = State.Human;
        changeImm = true;
        TurnBody();
        if (humanBody.transform.position.y < 0f)
        {
            StartCoroutine(Up());
        }
    }

    void ChangeSquid()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Player_CameraAndMove.instance.inkState != Player_CameraAndMove.InkState.none && changeImm == false)
        {
            state = State.Squid;
            if(Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.other)
            {
                currentTime += Time.deltaTime;
                if (currentTime > MaxlimitTime)
                {
                    ChangeHumanImm();
                    currentTime = 0;
                }
                if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.my)
                {
                    currentTime = 0;
                }
            }
            if (Player_CameraAndMove.instance.jumping == false)
            {
                Vector3 tmp = humanBody.transform.position;
                tmp.y = -2f;
                humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, tmp, 0.2f);
            }
            if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.my)
            {
                ShootingTest.instance.INKGAGE += 0.5f;
            }
                TurnBody();
        }
        else if(Input.GetKey(KeyCode.LeftShift) && Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.none)
        {
            state = State.Squid;
            changeImm = false;
            if (Player_CameraAndMove.instance.jumping == false)
            {
                Vector3 tmp = humanBody.transform.position;
                tmp.y = -2f;
                humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, tmp, 0.2f);
            }
            TurnBody();
        }
    }

    void SetBodyPosition()
    {
        Vector3 changeTmp = humanBody.transform.position;
        changeTmp.y = humanBody.transform.position.y + 2;
        squidBody.transform.position = changeTmp;
        squidBody.transform.rotation = humanBody.transform.rotation;
    }

    void ChaneOnTheGround()
    {
        humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, humanBodyCollider.transform.position, 0.02f);
    }

    void TurnBody()
    {
        if (state == State.Human)
        {
            for(int  i = 0; i < humanCount; i++)
            {
                humanMeshList[i].enabled = true;
            }
            for (int i = 0; i < squidCount; i++)
            {
                squidMeshList[i].enabled = false;
            }
            otherBody.SetActive(true);
        }
        else if (state == State.Squid && Player_CameraAndMove.instance.inkState != Player_CameraAndMove.InkState.none && Player_CameraAndMove.instance.inkState != Player_CameraAndMove.InkState.other)
        {
            for (int i = 0; i < humanCount; i++)
            {
                humanMeshList[i].enabled = false;
            }
            for (int i = 0; i < squidCount; i++)
            {
                squidMeshList[i].enabled = false;
            }
            otherBody.SetActive(false);
        }
        else if (state == State.Squid && Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.none)
        {
            for (int i = 0; i < humanCount; i++)
            {
                humanMeshList[i].enabled = false;
            }
            for (int i = 0; i < squidCount; i++)
            {
                squidMeshList[i].enabled = true;
            }
            otherBody.SetActive(false);
        }
        else if (state == State.Squid && Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.other || Player_CameraAndMove.instance.jumping == true)
        {
            print("Int");
            for (int i = 0; i < humanCount; i++)
            {
                humanMeshList[i].enabled = false;
            }
            for (int i = 0; i < squidCount; i++)
            {
                squidMeshList[i].enabled = true;
            }
            otherBody.SetActive(false);
        }
    }

    IEnumerator Up()
    {
        while (humanBody.transform.position.y <= 0)
        {
            Vector3 tmp = humanBody.transform.position;
            tmp.y = 0.1f;
            humanBody.transform.position = Vector3.Lerp(humanBody.transform.position, tmp, 5f * Time.deltaTime);
            if (state == State.Squid)
                break;
            yield return null;
        }
    }
}
