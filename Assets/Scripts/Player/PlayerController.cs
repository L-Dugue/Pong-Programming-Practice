using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Fields
    private float moveInput;

    // Serialized Fields
    [SerializeField] private float moveSpeed;
   
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
        Debug.Log(moveInput * moveSpeed);
    }

}
