using UnityEngine;

public class ChangeSpawnPoint : MonoBehaviour
{
    [SerializeField] private SpawnPoint._SpawnPoint spawnLocation;
    public void ChangePlayerSpawnPoint()
    {
        SetPosOnSceneChange.SetSpawnPoint(spawnLocation);
    }
}
