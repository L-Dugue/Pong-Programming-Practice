using TMPro;
using UnityEngine;

public class PlayerScores : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] TextMeshProUGUI playerOneScoreText;
    [SerializeField] TextMeshProUGUI playerTwoScoreText;

    private void Awake()
    {
        GameplayManager.OnUpdateStatsEvent += UpdateScores;
    }

    // Private Method
    private void UpdateScores(int playerOneScore, int playerTwoScore)
    {
        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
    }
}
