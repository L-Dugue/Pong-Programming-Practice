using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AABBCollisionLogic : MonoBehaviour
{
    // Private Fields
    private static float E = 1f;
    private static List<BoxCollisionArea> boxCollisionAreas = new List<BoxCollisionArea>();

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
            BoxA.transform.position = new Vector2(BoxA.transform.position.x, BoxA.transform.position.y + YOverlap(BoxA.Collider, BoxB.Collider));
            if (bouceBack) { BounceBackY(BoxA); }

        }
        else // OverLap on the X
        {
            if (BoxA.GetComponent<BallMovement>().VelocityOld.normalized.x < 0) // If the Tangent is negative, add MPD (1 == 1 is a placeholder)
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

    private static void BounceBackY(BoxCollisionArea BoxA)
    {

        // Breaking down the Vectors to the VectorNormal and VectorTangent

        Vector2 Reflection = new Vector2(BoxA.gameObject.GetComponent<BallMovement>().VelocityOld.x, BoxA.gameObject.GetComponent<BallMovement>().VelocityOld.y * -1);

        // Combination of the Vector Prime and VectorWithFriction, apply them to the veclocityOld of PhysicsMovement
        BoxA.GetComponent<BallMovement>().VelocityOld = Reflection;

        Debug.Log(Reflection);
    }

    private static void BounceBackFromPlayer(BoxCollisionArea BoxA, PlayerController player)
    {
        //string result = (time < 18) ? "Good day." : "Good evening.";

        float reflectDir = 0f;

       if(player.MoveInput >= 0.5)
        {
            reflectDir = 0.15f;
        }
       else if(player.MoveInput <= -0.5)
        {
            reflectDir = -0.15f;
        }
        else
        {
            reflectDir = 0f;
        }


  
        Vector2 reflection = new Vector2(BoxA.gameObject.GetComponent<BallMovement>().VelocityOld.x, reflectDir) * -1;

        // Combination of the Vector Prime and VectorWithFriction, apply them to the veclocityOld of PhysicsMovement
        BoxA.GetComponent<BallMovement>().VelocityOld = reflection;
    }




}
