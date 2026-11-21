
using System.Collections.Generic;
using UnityEngine;

public class GroundMover : MonoBehaviour
{
    private LevelManager levelManager;
    private float cameraBottom;
    private float tileHeight;

    private List<Transform> tileList;

    private void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();

        cameraBottom = Camera.main.ScreenToWorldPoint(
            new Vector3(0, 0, Camera.main.nearClipPlane)
        ).y;

        tileList = new List<Transform>();

        foreach (Transform child in transform)
            tileList.Add(child);

        tileHeight = tileList[0]
            .GetComponent<SpriteRenderer>()
            .bounds.size.y;
    }

    public void MoveTiles()
    {
        float movement = levelManager.currentSpeed * Time.deltaTime;

        foreach (Transform tile in tileList)
        {
            tile.position += Vector3.down * movement;
        }

        foreach (Transform tile in tileList)
        {
            if (tile.position.y + tileHeight / 2f >= cameraBottom)
                continue;

            float highestY = float.NegativeInfinity;

            foreach (Transform otherTile in tileList)
            {
                if (otherTile != tile)
                    highestY = Mathf.Max(highestY, otherTile.position.y);
            }

            tile.position = new Vector3(
                tile.position.x,
                highestY + tileHeight,
                tile.position.z
            );
        }
    }
}
