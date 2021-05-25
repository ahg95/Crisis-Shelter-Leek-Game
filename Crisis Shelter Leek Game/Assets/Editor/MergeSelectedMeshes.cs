using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MergeSelectedMeshes : EditorWindow
{
    // An O.G. Script

    [MenuItem("Tools/Merge Meshes/Merge Selected Meshes %m")]
    private static void CreateCombinedMeshAsset()
    {
        if (!Application.isPlaying)
        {

            AssetDatabase.CreateAsset(CombineMeshes(), "Assets/Meshes/CombinedMesh.asset");

            AssetDatabase.Refresh();
        }
    }   

    /// <summary>
    /// ONLY works nicely with meshes that share the same material!
    /// </summary>
    [MenuItem("Tools/Merge Meshes/Merge + Make Prefab Mesh %#m")]
    private static void CreateCombinedMeshAssetWithPrefabAndMaterial()
    {
        if (!Application.isPlaying)
        {
            // GameObject to make prefab out of
            GameObject go = new GameObject();
            MeshFilter filter = go.AddComponent<MeshFilter>();

            // make the combined mesh and make the combined mesh the mesh of the new prefab
            Mesh combinedMesh = CombineMeshes();
            filter.mesh = combinedMesh;

            // apply shared material to new mesh object
            MeshRenderer renderer = go.AddComponent<MeshRenderer>();
            Material sharedMaterial = Selection.activeGameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
            renderer.material = sharedMaterial;

            // create prefab asset of new object
            AssetDatabase.CreateAsset(combinedMesh, "Assets/CombinedMesh.asset");

            PrefabUtility.SaveAsPrefabAsset(go, "Assets/New Prefab.prefab");

            AssetDatabase.Refresh();

            DestroyImmediate(go);
        }
    }

    /// <summary>
    /// Get an array of meshes of all objects which are selected in the scene,
    /// Merge said meshes through a tool made by Unity,
    /// Import the new mesh into the asset folder.
    /// </summary>
    private static Mesh CombineMeshes()
    {
        List<MeshFilter> selectedMeshesToCombine = new List<MeshFilter>();

        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Transform selectedObject = Selection.gameObjects[i].transform;
            
            // For this object and each of its children
            foreach (MeshFilter meshInTransform in selectedObject.GetComponentsInChildren<MeshFilter>())
            {
                selectedMeshesToCombine.Add(meshInTransform);
            }
        }

        GameObject average = GroupObjects.GetAveragePositionObject(Selection.gameObjects);

        foreach (GameObject go in Selection.gameObjects)
        {
            go.transform.SetParent(average.transform);
        }
        Vector3 originalPosition = average.transform.position;

        average.transform.position = Vector3.zero;

        CombineInstance[] combineInstanceArray = new CombineInstance[selectedMeshesToCombine.Count];
        for (int i = 0; i < selectedMeshesToCombine.Count; i++)
        {
            Mesh selectedMeshToCombine = selectedMeshesToCombine[i].sharedMesh;
            combineInstanceArray[i].mesh = selectedMeshToCombine;
            combineInstanceArray[i].transform = selectedMeshesToCombine[i].transform.localToWorldMatrix;
        }

        average.transform.position = originalPosition;

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstanceArray);
        combinedMesh.name = "Combined New Mesh";
        combinedMesh.RecalculateNormals();
        combinedMesh.RecalculateTangents();
        combinedMesh.RecalculateBounds();
        combinedMesh.Optimize();
        combinedMesh.OptimizeIndexBuffers();
        combinedMesh.OptimizeReorderVertexBuffer();
        Unwrapping.GenerateSecondaryUVSet(combinedMesh);
        Unwrapping.GeneratePerTriangleUV(combinedMesh);

        return combinedMesh;
    }

    [MenuItem("Tools/Merge Meshes/Test %t")]
    private static void TestVoid()
    {
        Debug.Log(Application.dataPath + "/Meshes");
    }
}