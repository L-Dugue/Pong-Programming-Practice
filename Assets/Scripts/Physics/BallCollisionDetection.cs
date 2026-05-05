using UnityEngine;

public class BallCollisionDetection : MonoBehaviour
{
    // Private Fields
    private BoxCollisionArea _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollisionArea>();
    }

    private void FixedUpdate()
    {
        foreach (BoxCollisionArea boxB in AABBCollisionLogic.BoxCollisionAreas)
        {
            if (boxB.Collider.Intersects(_collider.Collider))
            {
                AABBCollisionLogic.PositionCorrection(_collider, boxB, true);
                Debug.Log(boxB.gameObject.name);
            }
        }
        
    }
   
}
