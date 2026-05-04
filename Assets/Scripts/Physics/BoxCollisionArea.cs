using UnityEngine;

public class BoxCollisionArea : MonoBehaviour
{
    // Private Fields
    private Bounds _bounds = new Bounds();

    // Serialized Fields

    // Defining Bounds
    [SerializeField] private Vector2 min; // Defining Min
    [SerializeField] private Vector2 max; // Defining Max

    // Properties
    public Bounds collider {  get { return _bounds; } }

    private void Awake()
    {
        _bounds.center = transform.position;
        _bounds.min = min;
        _bounds.max = max;
    }

    // Draw shape of bounds
    void OnDrawGizmosSelected()
    {
        _bounds.min = min;
        _bounds.max = max;
        _bounds.center = transform.position;


        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
        Gizmos.color = Color.hotPink;
    }

}
