using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] Text scoreText;
    [SerializeField] Text healthText;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject gameOverUI;
    
    int scoreValue;
    [SerializeField] int healthValue;

    void Start() {
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
    }
    
    void Update() {
        scoreText.text = "Score: " + scoreValue;
        healthText.text = "Health: " + healthValue;

        if (healthValue <= 0)
            GameOver();
    }

    void GameOver() {
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))  
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
    }
}
