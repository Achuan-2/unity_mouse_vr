using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIScript : MonoBehaviour
{
    public Dropdown dpd;
    // Start is called before the first frame update
    void Start()
    {
        if (dpd != null)
        {
            dpd.value = 1;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Test();
    }
    public void Test()
    {
        Debug.Log("Change to "+ dpd.options[dpd.value].text);
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(dpd.options[dpd.value].text);
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR //如果是在编辑器环境下
                UnityEditor.EditorApplication.isPlaying = false;
        #else//在打包出来的环境下
                Application.Quit();
        #endif
    }


}
