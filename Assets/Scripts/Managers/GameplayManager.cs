using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    // Singleton Variable
    public static GameObject Instance = null;


    // Storing Balls in Storage
    [SerializeField] private GameObject ball;



    // Score Variables
    private int playerOneScore = 0;
    private int playerTwoScore = 0;


    private void Awake()
    {
        // Singleton Declaration
        if (Instance == null)
        {
            Instance = this.gameObject;
        }
        else
        {
            Destroy(gameObject);
        }


        BallMovement.OnPlayerOneScoredTriggered += PlayerOneScored;
        BallMovement.OnPlayerTwoScoredTriggered += PlayerTwoScored;
    }

    private void PlayerOneScored()
    {
        playerOneScore++;
        UpdateBall();
        Debug.Log($"PLAYER ONE HAS:{playerOneScore} POINTS");

    }
    private void PlayerTwoScored()
    {
        playerTwoScore++;
        UpdateBall();
        Debug.Log($"PLAYER TWO HAS:{playerTwoScore} POINTS");

    }

    private void UpdateBall()
    {
        ball.transform.position = Vector2.zero;
        ball.GetComponent<BallMovement>().VelocityOld = new Vector2(ball.GetComponent<BallMovement>().VelocityOld.x * -1, 0);
    }

}
