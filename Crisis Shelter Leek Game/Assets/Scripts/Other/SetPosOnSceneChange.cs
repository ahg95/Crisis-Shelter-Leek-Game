using UnityEngine;
public static class SetPosOnSceneChange
{
    public static SpawnPoint._SpawnPoint currentSpawnPoint = SpawnPoint._SpawnPoint.entranceWender;

    public static void SpawnOnCurrentSpawnPoint()
    {
        SpawnPoint currentSpawnPoint = GetCurrentSpawnPoint();

        SpawnPlayerOnPoint(currentSpawnPoint);
    }

    private static SpawnPoint GetCurrentSpawnPoint()
    {
        SpawnPoint[] spawnPointsInScene = GameObject.FindObjectsOfType<SpawnPoint>();

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
    public static void SpawnPlayerOnPoint(SpawnPoint point)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && point != null)
        {
            player.transform.SetPositionAndRotation(point.transform.position, Quaternion.LookRotation(point.transform.forward));
            player.transform.position = point.transform.position;

            Transform camTransform = Camera.main.transform;
            Vector3 cameraEulerRot = new Vector3(6, camTransform.rotation.eulerAngles.y, camTransform.rotation.eulerAngles.z);
            camTransform.rotation = Quaternion.Euler(cameraEulerRot);
        }
    }

    public static void SetSpawnPoint(SpawnPoint._SpawnPoint spawnPoint)
    {
        currentSpawnPoint = spawnPoint;
    }
}
