using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_MainScene : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            // 튜토리얼 씬 열기
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // esc 메뉴 열기
        }
    }
}
