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
    [SerializeField] Text criticalHealthText;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject criticalHealthUI;
    
    int scoreValue;
    [SerializeField] int healthValue;

    void Start() {
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
        criticalHealthUI.SetActive(false);
    }
    
    void Update() {
        scoreText.text = "Score: " + scoreValue;
        healthText.text = "Health: " + healthValue;

        if (Input.GetKey(KeyCode.K))
            healthValue = healthValue - 10;
        
        if (healthValue <= 0)
            GameOver();

        HealthIndicator();
    }

    void HealthIndicator() {
        if (healthValue > 50)
            healthText.color = Color.green;
        if (healthValue <= 50 && healthValue > 30)
            healthText.color = Color.yellow;
        if (healthValue <= 30) {
            healthText.color = Color.red;
            criticalHealthText.color = Color.red;
            StartCoroutine(FlashCriticalHealthUI());
        } else StopCoroutine(FlashCriticalHealthUI());
    }

    IEnumerator FlashCriticalHealthUI() {
        while (true) {
            criticalHealthUI.SetActive(true);
            criticalHealthText.text = " ";
            yield return new WaitForSeconds(.7f);
            criticalHealthText.text = "Health critical!";
            yield return new WaitForSeconds(.7f);
            criticalHealthUI.SetActive(false);
        }
    }

    void GameOver() {
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))  
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
    }
}
