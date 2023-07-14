using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_ButtonESC : MonoBehaviour
{
    GameObject escUI;

    public void OnMyButtonYes()
    {
        // 대기실 씬으로 이동
    }

    public void OnMyButtonNo()
    {
        Time.timeScale = 1;
        escUI.SetActive(false);
    }
}
