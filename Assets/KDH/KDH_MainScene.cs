using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KDH_MainScene : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LowPolyFPS_Lite_Demo 1");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // esc 메뉴 열기
        }
    }
}
