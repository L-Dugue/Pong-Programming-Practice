using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Fields
    private float moveInput;
    private BoxCollisionArea collisionArea;

    // Serialized Fields
    [SerializeField] private float moveSpeed;

    // Properties
    public float MoveInput { get { return moveInput; } }
    
    private void Awake()
    {
        collisionArea = GetComponent<BoxCollisionArea>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }


    // Input System Methods
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().y;
    }

    private void MovePlayer()
    {
        float clampedValue = math.clamp((moveInput * moveSpeed * Time.deltaTime) + transform.position.y, -3.5f, 3.5f);
        transform.position = new Vector2(transform.position.x, clampedValue);
        collisionArea.CenterOfCollisionBox = transform.position;
    }



}
