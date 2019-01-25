using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.InteropServices;
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
    [SerializeField] GameObject cameraGO;
    
    [SerializeField] GameObject _player;
    
    //[SerializeField] GameObject _bonusPrefab;
    [SerializeField] float _timeToSpawn = 15;

    int StopFirstCorutineInduction = 1;
    
    [Header("Map Sizes")] 
    [SerializeField]
    float _minVectorXValue;
    [SerializeField] float _maxVectorXValue;
    [SerializeField] float _minVectorYValue;
    [SerializeField] float _maxVectorYValue;
    
    int scoreValue;
    [SerializeField] int healthValue;
    bool isPies;

    void Awake() {
//        StartCoroutine(SpawnBonus());
    }
    
    void Start() {
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
        criticalHealthUI.SetActive(false);
    }
    
    void Update() {
        _player.transform.position = cameraGO.transform.position;
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
        }
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
        Destroy(criticalHealthUI);
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

//    IEnumerator SpawnBonus()
//    {
//        while (true) {
//            StopFirstCorutineInduction -= 1;
//            
//            if (StopFirstCorutineInduction <= 0) {
//                
//                var bonus =  Instantiate(_bonusPrefab, 
//                    new Vector3(Random.Range(_minVectorXValue,_maxVectorXValue),
//                        Random.Range(_minVectorYValue,_maxVectorYValue))
//                    , new Quaternion(0,0,0,0));
//                yield return new WaitForSeconds(_timeToSpawn);
//                if(bonus != null)
//                   Destroy(bonus);
//                yield return new  WaitForSeconds(_timeToSpawn);
//            }
//        }
//    }

    void PickUpBonus() {
     //   if(_player.transform == )
    }

    void PickUp() {
        var pickUpId = Random.Range(0,2);
        switch (pickUpId) {
                case 0: //Health
                    
                    break;
                case 1: //Speed
                    
                    break;
                
                case 2: //DMG
                    
                    break;
        }
    }
}
