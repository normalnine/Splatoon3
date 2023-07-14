using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CameraSlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        transform.position += dir * Time.deltaTime * 5f; 
    }
}
