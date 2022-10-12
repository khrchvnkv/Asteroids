using CoreLogic;
using CoreLogic.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityLogic.GamePlay;

namespace UnityLogic.UI.MainMenu
{
    public class MainMenuWindowData : IWindowData { }
    public class MainMenuWindow : UserInterfaceWindow<MainMenuWindowData>
    {
        [SerializeField] private Button startButton;
  
        protected override void InitWindowData()
        {
            WindowData = new MainMenuWindowData();
        }
        protected override void Inject()
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        private void OnStartButtonClicked()
        {
            HideWindow();
            var gamePlayController = GameCore.Instance.GetManager<GamePlayController>();
            gamePlayController.StartGame();
        }
    }
}