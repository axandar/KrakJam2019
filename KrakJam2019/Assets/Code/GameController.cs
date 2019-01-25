using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

//    [SerializeField] Text scoreText;
//    [SerializeField] Text healthText;
//    [SerializeField] GameObject gameUI;
//    [SerializeField] GameObject gameOverUI;
    
    
    [SerializeField] GameObject _player;
    
    [SerializeField] GameObject _bonusPrefab;
    [SerializeField] private float _timeToSpawn = 15;

    private int StopFirstCorutineInduction = 1;
    
    [Header("Map Sizes")] 
    [SerializeField] private float _minVectorXValue;
    [SerializeField] private float _maxVectorXValue;
    [SerializeField] private float _minVectorYValue;
    [SerializeField] private float _maxVectorYValue;

    
    int scoreValue;
     [SerializeField] int healthValue;

    private void Awake()
    {
        StartCoroutine(SpawnBonus());
    }

    void Start() {
//        gameUI.SetActive(true);
//        gameOverUI.SetActive(false);
    }
    
    void Update() {
//        scoreText.text = "Score: " + scoreValue;
//        healthText.text = "Health: " + healthValue;

        if (healthValue <= 0)
            GameOver();
    }

    void GameOver() {
//        gameUI.SetActive(false);
//        gameOverUI.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))  
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
    }

    private IEnumerator SpawnBonus()
    {
        while (true) {
            StopFirstCorutineInduction -= 1;
            
            if (StopFirstCorutineInduction <= 0) {
                
                var bonus =  Instantiate(_bonusPrefab, 
                    new Vector3(Random.Range(_minVectorXValue,_maxVectorXValue),
                        Random.Range(_minVectorYValue,_maxVectorYValue))
                    , new Quaternion(0,0,0,0));
                yield return new WaitForSeconds(_timeToSpawn);
                if(bonus != null)
                   Destroy(bonus);
                yield return new  WaitForSeconds(_timeToSpawn);
            }
        }
    }

    private void PickUpBonus()
    {
        if(_player.transform == )
    }
    
    private void PickUp()
    {
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
