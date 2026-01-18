// merged MenuEditor

#region QuantumMenuUIControllerEditor.cs

namespace Quantum.Menu.Editor {
  using Quantum;
  using Quantum.Menu;
  using UnityEditor;

  /// <summary>
  /// The custom editor searches and displays all <see cref="QuantumMenuSceneInfo"/> assets in the project."/>
  /// </summary>
  [CustomEditor(typeof(QuantumMenuUIController))]
  public class QuantumMenuUIControllerEditor : Editor {
    bool _sceneInfoFoldout;
    QuantumMenuSceneInfo[] _sceneInfo;

    /// <summary>
    /// Adding scene info asset selection foldout.
    /// </summary>
    public override void OnInspectorGUI() {
      base.OnInspectorGUI();

      _sceneInfoFoldout = EditorGUILayout.Foldout(_sceneInfoFoldout, "Scene Info Files");
      if (_sceneInfoFoldout) {
        _sceneInfo ??= UnityEngine.Resources.LoadAll<QuantumMenuSceneInfo>("");
        foreach (var info in _sceneInfo) {
          EditorGUILayout.ObjectField(info, typeof(QuantumMenuSceneInfo), false);
        }
      }
    }
  }
}

#endregion


#region QuantumMenuUIScreenEditor.cs

namespace Quantum.Editor {
  using Menu;
  using UnityEditor;

  /// <summary>
  /// Debug QuantumMenuUIScreen content.
  /// </summary>
  [CustomEditor(typeof(QuantumMenuUIScreen), true)]
  public class QuantumMenuUIScreenEditor : Editor {
    /// <inheritdoc/>
    public override void OnInspectorGUI() {
      base.OnInspectorGUI();

      var data = (QuantumMenuUIScreen)target;

      if (data.ConnectionArgs != null) {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Connect Args", EditorStyles.boldLabel);
        using (new EditorGUI.DisabledScope(true)) {
          EditorGUILayout.TextField("Username", data.ConnectionArgs.Username);
          EditorGUILayout.TextField("Session", data.ConnectionArgs.Session);
          EditorGUILayout.TextField("PreferredRegion", data.ConnectionArgs.PreferredRegion);
          EditorGUILayout.TextField("Region", data.ConnectionArgs.Region);
          EditorGUILayout.TextField("AppVersion", data.ConnectionArgs.AppVersion);
          EditorGUILayout.TextField("Scene", data.ConnectionArgs.Scene != null ? data.ConnectionArgs.Scene.ScenePath : null);
          EditorGUILayout.IntField("MaxPlayerCount", data.ConnectionArgs.MaxPlayerCount);
          EditorGUILayout.Toggle("Creating", data.ConnectionArgs.Creating);
        }
      }
    }
  }
}

#endregion

