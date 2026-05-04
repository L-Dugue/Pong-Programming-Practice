using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Private Fields
    private Vector2 velOld = new Vector2(0, 0);
    private Vector2 velNew = Vector2.zero;

    // Serialized Fields
    [SerializeField] private float ballSpeed;

    // Public Properties
    public Vector2 VelocityOld
    {
        get { return velOld; }
        set { velOld = new Vector2(Mathf.Clamp(value.x, -10, 10), Mathf.Clamp(value.y, -10, 10)); }
    }

    private void Awake()
    {
        velOld.x = ballSpeed;
        AABBCollisionLogic.GatherAllCollisionBoxes();
    }

    private void FixedUpdate()
    {
        MoveBall();

    }


    // Helper Methods
    private void MoveBall()
    {
        velNew.x = velOld.x + (ballSpeed * Time.deltaTime);
        velNew.y = velOld.y;
        velOld = velNew; // Apply the changes made to VelNew to VelOld

        // Pos Calculation
        gameObject.transform.position = (Vector2)gameObject.transform.position + velNew;
    }
}

