using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour
{
    private void Awake()
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();

        if (bootstrapper != null) return;
        

        if (SceneManager.GetActiveScene().name != "Initial")
        {
            SceneManager.LoadScene("Initial");
        }

    }
    
}
