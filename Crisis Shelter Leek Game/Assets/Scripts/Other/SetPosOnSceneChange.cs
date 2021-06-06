using UnityEngine;
public class SetPosOnSceneChange : MonoBehaviour
{
    public static SetPosOnSceneChange instance = null;

    public GameObject player;

    public SpawnPoint._SpawnPoint currentSpawnPoint;
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
    }

    private void OnLevelWasLoaded()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        SpawnPoint[] spawnPointsInScene = FindObjectsOfType<SpawnPoint>();

        if (player != null && spawnPointsInScene.Length != 0)
        {
            for (int i = 0; i < spawnPointsInScene.Length; i++)
            {
                if (spawnPointsInScene[i].thisSpawnPoint == currentSpawnPoint)
                {
                    player.transform.position = spawnPointsInScene[i].transform.position;
                    player.transform.rotation = Quaternion.LookRotation(spawnPointsInScene[i].transform.forward);//make the player look in the same direction as the position point
                }
            }
        }
    }
    public void SetPositionId(SpawnPoint._SpawnPoint spawnPoint)
    {
        instance.currentSpawnPoint = spawnPoint;
    }
}
