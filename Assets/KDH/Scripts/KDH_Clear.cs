using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class KDH_Clear : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject clearTimer;

    void Start()
    {
        videoPlayer.Play();

        StartCoroutine(TimerSpawn());
    }

    IEnumerator TimerSpawn()
    {
        yield return new WaitForSecondsRealtime(4f);
        clearTimer.GetComponent<Text>().enabled = true;

        yield return new WaitForSecondsRealtime(5f);
        clearTimer.GetComponent<Text>().enabled = false;

    }
}
