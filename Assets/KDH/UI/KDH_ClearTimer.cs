using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KDH_ClearTimer : MonoBehaviour
{
    private float accumulatedTime = 0f;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        //timeText = GetComponent<Text>();
        UpdateTimeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;
        UpdateTimeDisplay();
    }

    void UpdateTimeDisplay()
    {
        
        int minutes = Mathf.FloorToInt(accumulatedTime / 60f);
        int seconds = Mathf.FloorToInt(accumulatedTime % 60f);
        int milliseconds = Mathf.FloorToInt((accumulatedTime * 100f) % 100f);

        string timeString = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        timeText.text = "Time: " + timeString;
    }
}
