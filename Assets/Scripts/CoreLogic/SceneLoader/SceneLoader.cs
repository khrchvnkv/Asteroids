using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CoreLogic.SceneLoader
{
    public sealed class SceneLoader
    {
        private const string AppLoadingSceneName = "AppStart";
        private const string AppSceneName = "Application";

        public async UniTask LoadSceneAsync()
        {
            await SceneManager.LoadSceneAsync(AppSceneName, LoadSceneMode.Additive);
        }
    }
}