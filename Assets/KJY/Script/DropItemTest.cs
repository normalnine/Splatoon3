using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemTest : MonoBehaviour
{
    float gage;

    // Start is called before the first frame update
    void Start()
    {
        gage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -1f * 0.5f, 0);
    }
}
