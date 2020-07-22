using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChangeWallType : EditorWindow
{
    int currentSelectionCount = 0;
    GameObject currentlySelectedGameobject;

    [MenuItem("Custom Tools/Change Wall Type")]
    public static void ChangeWall()
    {
        LaunchEditor();
    }

    public static void LaunchEditor()
    {
        var editorWin = GetWindow<ChangeWallType>();
        editorWin.Show();
    }

    void OnGUI()
    {
        GetSelection();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Selection Count: " + currentSelectionCount, EditorStyles.boldLabel);

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        Repaint();
    }

    void GetSelection()
    {
        currentSelectionCount = 0;
        currentSelectionCount = Selection.gameObjects.Length;

    }
}
