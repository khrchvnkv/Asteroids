using CoreLogic.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityLogic.GamePlay;

namespace UnityLogic.UI.MainMenu
{
    public sealed class MainMenuWindowData : IWindowData { }
    public sealed class MainMenuWindow : UserInterfaceWindow<MainMenuWindowData>
    {
        [SerializeField] private Button startButton;
  
        protected override void Inject()
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        private void OnStartButtonClicked()
        {
            HideWindow();
            var gamePlayController = GameCore.Instance.GetController<GamePlayController>();
            gamePlayController.StartGame();
        }
    }
}