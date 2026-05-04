using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AABBCollisionLogic
{
    private static float E = 1f;

    // Public Static Methods
    public static void PositionCorrection(BoxCollisionArea BoxA, BoxCollisionArea BoxB)
    {
        
        if(YOverlap(BoxA.collider, BoxB.collider) < XOverlap(BoxA.collider, BoxB.collider)) // OverLap on the Y
        {
            BoxA.transform.position = new Vector2(BoxA.transform.position.x, BoxA.transform.position.y + YOverlap(BoxA.collider, BoxB.collider));
            BounceBack(BoxA);
        }
        else // OverLap on the X
        {
            if ( 1 == 1) // If the Tangent is negative, add MPD
            {
                BoxA.transform.position = new Vector2(BoxA.transform.position.x + XOverlap(BoxA.collider, BoxB.collider), BoxA.transform.position.y);
                BounceBack(BoxA);
            }
            else // Tangent is positive, substract MPD
            {
                BoxA.transform.position = new Vector2(BoxA.transform.position.x - XOverlap(BoxA.collider, BoxB.collider), BoxA.transform.position.y);
                BounceBack(BoxA);
            }
        } 
        
    }

    // Private Helper Methods
    private static float XOverlap(Bounds BoxA, Bounds BoxB) => Mathf.Min(BoxA.max.x, BoxB.max.x) - Mathf.Max(BoxA.min.x, BoxB.min.x);
    private static float YOverlap(Bounds BoxA, Bounds BoxB) => Mathf.Min(BoxA.max.y, BoxB.max.y) - Mathf.Max(BoxA.min.y, BoxB.min.y);

    private static void BounceBack(BoxCollisionArea BoxA)
    {

        // Breaking down the Vectors to the VectorNormal and VectorTangent

        Vector2 VectorNormal = new Vector2(0, BoxA.gameObject.GetComponent<BallMovement>().velocityOld.y);
        Vector2 VectorTangent = new Vector2(BoxA.gameObject.GetComponent<BallMovement>().velocityOld.x, 0);

        // Caclulate the Coeficient of Resitution/Rebound
        Vector2 VectorPrime = -E * VectorNormal;
   
        // Combination of the Vector Prime and VectorWithFriction, apply them to the veclocityOld of PhysicsMovement
        BoxA.GetComponent<BallMovement>().velocityOld = VectorPrime;
    }
}
