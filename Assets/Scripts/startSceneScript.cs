using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//startSceneScript
//draws start screen when game begins or scene is on startScene
public class startSceneScript : MonoBehaviour {

    public Texture startTexture;

    //OnGUI
    //adjusts size and placement of buttons and screen
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 825, 550), startTexture);
        if (GUI.Button(new Rect(Screen.width / 2 - 65, Screen.height / 2, 150, 25), "Start"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (GUI.Button(new Rect(Screen.width / 2 -65, Screen.height / 2 + 25, 150, 25), "Quit"))
        {
            Application.Quit();
        }
    }
}
