using CoreLogic.UI;

namespace UnityLogic.UI.SplashScreen
{
    public class SplashScreenWindowData : IWindowData
    {
        public string WindowName => "SplashScreen";
    }
    public sealed class SplashScreenWindow : UserInterfaceWindow<SplashScreenWindowData>
    {
        protected override void InitWindowData()
        {
            _windowData = new SplashScreenWindowData();
        }
    }
}