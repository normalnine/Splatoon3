using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KDH_Button : MonoBehaviour
{
    Vector2 originSize;
    float upSizeValue = 1.2f;
    public Text buttonText;
    public Text triangle;
    RectTransform rectTransfrom;
    
    // Start is called before the first frame update
    void Start()
    {
        rectTransfrom = GetComponent<RectTransform>();
        originSize = rectTransfrom.sizeDelta;
        triangle.enabled = false;
    }

    public void OnMyPointEnter()
    {
        rectTransfrom.sizeDelta = new Vector2(originSize.x * upSizeValue, originSize.y);
        buttonText.color = Color.black;
        triangle.enabled = true;
    }

    public void OnMyPointExit()
    {
        rectTransfrom.sizeDelta = originSize;
        buttonText.color = Color.white;
        triangle.enabled = false;

    }

    public void Restart()
    {
        // 보스 씬 열기
        SceneManager.LoadScene("KJYScene_TEST");
    }

    public void GoLobby()
    {
        // 대기실 씬 열기
        SceneManager.LoadScene("LowPolyFPS_Lite_Demo 1");
    }
    

}
