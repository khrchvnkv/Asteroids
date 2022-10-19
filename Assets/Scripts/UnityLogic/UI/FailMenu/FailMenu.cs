using CoreLogic.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityLogic.GamePlay;

namespace UnityLogic.UI.FailMenu
{
    public class FailMenuWindowData : IWindowData
    {
        public readonly int Score;

        public FailMenuWindowData(int score)
        {
            Score = score;
        }
    }
    public class FailMenu : UserInterfaceWindow<FailMenuWindowData>
    {
        private const string ScoreTextFormat = "Score: {0}";
        
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Button restartButton;
        
        private void Awake()
        {
            restartButton.onClick.AddListener(Restart);
        }
        private void Restart()
        {
            GameCore.Instance.GetManager<GamePlayManager>().StartGame();
            HideWindow();
        }
        public override void ShowWindow(IWindowData data)
        {
            base.ShowWindow(data);
            scoreText.text = string.Format(ScoreTextFormat, WindowData.Score);
        }
    }
}