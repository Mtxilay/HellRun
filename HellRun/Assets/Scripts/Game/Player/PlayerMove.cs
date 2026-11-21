using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Transform playerTransform;
    private int LeftMovement = -1;
    private int RightMovement = 1;

    private int currentLane= 1; // 0 = left, 1 = middle, 2 = right

    enum MovementDirection
    {
        Left,
        Right
    }
    void Start()
    {
       playerTransform = this.gameObject.GetComponent<Transform>(); 
    }

    // Update is called once per frame
    public void OnSwipeLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            UpdatePlayerPosition(MovementDirection.Left);
        }
    }

    public void OnSwipeRight()
    {
        if (currentLane < 2)
        {
            currentLane++;
            UpdatePlayerPosition(MovementDirection.Right);
        }
    }

    private void UpdatePlayerPosition(MovementDirection direction)
    {
        if(direction == MovementDirection.Left)
        {
            playerTransform.position =new Vector3(playerTransform.position.x + LeftMovement, playerTransform.position.y, playerTransform.position.z);
        }
        else if(direction == MovementDirection.Right)
        {
            playerTransform.position =new Vector3(playerTransform.position.x + RightMovement, playerTransform.position.y, playerTransform.position.z);
        }
    }
}
