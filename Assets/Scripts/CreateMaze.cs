using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class CreateMaze : MonoBehaviour
{
#if UNITY_EDITOR
    public Transform emptyObject;
    public GameObject cubeMesh;
    [Range(1, 20)]
    public int row = 5;
    [Range(1, 20)]
    public int column = 8;
    public float xScale = 1.75f;
    public float yScale = 0.5f;
    public float zScale = 1.75f;

    public void CreateMeshes()
    {
        for (int x = 0; x < row; x++)
            for (int y = 0; y < column; y++)
            {

                Transform cube = Instantiate(cubeMesh).transform;
                cube.parent = emptyObject.transform;
                cube.position = emptyObject.position + new Vector3(x * xScale, yScale, y * zScale);
                cube.localScale = new Vector3(xScale, yScale, zScale);
            }
        //emptyObject.transform.position = new Vector3(0, 0, 0);
        //emptyObject.transform.rotation = Quaternion.Euler(90f,0f,0f);
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(CreateMaze))]
public class createMazeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Mesh"))
            (target as CreateMaze).CreateMeshes();
    }
}

#endif