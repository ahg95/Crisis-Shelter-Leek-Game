using UnityEngine;

public class Spawn : MonoBehaviour
{
    private void OnLevelWasLoaded(int level)
    {
        SetPosOnSceneChange.SpawnOnCurrentSpawnPoint();
    }
}
