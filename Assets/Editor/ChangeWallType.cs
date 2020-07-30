using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChangeWallType : EditorWindow
{
    int currentSelectionCount = 0;
    GameObject currentlySelectedGameobject;
    List<Transform> children = new List<Transform>();
    List<bool> foldoutState = new List<bool>();

    Vector2 scrollPosition;
    //bool displayFoldout;

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
        //GetSelection();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        EditorGUI.indentLevel = 0;

        if (currentSelectionCount != 1)
        {
            EditorGUILayout.LabelField("Selection Count: " + currentSelectionCount, EditorStyles.largeLabel);
            EditorGUILayout.LabelField("Only one object can to be selected.", EditorStyles.largeLabel);
            EditorGUILayout.LabelField("This tool doesn't work with more or less.", EditorStyles.largeLabel);

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

        }
        else
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Width(this.position.width), GUILayout.Height(this.position.height));

            EditorGUILayout.LabelField("Selection Count: " + currentSelectionCount, EditorStyles.largeLabel);
            EditorGUILayout.LabelField("Parent Gameobject: " + currentlySelectedGameobject.name, EditorStyles.largeLabel);
            EditorGUILayout.LabelField("Child Count: " + children.Count, EditorStyles.largeLabel);

            foreach (Transform child in children)
            {
                int index = children.IndexOf(child);
                EditorGUI.indentLevel = 1;
                foldoutState[index] = EditorGUILayout.Foldout(foldoutState[index], "Wall: " + child.name);

                if (foldoutState[index])
                {
                    EditorGUILayout.BeginHorizontal();
                    foreach (Transform nestedChild in child)
                    {
                        //EditorGUI.indentLevel = 3;
                        if (GUILayout.Button(nestedChild.name.Remove(0,9), GUILayout.Width(100), GUILayout.Height(30)))
                        {
                            DeactivateWalls(child);
                            nestedChild.gameObject.SetActive(!nestedChild.gameObject.activeSelf);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        Repaint();
    }

    void OnSelectionChange()
    {
        Reset();

        currentSelectionCount = Selection.gameObjects.Length;

        if (currentSelectionCount != 1)
            return;

        currentlySelectedGameobject = Selection.gameObjects[0].transform.gameObject;

        while (currentlySelectedGameobject.transform.parent != null)
            currentlySelectedGameobject = currentlySelectedGameobject.transform.parent.gameObject;

        foreach (Transform child in currentlySelectedGameobject.transform)
        {
            children.Add(child);
            foldoutState.Add(false);
        }
    }

    void Reset()
    {
        currentSelectionCount = 0;
        children.Clear();
        foldoutState.Clear();
        currentlySelectedGameobject = null;
    }

    void DeactivateWalls(Transform child)
    {
        foreach (Transform nestedChild in child)
        {
            nestedChild.gameObject.SetActive(false);
        }
    }

    //void GetSelection()
    //{
    //    currentSelectionCount = 0;
    //    children.Clear();
    //    currentSelectionCount = Selection.gameObjects.Length;

    //    currentlySelectedGameobject = Selection.gameObjects[0].transform.gameObject;

    //    while (true)
    //    { 
    //        if (currentlySelectedGameobject.transform.parent != null)
    //        {
    //            currentlySelectedGameobject = currentlySelectedGameobject.transform.parent.gameObject;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    foreach (Transform child in currentlySelectedGameobject.transform)
    //    {
    //        children.Add(child);
    //    }
    //}
}
