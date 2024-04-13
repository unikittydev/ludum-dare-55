using UnityEngine;
using UnityEngine.UI;

namespace UniOwl.UI
{
    public class ApplicationQuit : MonoBehaviour
    {
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                quitButton.gameObject.SetActive(false);
            else
                quitButton.onClick.AddListener(Application.Quit);
            
            Destroy(this);
        }
    }
}
