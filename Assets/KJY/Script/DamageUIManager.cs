using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DamageUIManager : MonoBehaviour
{
    public static DamageUIManager instance;
    //public Image[] imageList;
    public GameObject[] imageList;
    public int count;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        imageList = new GameObject[transform.childCount];
        for (int i = 1; i < transform.childCount; i++)
        {
            imageList[i] = transform.GetChild(i).gameObject;
            imageList[i].SetActive(false);
        }
        count = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
