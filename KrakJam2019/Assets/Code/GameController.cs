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
    public static float PlayerDMG;
    
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _bonusPrefab;
    [SerializeField] private float _timeToSpawn = 15;

    private int StopFirstCorutineInduction = 1;
    private GameObject bonus;
    private bool _dontAsk;

    [Header("Map Sizes")] 
    [SerializeField] private float _minVectorXValue;
    [SerializeField] private float _maxVectorXValue;
    [SerializeField] private float _minVectorYValue;
    [SerializeField] private float _maxVectorYValue;
    
    [Header("Bonus Values")]
    [SerializeField] private int _healingValues = 10;
    [SerializeField] private float _addSpeed = 0.1f;
    [SerializeField] private float _addDMG = 1;


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

        PickUpBonus();
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
            _dontAsk = true;
            StopFirstCorutineInduction -= 1;
            
            if (StopFirstCorutineInduction <= 0) {
                
                bonus =  Instantiate(_bonusPrefab, 
                    new Vector3(Random.Range(_minVectorXValue,_maxVectorXValue),
                        Random.Range(_minVectorYValue,_maxVectorYValue))
                    , new Quaternion(0,0,0,0));
                yield return new WaitForSeconds(_timeToSpawn);
                if (bonus != null) {
                    _dontAsk = false;
                    Destroy(bonus);
                }
                yield return new  WaitForSeconds(_timeToSpawn);
            }
        }
    }

    private void PickUpBonus()
    {
        if(!_dontAsk)
            return;
        
            if(Vector2.Distance(_player.transform.position, bonus.transform.position) <= 1){
            PickUp();
            Destroy(bonus);
            _dontAsk = false;
        }
    }
    
    private void PickUp()
    {
        var pickUpId = Random.Range(0,2);
        Debug.Log(pickUpId);
        switch (pickUpId) {
                case 0: //Health
                    healthValue += _healingValues;
                    break;
                case 1: //Speed 0.1
                    PlayerController.acceleration += _addSpeed;
                    break;
                
                case 2: //DMG 0.1
                    PlayerDMG += _addDMG;
                    break;
        }
    }
}
