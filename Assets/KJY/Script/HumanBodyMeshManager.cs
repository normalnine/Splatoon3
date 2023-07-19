using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBodyMeshManager : MonoBehaviour
{
    public static HumanBodyMeshManager Instance;
    public SkinnedMeshRenderer[] MeshList;
    public Material[] materialsList;
    public int Materialcount;
    public int MeshCount;
    private void Awake()
    {
        Instance = this;   
    }
    // Start is called before the first frame update
    void Start()
    {
        MeshCount = 3;
        Materialcount = 10;
        MeshList = new SkinnedMeshRenderer[MeshCount];
        for (int i = 0; i < MeshCount; i++)
        {
            MeshList[i] = transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
        }
        materialsList = new Material[10];
        for (int i = 0; i < 8; i++)
        {
            materialsList[i] = MeshList[0].materials[i];
        }
        materialsList[8] = MeshList[1].material;
        materialsList[9] = MeshList[2].material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
