using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    // Singleton Variable
    public static GameplayManager Instance = null;


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
            Instance = this;
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
        OnUpdateStatsEvent?.Invoke( playerOneScore, playerTwoScore );
        AudioManager.Instance.PlayScoreAudio();
    }

    private void PlayerTwoScored()
    {
        playerTwoScore++;
        ResetBall();
        OnUpdateStatsEvent?.Invoke(playerOneScore, playerTwoScore);
        AudioManager.Instance.PlayScoreAudio();
    }

    private void ResetBall()
    {
        ball.transform.position = Vector2.zero;
        ball.GetComponent<BallMovement>().VelocityOld = new Vector2(ball.GetComponent<BallMovement>().VelocityOld.x * -1, 0);
    }

}
