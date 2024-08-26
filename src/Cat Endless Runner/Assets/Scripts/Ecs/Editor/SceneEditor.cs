using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Ecs.Editor
{
    public class SceneEditor
    {
#if UNITY_EDITOR
        [MenuItem("Helper/StartScene")]
        public static void GoToStartScene()
        {
            OpenScene("Assets/Scenes/StartScene.unity");
        }
        
        [MenuItem("Helper/GameScene")]
        public static void GoToGameScene()
        {
            OpenScene("Assets/Scenes/GameScene.unity");
        }
        
        [MenuItem("Helper/DefeatScene")]
        public static void GoToDefeatScene()
        {
            OpenScene("Assets/Scenes/DefeatScene.unity");
        }

        private static void OpenScene(string name)
        {
            if (Application.isPlaying)
            {
                Debug.Log("Open scene only in Edit mode!");
                return;
            }
            
            bool isSaved = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), EditorSceneManager.GetActiveScene().path);
            Debug.Log("Saved Scene " + (isSaved ? "OK" : "Error!"));
            EditorSceneManager.OpenScene(name);
        }
#endif
    }
}