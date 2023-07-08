using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ShowLine : MonoBehaviour
{
    GameObject s1;
    Vector3 startPos, endPos;
    LineRenderer lr;
    public float rotX;
    public float rotY;
    public GameObject sphereEffect;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        sphereEffect.GetComponent<MeshRenderer>().enabled = false;
        s1 = GameObject.Find("ThrowTarget");
    }

    // Update is called once per frame
    void Update()
    {
        Parabola();
    }

    void Parabola()
    {
        rotX += Input.GetAxis("Mouse Y");
        rotX = Mathf.Clamp(rotX, 0f, 20f);
        if (rotX < 0f)
        {
            rotX = 0;
        }
        rotY += Input.GetAxis("Mouse X");
        startPos = s1.transform.position;
        rotY = Mathf.Clamp(rotY, -5f, 5f);
        endPos = new Vector3(rotY, 0, rotX);
        RaycastHit hit;
        Physics.Raycast(startPos, endPos, out hit);
        if (hit.collider != null)
        {
            sphereEffect.GetComponent<MeshRenderer>().enabled = true;
            sphereEffect.transform.position = new Vector3(endPos.x, endPos.y - 1, endPos.z);
        }
        //endPos = new Vector3(s2.rotation.x, 0, s2.position.y);

        Vector3 center = (startPos + endPos) * 0.5f;
        center.y -= 2;

        startPos = startPos - center;
        endPos = endPos - center;
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1));
            point += center;
            if (point.y <= 0)
            {
                Vector3 tmp = point;
                tmp.y = 0;
                point = tmp;
            }
            lr.SetPosition(i, point);
        }
    }
}
