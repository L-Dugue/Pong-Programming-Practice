using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Private Fields
    private Vector2 velOld = new Vector2(0, 0);
    private Vector2 velNew = Vector2.zero;
    private BoxCollisionArea collisionArea;

    // Serialized Fields
    [SerializeField] private float ballSpeed;

    // Public Properties
    public Vector2 VelocityOld
    {
        get { return velOld; }
        set { velOld = new Vector2(Mathf.Clamp(value.x, -10, 10), Mathf.Clamp(value.y, -10, 10)); }
    }

    // Events
    public delegate void OnPlayerScored();
    public static event OnPlayerScored OnPlayerOneScoredTriggered;
    public static event OnPlayerScored OnPlayerTwoScoredTriggered;

    private void Awake()
    {
        velOld.x = ballSpeed;
        AABBCollisionLogic.GatherAllCollisionBoxes();
        collisionArea = GetComponent<BoxCollisionArea>();
    }

    private void FixedUpdate()
    {
        MoveBall();
        if (transform.position.x >= 10)
        {
            OnPlayerTwoScoredTriggered?.Invoke();
        }
        else if (transform.position.x <= -10)
        {
            OnPlayerOneScoredTriggered?.Invoke();
        }
    }


    // Helper Methods
    private void MoveBall()
    {
        velOld = velOld.normalized; // Normalize Vector first before doing operations.

        velNew.x = Mathf.Clamp(velOld.x + ((ballSpeed * Mathf.Sign(velOld.x) * Time.deltaTime)), -0.2f, 0.2f);
        velNew.y = Mathf.Clamp(velOld.y, -0.15f, 0.15f);

        velOld = velNew; // Apply the changes made to VelNew to VelOld

        // Pos Calculation
        gameObject.transform.position = (Vector2)gameObject.transform.position + velNew;
        collisionArea.CenterOfCollisionBox = gameObject.transform.position;
    }
}

