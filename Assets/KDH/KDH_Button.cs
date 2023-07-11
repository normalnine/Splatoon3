using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KDH_Button : MonoBehaviour
{
    Vector2 originSize;
    public float upSizeValue;
    Text buttonText;
    RectTransform rectTransfrom;
    
    // Start is called before the first frame update
    void Start()
    {
        rectTransfrom = GetComponent<RectTransform>();
        originSize = rectTransfrom.sizeDelta;
        buttonText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMyPointEnter()
    {
        rectTransfrom.sizeDelta = new Vector2(originSize.x * upSizeValue, originSize.y);
        buttonText.color = Color.black;
    }

    public void OnMyPointExit()
    {
        rectTransfrom.sizeDelta = originSize;
        buttonText.color = Color.white;
    }


}
