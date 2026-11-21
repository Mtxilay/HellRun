using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    private LevelManager levelManager;
    private Transform thisMonster;
    private float cameraBottom;

    void Awake()
    {
        levelManager = this.gameObject.GetComponentInParent<LevelManager>();
        thisMonster = this.gameObject.GetComponent<Transform>();
        cameraBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
    }

    void OnEnable()
    {
        levelManager.moveMosters += MoveMonster;
    }
     void OnDisable()
    {
        levelManager.moveMosters -= MoveMonster;
    }
    public void MoveMonster()
    {

            thisMonster.transform.position = new Vector3(this.transform.position.x,  thisMonster.transform.position.y - levelManager.currentSpeed * Time.deltaTime,  thisMonster.transform.position.z);
            if ( thisMonster.transform.position.y < cameraBottom)
            {
                Destroy(this.gameObject);
            }
        }
    }

