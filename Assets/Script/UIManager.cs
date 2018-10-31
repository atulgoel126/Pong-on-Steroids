using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public Text scoreLeft;
    public Text scoreRight;
    public Text startText;

    public GameObject centerLine;

    public static UIManager uIManager;
    private void Start()
    {
        if (uIManager == null)
        {
            uIManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StartCoroutine(startCountdown());
        centerLine.SetActive(false);
    }

    private void Update()
    {
        scoreLeft.text = "Score : " + ScoreManager.scoreManager.scoreLeft.ToString();
        scoreRight.text = "Score : " + ScoreManager.scoreManager.scoreRight.ToString();
    }

    IEnumerator startCountdown()
    {
        if (startText == null)
        {
            Debug.Log("NULL");
        }
        yield return new WaitForSeconds(0.5f);
        startText.text = "REACH SCORE " + GameManager.gameManager.maxScore + " TO WIN!";
        yield return new WaitForSeconds(2.0f);
        startText.text = "3";
        yield return new WaitForSeconds(1.0f);
        startText.text = "2";
        yield return new WaitForSeconds(1.0f);
        startText.text = "1";
        yield return new WaitForSeconds(1.0f);
        startText.text = "GO!";
        yield return new WaitForSeconds(1.0f);
        startText.text = "";
        centerLine.SetActive(true);
        GameManager.gameManager.startGameCall();
    }

    public void EndGame(String s)
    {
        startText.text = s;
        //centerLine.SetActive(false);
    }
}
