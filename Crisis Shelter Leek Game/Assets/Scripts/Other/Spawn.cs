using UnityEngine;

public class Spawn : MonoBehaviour
{
    private void Awake()
    {
        SetPosOnSceneChange.SpawnOnCurrentSpawnPoint();
    }
}
