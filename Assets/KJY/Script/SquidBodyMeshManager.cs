using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBodyMeshManager : MonoBehaviour
{
    public static SquidBodyMeshManager instance;
    public SkinnedMeshRenderer[] squidList;
    public int squidCount;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
