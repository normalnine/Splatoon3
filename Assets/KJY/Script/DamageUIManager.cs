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
        count = 5;
        imageList = new GameObject[count];
        for (int i = 1; i < count; i++)
        {
            imageList[i] = transform.GetChild(i).gameObject;
            imageList[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
