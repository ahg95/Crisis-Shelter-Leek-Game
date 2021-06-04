﻿using UnityEngine;

[CreateAssetMenu(fileName = "Text Array", menuName = "Text Array")]
public class TransitionTextAssociatedToTask : ScriptableObject
{
    public Task associatedTask;
    [TextArea(0, 1)]
    public string[] textArray;
    [Space(10)]
    [Tooltip("Type it exactly as you see the name in the Build Settings")]
    public string SceneToTransferTo;
    [Tooltip("If you need the player to spawn at a specific spawnpoint in the room.")]
    public int doorID;
}
