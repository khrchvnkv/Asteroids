using CoreLogic.UI;
using TMPro;
using UnityEngine;
using UnityLogic.GamePlay;
using UnityLogic.GamePlay.Player;

namespace UnityLogic.UI.GameHUD
{
    public sealed class GameHUD_Data : IWindowData
    {
        public readonly MovingBehaviour MovingBehaviour;
        public readonly LaserShootingBehaviour LaserBehaviour;
        
        public GameHUD_Data() { }
        public GameHUD_Data(MovingBehaviour movingBehaviour, LaserShootingBehaviour laserBehaviour)
        {
            MovingBehaviour = movingBehaviour;
            LaserBehaviour = laserBehaviour;
        }
    }
    public sealed class GameHUD : UserInterfaceWindow<GameHUD_Data>
    {
        private const string X_TextFormat = "X: {0:0.0}";
        private const string Y_TextFormat = "Y: {0:0.0}";
        private const string Rot_TextFormat = "Rot: {0:0}";
        private const string Speed_TextFormat = "Sp: {0:0.0}";

        [Header("Moving Data")] 
        [SerializeField] private TMP_Text xCoordText;
        [SerializeField] private TMP_Text yCoordText;
        [SerializeField] private TMP_Text rotationText;
        [SerializeField] private TMP_Text speedText;

        [Header("Score")] 
        [SerializeField] private TMP_Text scoreText;

        [Header("Laser Data")] 
        [SerializeField] private LaserInfoView laserInfo;

        public override void ShowWindow(IWindowData data)
        {
            base.ShowWindow(data);
            laserInfo.Inject(LaserShootingBehaviour.MaxLaserShoots);
            WindowData.MovingBehaviour.OnPositionChanged += UpdateCoordinateView;
            WindowData.LaserBehaviour.OnReloadStarted += laserInfo.EnableSlider;
            WindowData.LaserBehaviour.OnReloadFinished += laserInfo.DisableSlider;
            WindowData.LaserBehaviour.OnLaserReloadProgressChanged += laserInfo.SetReloadProgressValue;
            WindowData.LaserBehaviour.OnLaserShootsCountChanged += laserInfo.SetShootCount;
            var scoreController = GameCore.Instance.GetManager<GamePlayManager>().ScoreController;
            scoreController.OnScoreChanged += UpdateScoreText;
            UpdateScoreText(scoreController.Score);
            laserInfo.SetShootCount(WindowData.LaserBehaviour.AvailableShoots);
        }
        public override void HideWindow()
        {
            base.HideWindow();
            WindowData.MovingBehaviour.OnPositionChanged -= UpdateCoordinateView;
            WindowData.LaserBehaviour.OnReloadStarted -= laserInfo.EnableSlider;
            WindowData.LaserBehaviour.OnReloadFinished -= laserInfo.DisableSlider;
            WindowData.LaserBehaviour.OnLaserReloadProgressChanged -= laserInfo.SetReloadProgressValue;
            WindowData.LaserBehaviour.OnLaserShootsCountChanged -= laserInfo.SetShootCount;
            GameCore.Instance.GetManager<GamePlayManager>().ScoreController.OnScoreChanged -= UpdateScoreText;
        }
        private void UpdateCoordinateView(MovingBehaviour.MovableInfo info)
        {
            xCoordText.text = string.Format(X_TextFormat, info.X);
            yCoordText.text = string.Format(Y_TextFormat, info.Y);
            rotationText.text = string.Format(Rot_TextFormat, info.Rotation);
            speedText.text = string.Format(Speed_TextFormat, info.Speed);
        }
        private void UpdateScoreText(int score)
        {
            scoreText.text = $"{score}";
        }
    }
}