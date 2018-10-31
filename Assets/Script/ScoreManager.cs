using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager scoreManager;

    public int scoreLeft;
    public int scoreRight;


	// Use this for initialization
	void Start () {

        if (scoreManager == null)
        {
            scoreManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        scoreLeft = scoreRight = 0;
		
	}

    //If the ball hits the right death zone, add points for left player and vice versa
    public void addScore(int scoreValue, DeadZoneScript.DeathZoneDirection direction)
    {
        if (direction == DeadZoneScript.DeathZoneDirection.LEFT)
        {
            scoreRight = scoreRight + scoreValue;
        }
        else if (direction == DeadZoneScript.DeathZoneDirection.RIGHT)
        {
            scoreLeft = scoreLeft + scoreValue;
        }

        if (scoreLeft == GameManager.gameManager.maxScore)
        {
            endGame("Player Left Won!!");
        }
        else if (scoreRight == GameManager.gameManager.maxScore)
        {
            endGame("Player Right Won!!");
        }
    }

    //When the game ends, change sounds and start a timer for restart.
    public void endGame(string s)
    {
        UIManager.uIManager.EndGame(s);
        BGMManager.bGMManager.audioSource.Stop();
        SFXManager.sFXManager.playSound(SFXManager.sFXManager.gameOverSound);
        GameManager.gameManager.deleteAllBalls();
        GameManager.gameManager._currentGameState = GameManager.GameState.ENDED;
        StartCoroutine(waitBeforeRestart());
    }

    IEnumerator waitBeforeRestart()
    {
        float restartTime = 5.0f;
        yield return new WaitForSeconds(restartTime);
        GameManager.gameManager.restartGame();
    }
}
