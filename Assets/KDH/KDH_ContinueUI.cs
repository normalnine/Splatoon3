using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_ContinueUI : MonoBehaviour
{
    public AnimationCurve ac;
    private Vector3 origin;
    float i = 0;

    void Start()
    {
        //ac = GetComponent<AnimationCurve>();
        origin = transform.localPosition;        
    }

    // Update is called once per frame
    void Update()
    {
        i += 0.005f;
        float value = ac.Evaluate(i);        
        transform.localPosition = origin+ Vector3.up * value * 20;
    }
}
