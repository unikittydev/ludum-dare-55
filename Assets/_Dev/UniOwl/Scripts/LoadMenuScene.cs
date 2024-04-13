using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LoadMenuScene : MonoBehaviour
    {
        [SerializeField] private int sceneIndex;
        
        private async void Awake()
        {
            if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded)
            {
                await SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
                // This is clearly very bad but I guess I'll just leave it here.
                FindFirstObjectByType<SettingsView>().SetInGame(true);
            }
            Destroy(gameObject);
        }
    }
}