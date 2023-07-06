using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //화면 중앙에 커서 고정
        Cursor.lockState = CursorLockMode.Locked;
        //커서 보이지않음
        Cursor.visible = false;
    }
}
