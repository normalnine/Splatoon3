using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_ESC : MonoBehaviour
{
    public GameObject escUI;
    bool isESC;

    // Start is called before the first frame update
    void Start()
    {
        escUI.SetActive(false);
        isESC = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isESC)
            {
                escUI.SetActive(false);
                isESC = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;

            }
            else
            {
                escUI.SetActive(true);
                isESC = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void MyNoButton()
    {
        escUI.SetActive(false);
        isESC = false;
        Time.timeScale = 1;
    }

    public void MyYesButton()
    {
#if UNITY_EDITOR
        // If in the Unity editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            // If running in WebGL, use JavaScript to close the browser tab (requires a JavaScript function)
            CloseBrowserTab();
#else
            // For other platforms, use the Application.Quit() method
            Application.Quit();
#endif
    }
}
