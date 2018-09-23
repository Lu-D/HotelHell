using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//gameOverScript
//draws gameover screen when scene changes to gameover
public class gameOverScript : MonoBehaviour {

    public Texture gameOverTexture;

    //OnGUI
    //adjusts size and placement of buttons and screen
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 825, 550), gameOverTexture);
        if (GUI.Button(new Rect(Screen.width / 2 - 65, Screen.height / 2 +135, 150, 25), "Restart"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 65 , Screen.height / 2 + 160, 150, 25), "Quit"))
        {
            Application.Quit();
        }
    }
}
