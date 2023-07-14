using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBodyMeshManager : MonoBehaviour
{
    public static HumanBodyMeshManager Instance;
    public SkinnedMeshRenderer[] MeshList;
    public Material[] materialsList;
    public int count;
    private void Awake()
    {
        Instance = this;   
    }
    // Start is called before the first frame update
    void Start()
    {
        count = 11;
        materialsList = new Material[11];
        for (int i = 0; i < 11; i++)
        {
            materialsList[i] = MeshList[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
