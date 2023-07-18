using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Autoaim : MonoBehaviour
{
    public Image smallAim;
    public Camera cam;
    public float distance = 10f;
    bool isAming;
    Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        isAming = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();
        if (isAming)
        {
            AutoAming();
        }
    }

    void CheckTarget()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            if (hit.transform.gameObject.tag == "Boss")
            {
                if(!isAming)
                {
                    Debug.Log("Find Target");
                }
                Target = hit.transform;
                isAming=true;
            }
        }
        //else
        //{
        //    Target = null;
        //    isAming = false;
        //}
    }

    void AutoAming()
    {
    }
}
