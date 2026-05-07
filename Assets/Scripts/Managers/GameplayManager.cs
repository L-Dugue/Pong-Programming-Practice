using UnityEngine;

public class GameplayManager : MonoBehaviour
{   
    // Singleton Variable
    public static GameObject Instance = null;


    // Storing Balls in Storage
    [SerializeField] private GameObject[] Balls = new GameObject[2]; // Array of Balls


    private void Awake()
    {
        // Singleton Declaration
        if(Instance == null)
        {
            Instance = this.gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        // Place a ball in the scene
        Balls[0].SetActive(true);
        Balls[0].transform.position = Vector2.zero;

    }

    
}
