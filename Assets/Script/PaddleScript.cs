using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    //Variable to store whether the paddle is left or right
    public paddle thisPaddle;

    //All these variable values have to be taken from GameManager
    private GameObject ballReference;
    private float paddleMovementSpeed;

    //These define the top and bottom limit for the paddle to move.
    private float topLimit;
    private float bottomLimit;

    //An enum to determine whether the paddle is left or right.
    public enum paddle
    {
        left,
        right
    };

    //Called at the start of the game.
    private void Start()
    {
        ballReference = GameManager.gameManager._ballRef;
        paddleMovementSpeed = GameManager.gameManager._paddleMovementSpeed;

        //Reducing the half of the length of the paddle and adding half of the side of the border cube
        topLimit = GameManager.top - transform.localScale.z * 0.5f;
        bottomLimit = GameManager.bottom + transform.localScale.z * 0.5f; 
        placePaddle();
    }

    //Responsible for placing the paddle correctly either on the Left side or the right side of the screen.
    private void placePaddle()
    {
        float xPos =0f, yPos = 0f, zPos = 0f;
        zPos = (topLimit + bottomLimit) / 2;
        if (thisPaddle == paddle.left)
        {
            xPos = GameManager.gameManager._paddleBottomLeftCorner.transform.position.x;
        }
        else if (thisPaddle == paddle.right)
        {
            xPos = GameManager.gameManager._paddleTopRightCorner.transform.position.x;
        }
        transform.position = new Vector3(xPos, yPos, zPos);
    }

    //Whenever any gameObject with a collider collides with the paddle, this function is called.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == ballReference)
        {
            //Since we are making the ball kinematic, this won't work.
            //ballReference.GetComponent<Rigidbody>().AddForce(Vector3.right * paddleForce * forceMultiplier);
        }
    }

    //Called every frame
    private void LateUpdate()
    {
        //If the game is not Playing, don't do anything
        if (GameManager.gameManager._currentGameState !=  GameManager.GameState.PLAYING)
        {
            return;
        }

        //Debug.Log(ballReference.GetComponent<Rigidbody>().velocity);

        bool moved = false;
        //-----------<<<<<INEFFICIENT CODE STARTS>>>>>>>------------------
        if (thisPaddle == paddle.left)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * paddleMovementSpeed * Time.deltaTime);
                moved = true;
            }
            else
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-Vector3.forward * paddleMovementSpeed * Time.deltaTime);
                moved = true;
            }
        }
        else if (thisPaddle == paddle.right)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * paddleMovementSpeed * Time.deltaTime);
                moved = true;
            }
            else
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(-Vector3.forward * paddleMovementSpeed * Time.deltaTime);
                moved = true;
            }
        }

        if (moved)
        {
            float newZ = transform.position.z;
            if (newZ > topLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, topLimit);
            }
            else if (newZ < bottomLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, bottomLimit);
            }
        }
        //-----------<<<<<INEFFICIENT CODE ENDS>>>>>>>--------------------

    }
}
