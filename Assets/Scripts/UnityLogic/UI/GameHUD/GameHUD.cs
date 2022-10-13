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
        
        [SerializeField] private TMP_Text xCoordText, yCoordText;
        
        public override void ShowWindow(IWindowData data)
        {
            base.ShowWindow(data);
            WindowData.MovingBehaviour.OnPositionChanged += UpdateCoordinateView;
        }
        private void UpdateCoordinateView(float x, float y)
        {
            xCoordText.text = string.Format(X_TextFormat, x);
            yCoordText.text = string.Format(Y_TextFormat, y);
        }
    }
}