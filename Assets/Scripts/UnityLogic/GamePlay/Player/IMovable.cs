namespace UnityLogic.GamePlay.Player
{
    public struct MovableCharacterData
    {
        public float AcceleratingSpeed;
        public float MaxSpeed;
        public float StoppingSpeed;
        public float MaxX;
        public float MaxY;
    }

    public interface IMovable
    {
        float Horizontal { get; }
        float Vertical { get; }
        MovableCharacterData MovableData { get; }
    }
}