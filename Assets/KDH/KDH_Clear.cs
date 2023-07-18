using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class KDH_Clear : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        this.gameObject.SetActive(false);
        //videoPlayer.Play();
    }
}
