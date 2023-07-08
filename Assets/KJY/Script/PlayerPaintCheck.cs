using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (KDH_ColorCheck.instance.ColorCheck() == 1)
        {

        }
    }
}
