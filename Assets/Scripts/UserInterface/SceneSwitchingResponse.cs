using UnityEngine;
using UnityEngine.SceneManagement;

namespace UserInterface
{
    public class SceneSwitchingResponse : MonoBehaviour, IClickResponse
    {
        public string targetScene;
        public void ExecuteFunctionality()
        {
            SwitchScene();
        }

        private void SwitchScene()
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}