using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KDH_Portal : MonoBehaviour
{
    bool isPlayerTriggered;
    bool isPortal;
    public Image fadeImage;
    Color fadeColor;

    void Start()
    {
        fadeColor = fadeImage.color;
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
            print(isPortal);
            fadeColor.a += Time.deltaTime;
            fadeImage.color = fadeColor;
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

    //IEnumerator MainSplash()

    //{
    //    Color color = image.color;                            //color 에 판넬 이미지 참조

    //    for (int i = 100; i >= 0; i--)                            //for문 100번 반복 0보다 작을 때 까지
    //    {
    //        color.a -= Time.deltaTime * 0.01f;               //이미지 알파 값을 타임 델타 값 * 0.01
    //        image.color = color;                                //판넬 이미지 컬러에 바뀐 알파값 참조

    //        if (image.color.a <= 0)                        //만약 판넬 이미지 알파 값이 0보다 작으면
    //        {
    //            checkbool = true;                              //checkbool 참 
    //        }
    //    }
        
    //    yield return null;                                        //코루틴 종료

    //}

}


