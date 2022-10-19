namespace UnityLogic.GamePlay.Player
{
    public struct MovableCharacterData
    {
        public float AcceleratingSpeed;
        public float MaxSpeed;
        public float StoppingSpeed;
        public float RotateSpeed;   // Degrees per Second
        public GamePlayManager.ScreenSizeData Screen;
    }
    

    public interface IMovable
    {
        float Horizontal { get; }
        float Vertical { get; }
        MovableCharacterData MovableData { get; }
    }
}