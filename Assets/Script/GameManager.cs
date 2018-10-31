using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Game Manager contains all the settings for the game. It also contains references for other scripts to use. It's a very nice script.
 * GameManager also received the que to start the game after the story line sequence at the beginning.
 * To change game audio, go to SFXManager and BGMManager respectively.
 */
 
public class GameManager : MonoBehaviour {
    
    
    // Singleton for the class.
    public static GameManager gameManager;

    //Gameobject references
    public GameObject _ballRef;
    public GameObject ballsParent;

    //configuration variables that can be edited in editor
    public float _paddleForce;
    public float _paddleMovementSpeed;
    public float _ballSpeed;
    public int _scoreValue;
    public int maxScore;

    //Found this great way of defining boundaries on Youtube.
    //Instead of using 4 variables, place two invisible cubes on two extremes and get their X and Y positions.
    public GameObject _paddleTopRightCorner;
    public GameObject _paddleBottomLeftCorner;

    //GameObject reference for other scripts.
    public PaddleScript paddle;
    public BallScript ball;

    //Some static variables for other scripts to use.
    public static float top;
    public static float bottom;
    public static Vector3 initialBallDirection;

    //Enum to check the state of the game.
    public enum GameState
    {
        CINEMATIC,
        PLAYING,
        ENDED,
        PAUSED
    };

    //This states teh current game state. Needs to be public to be used by other scripts.
    public GameState _currentGameState;

    public int currentBallCount;
    public int maxBallCount;

    public float ballSpeedIncrease;
    public float ballMaxSpeed;

    private void Awake()
    {
        //Compulsory code for singleton.
        //Very important that you don't accidently create another GameManager. It would be really bad.
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _currentGameState = GameState.CINEMATIC;
        currentBallCount = 0;
    }

    //In start function, instantiate both the paddles
    private void Start()
    {
        initialBallDirection = new Vector3(1, 0, 1);

        PaddleScript paddleLeft = Instantiate(paddle);
        PaddleScript paddleRight = Instantiate(paddle);

        //Assign whether the paddle is left or right.
        //When the paddle is instantiated, it checks whether it's left or right and aligns itself accordingly.
        //This feature was accidentally conceieved.
        paddleLeft.thisPaddle = PaddleScript.paddle.left;
        paddleRight.thisPaddle = PaddleScript.paddle.right;


        //The boundaries are calculated considering the width of the top boundary.
        //Careful while changing it.
        top = _paddleTopRightCorner.transform.position.z + _paddleTopRightCorner.transform.localScale.z * 0.5f;
        bottom = _paddleBottomLeftCorner.transform.position.z - _paddleBottomLeftCorner.transform.localScale.z * 0.5f;

        //Markers might cause issues later and we might totally forget about them.
        //It would be a good practice to delete them or set them inactive.
        disableMarkers();
    }

    //Make the markers invisible.
    //Disable the colliders also.
    private void disableMarkers()
    {
        _paddleBottomLeftCorner.GetComponent<MeshRenderer>().enabled = false;
        _paddleBottomLeftCorner.GetComponent<BoxCollider>().enabled = false;

        _paddleTopRightCorner.GetComponent<BoxCollider>().enabled = false;
        _paddleTopRightCorner.GetComponent<MeshRenderer>().enabled = false;

    }

    private void Update()
    {
        //If the game is not Playing, don't do anything
        if (_currentGameState != GameState.PLAYING)
        {
            return;
        }
    }

    //Spawns a new ball everytime this is called.
    //Also fires the ball in opposite direction everytime.
    //This is the reason behind the name of the project.
    public void spawnNewBall()
    {
        if (_currentGameState != GameState.PLAYING)
        {
            return;
        }
        if (currentBallCount < maxBallCount)
        {
            BallScript newBall = Instantiate(ball);

            //Randomize the start position of the new ball
            Vector3 position = newBall.transform.position;
            position.z = Random.Range(top, bottom);
            newBall.transform.position = position;

            currentBallCount++;
            //Assign a direction and change direction for the next ball
            newBall.direction = initialBallDirection;
            initialBallDirection.x = -initialBallDirection.x;

            //SFXManager.sFXManager.playSound(SFXManager.sFXManager.ballSpawnSound);

            //Make the paddle speed a little faster for the next time
            if (_ballSpeed < ballMaxSpeed)
            {
                _ballSpeed = _ballSpeed + ballSpeedIncrease;
            }
        }
    }

    //This function si called from UIManager once the UI part is done.
    public void startGameCall()
    {
        _currentGameState = GameState.PLAYING;
        StartCoroutine(startGame());
    }

    IEnumerator startGame()
    {
        float ballSpawnInterval = 5.0f;
        for(int i = 0; i < maxBallCount; i++)
        {
            spawnNewBall();
            yield return new WaitForSeconds(ballSpawnInterval);
        }
    }

    //Call this function at the end of the game to remove all balls from the game!!
    public void deleteAllBalls()
    {
        ballsParent = GameObject.Find("BallsParent");
        
        for (int i = 0; i < ballsParent.transform.childCount; i++)
        {
            GameObject child = ballsParent.transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }

    //Restart the first scene.
    //Only one scene
    public void restartGame()
    {
        Debug.Log("Restart");
        SceneManager.LoadSceneAsync(0);
    }
}