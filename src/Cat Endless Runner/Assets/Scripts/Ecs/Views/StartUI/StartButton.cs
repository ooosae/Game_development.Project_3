using Ecs.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ecs.Views.StartUI
{
    [RequireComponent(typeof(Button))]
    public class StartButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(LoadGameScene);
        }

        private void LoadGameScene()
        {
            SceneManager.LoadScene(SceneConstants.GameScene);
        }
    }
}