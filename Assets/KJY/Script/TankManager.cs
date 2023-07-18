using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    public static TankManager instance;
    public SkinnedMeshRenderer[] TankList;
    public Material[] TankMaterialsList;
    public int TankCount;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        TankCount = transform.childCount - 5;
        TankList = new SkinnedMeshRenderer[TankCount];
        for (int i = 0; i < TankCount; i++)
        {
            TankList[i] = transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
        }
        TankMaterialsList = new Material[TankCount];
        for (int i= 0; i < TankList.Length; i++)
        {
            TankMaterialsList[i] = TankList[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
