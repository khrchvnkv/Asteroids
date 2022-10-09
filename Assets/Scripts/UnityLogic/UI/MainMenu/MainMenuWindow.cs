using CoreLogic.UI;

namespace UnityLogic.UI.MainMenu
{
    public class MainMenuWindowData : IWindowData 
    {
        public string WindowName => "MainMenu";
    }
    public class MainMenuWindow : UserInterfaceWindow<MainMenuWindowData>
    {
        protected override void InitWindowData()
        {
            _windowData = new MainMenuWindowData();
        }
    }
}