using CoreLogic.UI;

namespace UnityLogic.UI.MainMenu
{
    public class MainMenuWindowData : IWindowData { }
    public class MainMenuWindow : UserInterfaceWindow<MainMenuWindowData>
    {
        protected override void InitWindowData()
        {
            WindowData = new MainMenuWindowData();
        }
    }
}