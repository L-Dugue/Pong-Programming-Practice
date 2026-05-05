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

    private void Awake()
    {
        velOld.x = ballSpeed;
        AABBCollisionLogic.GatherAllCollisionBoxes();
        collisionArea = GetComponent<BoxCollisionArea>();
    }

    private void FixedUpdate()
    {
        MoveBall();
    }


    // Helper Methods
    private void MoveBall()
    {
        velNew.x = Mathf.Clamp(velOld.x + ((ballSpeed * Mathf.Sign(velOld.x) * Time.deltaTime)), -0.2f, 0.2f);
        velNew.y = velOld.y;
        velOld = velNew; // Apply the changes made to VelNew to VelOld

        // Pos Calculation
        gameObject.transform.position = (Vector2)gameObject.transform.position + velNew;
        collisionArea.CenterOfCollisionBox = gameObject.transform.position;
        Debug.Log(velNew);
        
    }
}

