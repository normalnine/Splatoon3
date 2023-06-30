using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFormPlayer : MonoBehaviour
{
    public static ChangeFormPlayer instance;
    public bool isHuman;
    public GameObject body;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isHuman = true;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHuman();
        ChangeSquid();
        if (CameraAndMove.instance.jumping == false)
        {
            StopBody();
        }
    }

    void ChangeHuman()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) && isHuman == false)
        {
            body.GetComponent<Collider>().enabled = true;
            isHuman = true;
            if (transform.position.y < 0)
            {
                Vector3 tmp = transform.position;
                tmp.y = Mathf.Lerp(-2f, 0f, 0.5f);
                transform.position = tmp;
            }
        }
    }

    void ChangeSquid()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isHuman == true)
        {
            body.GetComponent<Collider>().enabled = false;
            isHuman = false;
        }
    }

    void StopBody()
    {
        if (isHuman == false)
        {
            if (transform.position.y <= -2)
            {
                Vector3 currentPosition = transform.position;
                currentPosition.y = -2f;
                transform.position = currentPosition;
            }
        }
    }
}
