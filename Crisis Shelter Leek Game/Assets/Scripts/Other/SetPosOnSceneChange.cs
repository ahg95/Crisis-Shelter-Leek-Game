using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosOnSceneChange : MonoBehaviour
{
    public static SetPosOnSceneChange instance = null;

    public GameObject player;
    public GameObject[] positionPoint;

    public int doorId;
    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(positionPoint.Length == 0)
        {
            positionPoint = GameObject.FindGameObjectsWithTag("PositionPoint");
        }
    }

    private void OnLevelWasLoaded()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        positionPoint = GameObject.FindGameObjectsWithTag("PositionPoint");

        if (player != null && positionPoint.Length != 0)
        {
            for (int i = 0; i < positionPoint.Length; i++)
            {
                if (positionPoint[i].transform.parent.GetComponent<Door>().positionId == doorId)
                {
                    player.transform.position = positionPoint[i].transform.position;
                    player.transform.rotation = Quaternion.LookRotation(positionPoint[i].transform.forward);//make the player look in the same direction as the position point
                }
            }
        }
    }
    public void SetPositionId(int passedPositionId)
    {
        instance.doorId = passedPositionId;
    }
}
