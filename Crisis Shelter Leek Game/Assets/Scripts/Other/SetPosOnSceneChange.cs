using UnityEngine;
public class SetPosOnSceneChange : MonoBehaviour
{
    public static SetPosOnSceneChange instance = null;

    public GameObject player;
    public SpawnPoint[] positionPoint;

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

        if(positionPoint.Length == 0)
        {
            positionPoint = FindObjectsOfType<SpawnPoint>();
        }
    }

    private void OnLevelWasLoaded()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        positionPoint = FindObjectsOfType<SpawnPoint>();

        if (player != null && positionPoint.Length != 0)
        {
            for (int i = 0; i < positionPoint.Length; i++)
            {
                if (positionPoint[i].thisSpawnPoint == currentSpawnPoint)
                {
                    player.transform.position = positionPoint[i].transform.position;
                    player.transform.rotation = Quaternion.LookRotation(positionPoint[i].transform.forward);//make the player look in the same direction as the position point
                }
            }
        }
    }
    public void SetPositionId(SpawnPoint._SpawnPoint spawnPoint)
    {
        instance.currentSpawnPoint = spawnPoint;
    }
}
