using CoreLogic.UI;
using TMPro;
using UnityEngine;
using UnityLogic.GamePlay.Player;

namespace UnityLogic.UI.GameHUD
{
    public sealed class GameHUD_Data : IWindowData
    {
        public readonly MovingBehaviour MovingBehaviour;
        
        public GameHUD_Data(MovingBehaviour movingBehaviour)
        {
            MovingBehaviour = movingBehaviour;
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
        
        public override void ShowWindow(IWindowData data)
        {
            base.ShowWindow(data);
            WindowData.MovingBehaviour.OnPositionChanged += UpdateCoordinateView;
        }
        private void UpdateCoordinateView(MovingBehaviour.MovableInfo info)
        {
            xCoordText.text = string.Format(X_TextFormat, info.X);
            yCoordText.text = string.Format(Y_TextFormat, info.Y);
            rotationText.text = string.Format(Rot_TextFormat, info.Rotation);
            speedText.text = string.Format(Speed_TextFormat, info.Speed);
        }
    }
}