using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControlScript : MonoBehaviour {

    public Texture gameOverTexture;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), gameOverTexture);
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 150, 25), "Restart"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 25, 150, 25), "Quit"))
        {
            Application.Quit();
        }
    }
}
