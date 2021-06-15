using UnityEngine;
public class SetPosOnSceneChange : MonoBehaviour
{
    public static SetPosOnSceneChange instance = null;

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
    }

    private void OnLevelWasLoaded()
    {
        SpawnOnCurrentSpawnPoint();
    }

    public void SpawnOnCurrentSpawnPoint()
    {
        SpawnPoint currentSpawnPoint = GetCurrentSpawnPoint();

        SpawnPlayerOnPoint(currentSpawnPoint);
    }

    private SpawnPoint GetCurrentSpawnPoint()
    {
        SpawnPoint[] spawnPointsInScene = FindObjectsOfType<SpawnPoint>();

        if (spawnPointsInScene.Length != 0)
        {
            for (int i = 0; i < spawnPointsInScene.Length; i++)
            {
                if (spawnPointsInScene[i].thisSpawnPoint == currentSpawnPoint)
                {
                    return spawnPointsInScene[i];
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Set the position and rotation of the player to the given point.
    /// </summary>
    /// <param name="point"></param>
    public void SpawnPlayerOnPoint(SpawnPoint point)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && point != null)
        {
            Debug.Break();
            player.transform.SetPositionAndRotation(point.transform.position, Quaternion.LookRotation(point.transform.forward));
            player.transform.position = point.transform.position;

            Transform camTransform = Camera.main.transform;
            Vector3 cameraEulerRot = new Vector3(6, camTransform.rotation.eulerAngles.y, camTransform.rotation.eulerAngles.z);
            camTransform.rotation = Quaternion.Euler(cameraEulerRot);
        }
    }

    public void SetSpawnPoint(SpawnPoint._SpawnPoint spawnPoint)
    {
        instance.currentSpawnPoint = spawnPoint;
        Debug.Break();
    }
}
