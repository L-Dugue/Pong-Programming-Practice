using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public abstract class AABBCollisionLogic : MonoBehaviour
{
    // Private Fields
    private static float E = 1f;
    private static List<BoxCollisionArea> boxCollisionAreas = new List<BoxCollisionArea>();
    private static bool bouncedLastFrame = false;

    // Public Properties
    public static List<BoxCollisionArea> BoxCollisionAreas { get { return boxCollisionAreas; }}


    public static void GatherAllCollisionBoxes()
    {
        // Ensures that the Ball's collision area is NOT included in the list.
        var tempBoxContainer = FindObjectsByType<BoxCollisionArea>(FindObjectsSortMode.None);
        foreach (var box in tempBoxContainer)
        {
            if (box.GetComponent<BallMovement>() != null)
            {
                continue;
            }

            boxCollisionAreas.Add(box);
        }

        Debug.Log(BoxCollisionAreas.Count);
    }
        
    


    // Public Static Methods
    public static void PositionCorrection(BoxCollisionArea BoxA, BoxCollisionArea BoxB, bool bouceBack)
    {

        if (YOverlap(BoxA.Collider, BoxB.Collider) < XOverlap(BoxA.Collider, BoxB.Collider)) // OverLap on the Y
        {
            if(Mathf.Sign(BoxA.transform.position.y) == 1)
            {
                BoxA.transform.position = new Vector2(BoxA.transform.position.x, BoxA.transform.position.y - YOverlap(BoxA.Collider, BoxB.Collider));
            }
            else
            {
                BoxA.transform.position = new Vector2(BoxA.transform.position.x, BoxA.transform.position.y + YOverlap(BoxA.Collider, BoxB.Collider));
            }
           
            if (bouceBack) { BounceBackFromWall(BoxA); }


        }
        else // OverLap on the X
        {
            if (BoxB.GetComponent<PlayerController>() == null) { return; } // Make sure not to return if the X overlap doesn't involve the player

            if (BoxA.GetComponent<BallMovement>().VelocityOld.normalized.x < 0) // If the Tangent is negative, add MPD
            {
                BoxA.transform.position = new Vector2(BoxA.transform.position.x + XOverlap(BoxA.Collider, BoxB.Collider), BoxA.transform.position.y);
            }
            else // Tangent is positive, substract MPD
            {
                BoxA.transform.position = new Vector2(BoxA.transform.position.x - XOverlap(BoxA.Collider, BoxB.Collider), BoxA.transform.position.y);
            }

            if (bouceBack) { BounceBackFromPlayer(BoxA, BoxB.gameObject.GetComponent<PlayerController>()); }
        }
    }

    // Private Helper Methods
    private static float XOverlap(Bounds BoxA, Bounds BoxB) => Mathf.Min(BoxA.max.x, BoxB.max.x) - Mathf.Max(BoxA.min.x, BoxB.min.x);
    private static float YOverlap(Bounds BoxA, Bounds BoxB) => Mathf.Min(BoxA.max.y, BoxB.max.y) - Mathf.Max(BoxA.min.y, BoxB.min.y);

    private static void BounceBackFromWall(BoxCollisionArea BoxA)
    {
        Vector2 Reflection = new Vector2(BoxA.gameObject.GetComponent<BallMovement>().VelocityOld.x, (BoxA.gameObject.GetComponent<BallMovement>().VelocityOld.y * 0.7f) * -1);
        BoxA.GetComponent<BallMovement>().VelocityOld = Reflection;
    }

    private static void BounceBackFromPlayer(BoxCollisionArea BoxA, PlayerController player)
    {
        Vector2 reflection = new Vector2(BoxA.gameObject.GetComponent<BallMovement>().VelocityOld.x, player.MoveInput) * -1;
        BoxA.GetComponent<BallMovement>().VelocityOld = reflection;
    }




}
