﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startSceneScript : MonoBehaviour {

    public Texture startTexture;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), startTexture);
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 150, 25), "Start"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 25, 150, 25), "Quit"))
        {
            Application.Quit();
        }
    }
}