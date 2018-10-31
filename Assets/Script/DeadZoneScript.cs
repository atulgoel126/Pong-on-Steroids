using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script detects when the ball has entered the deadZone and
 * tells the respective paddleHandler
 */
public class DeadZoneScript : MonoBehaviour {

    public enum DeathZoneDirection
    {
        LEFT,
        RIGHT
    };

    private int scoreValue;

    public DeathZoneDirection direction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            ScoreManager.scoreManager.addScore(GameManager.gameManager._scoreValue, direction);
            Destroy(other.gameObject);
            GameManager.gameManager.currentBallCount--;
            GameManager.gameManager.spawnNewBall();
            SFXManager.sFXManager.playSound(SFXManager.sFXManager.ballEntersDeathzoneSound);
        }
    }
}
