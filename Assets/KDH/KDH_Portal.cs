using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;


public class KDH_Portal : MonoBehaviour
{
    bool isPlayerTriggered;
    bool isPortal;

    [SerializeField] private Color targetColor = Color.black;
    [SerializeField, Range(0f, 1f)] private float fadeSpeed = 0.5f;

    private PostProcessVolume postProcessVolume;
    private ColorGrading colorGradingLayer;

    private void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out colorGradingLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.LeftShift) && isPlayerTriggered)
        {

            Invoke("GoBossScene", 2);
            isPortal = true;
        }

        if(isPortal)
        {
            float darkness = Mathf.Clamp01(colorGradingLayer.colorFilter.value.r - fadeSpeed * Time.deltaTime);
            colorGradingLayer.colorFilter.value = new Color(darkness, darkness, darkness, 1f);
            print(isPortal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerTriggered = false;

        }
    }

    void GoBossScene()
    {
        SceneManager.LoadScene("KJYScene_TEST");
    }

}
