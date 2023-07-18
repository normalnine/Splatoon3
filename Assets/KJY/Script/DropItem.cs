using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
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
        if (PlayerHP.instance.isDie == true)
        {
            gage += Time.deltaTime; 
            Vector3 tmp = transform.position;
            tmp.y = 0;
            transform.position = Vector3.Slerp(transform.position, tmp, gage);
        }
        
    }
}
