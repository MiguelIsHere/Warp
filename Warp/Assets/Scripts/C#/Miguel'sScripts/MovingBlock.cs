using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public GameObject movingBlock;
    public Transform startPoint, endPoint; // Set reference to start and end point of moving Block

    public float moveSpeed = 5f; // Speed at which the block will move

    public bool isActive = true; // Used to control blocks which only move if the corresponding switch is on. By default, always active aka always moves
    public bool isMovingToEnd = true; // Used to swap between moving towards endpoint and startpoint
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            // If block is not at endPoint and is moving there, move towards endPoint
            if (movingBlock.transform.position != endPoint.position && isMovingToEnd) 
            {
                movingBlock.transform.position = Vector3.MoveTowards(movingBlock.transform.position, endPoint.position, moveSpeed * Time.deltaTime);

                if (movingBlock.transform.position == endPoint.position) // Upon reaching endPoint, set isMovingToEnd to false and start moving back
                {
                    isMovingToEnd = false;
                }
            }
            else if (movingBlock.transform.position != startPoint.transform.position && !isMovingToEnd)
            {
                movingBlock.transform.position = Vector3.MoveTowards(movingBlock.transform.position, startPoint.position, moveSpeed * Time.deltaTime);

                if (movingBlock.transform.position == startPoint.position)
                {
                    isMovingToEnd = true;
                }
            }
        }
    }
}
