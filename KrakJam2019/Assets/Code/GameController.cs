using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] Text scoreText;
    int scoreValue;

    void Update() {
        scoreText.text = "Score: " + scoreValue;
    }
}
