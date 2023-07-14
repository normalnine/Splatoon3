using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float offsetX = 0f;
    public float offsetY = 25f;
    public float offsetZ = -35f;
    public Transform body;
    Vector3 cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition.x = body.transform.position.x + offsetX;
        cameraPosition.y = body.transform.position.y + offsetY;
        cameraPosition.z = body.transform.position.z + offsetZ;
        transform.position = Vector3.Lerp(transform.position, body.position, 0.5f * Time.deltaTime);
    }
}
