using CoreLogic.UI;

namespace UnityLogic.UI.SplashScreen
{
    public class SplashScreenWindowData : IWindowData { }
    public sealed class SplashScreenWindow : UserInterfaceWindow<SplashScreenWindowData>
    {
        protected override void InitWindowData()
        {
            WindowData = new SplashScreenWindowData();
        }
    }
}