using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    private float speed;
    private float top;
    private float bottom;

    private float radius;

    public  Vector3 direction;

    public GameObject ballsParent;

	// Use this for initialization
	void Start () {
        //My Unity version had a bug where it won't let me drag and drop, hence this.
        if (ballsParent == null)
        {
            ballsParent = GameObject.Find("BallsParent");
        }
        speed = GameManager.gameManager._ballSpeed;
        radius = gameObject.transform.localScale.x * 0.5f;
        top = GameManager.top;
        bottom = GameManager.bottom;
        transform.parent = ballsParent.transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);

        //if ball collides with top or bottom boundary
        if ((transform.position.z > (top - radius) && direction.z > 0) || (transform.position.z < (bottom + radius) && direction.z < 0))
        {
            direction.z = direction.z * -1;
            SFXManager.sFXManager.playSound(SFXManager.sFXManager.ballStrikesBoundarySound);
        }
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("paddle"))
        {
            direction.x = -direction.x;
            SFXManager.sFXManager.playSound(SFXManager.sFXManager.ballStrikesPaddleSound);
        }
        if (other.gameObject.CompareTag("ball"))
        {
            direction = direction * -1;
            SFXManager.sFXManager.playSound(SFXManager.sFXManager.ballStrikesBallSound);
        }
    }
}
