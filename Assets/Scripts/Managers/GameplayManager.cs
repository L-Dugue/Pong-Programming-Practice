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


    // Events
    public delegate void OnUpdateStats(int playerOneScore, int playerTwoScore);
    public static event OnUpdateStats OnUpdateStatsEvent;


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


    // Private Methods
    private void PlayerOneScored()
    {
        playerOneScore++;
        ResetBall();
        Debug.Log($"PLAYER ONE HAS:{playerOneScore} POINTS");
        OnUpdateStatsEvent?.Invoke( playerOneScore, playerTwoScore );
    }

    private void PlayerTwoScored()
    {
        playerTwoScore++;
        ResetBall();
        Debug.Log($"PLAYER TWO HAS:{playerTwoScore} POINTS");
        OnUpdateStatsEvent?.Invoke(playerOneScore, playerTwoScore);
    }

    private void ResetBall()
    {
        ball.transform.position = Vector2.zero;
        ball.GetComponent<BallMovement>().VelocityOld = new Vector2(ball.GetComponent<BallMovement>().VelocityOld.x * -1, 0);
    }

}
