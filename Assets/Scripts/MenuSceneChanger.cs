using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneChanger : MonoBehaviour
{
    // Update is called once per frame
   void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("Level_1_Test");
        }
    }
}
