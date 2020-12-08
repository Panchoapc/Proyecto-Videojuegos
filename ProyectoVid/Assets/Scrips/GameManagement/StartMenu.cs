﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit(); // este comando no funciona en el editor de UNITY
    }
}
