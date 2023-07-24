using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_PauseGame : MonoBehaviour
{
    GameObject bgm;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f; // Pause the game
        bgm.GetComponent<AudioSource>().Stop();
    }
}
