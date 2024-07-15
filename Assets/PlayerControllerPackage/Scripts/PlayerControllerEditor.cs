using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(PlayerController))]
[CanEditMultipleObjects]
public class PlayerControllerEditor : Editor
{
    PlayerController playerController;
    List<Component> currentScripts;
    PlayerControllerType previousType;
    public override void OnInspectorGUI()
    {
        // set variable refrences
        playerController = (PlayerController)target;
        currentScripts = playerController.currentScripts;
        DrawDefaultInspector();

        // add first person scripts
        if (previousType != playerController.controllerType)
        {
            if (playerController.controllerType == PlayerControllerType.FirstPerson)
            {
                if (playerController.GetComponent<ThirdPersonPlayerMovement>()) RemoveScripts();
                if (currentScripts.Count == 0) LoadScripts("Assets/PlayerControllerPackage/Behaviours/FirstPersonComponents");
                previousType = PlayerControllerType.FirstPerson;
            }

            // add third person scripts
            if (playerController.controllerType == PlayerControllerType.ThirdPerson)
            {
                if (playerController.GetComponent<FirstPersonPlayerMovement>()) RemoveScripts();
                if (currentScripts.Count == 0) LoadScripts("Assets/PlayerControllerPackage/Behaviours/ThirdPersonComponents");
                previousType = PlayerControllerType.ThirdPerson;
            }
        }

        // custom layout
        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(5));
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();

        // import button
        GUILayout.Label("Import settings", EditorStyles.boldLabel, GUILayout.Width(100));
        if (GUILayout.Button("", GUILayout.Width(20)))
        {
            if (playerController.importJson == null) { Debug.LogError("No input for importing settings!"); return; }
            Debug.Log(playerController.importJson);
        }

        GUILayout.Space(10);
        
        // import input
        playerController.importJson = (TextAsset)EditorGUILayout.ObjectField(playerController.importJson, typeof(TextAsset), false, GUILayout.Width(200));
        GUILayout.EndHorizontal();
    }
    private void LoadScripts(string folder)
    {
        string[] assetPaths = AssetDatabase.FindAssets("t:MonoScript", new string[] { folder });
        foreach (string assetPath in assetPaths)
        {
            string fullPath = AssetDatabase.GUIDToAssetPath(assetPath);
            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(fullPath);

            ControllerBehaviour component = playerController.gameObject.AddComponent(script.GetClass()) as ControllerBehaviour;
            currentScripts.Add(component);
            component.Load();
        }
    }
    private void RemoveScripts()
    {
        foreach (Component script in currentScripts)
        {
            script.GetComponent<ControllerBehaviour>().Save();
            DestroyImmediate(script);
        }
        currentScripts.Clear();
    }

}
