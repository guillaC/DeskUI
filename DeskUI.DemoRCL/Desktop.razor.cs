using DeskUI.Services;

namespace DeskUI.DemoRCL
{
    public partial class Desktop
    {
        private Task OpenFirstWindow() => WindowManager.OpenWindowAsync<FirstForm>("Sample Window", 240, 335);
        private Task OpenSecondWindow(bool isOverLayed = false) => WindowManager.OpenWindowAsync<SecondForm>("Sample Window 2", 550, 350, overlayed: isOverLayed);
        private Task OpenUIManagerWindow() => WindowManager.OpenWindowAsync<UIManagerForm>("UIManager", 1200, 400, singleInstance: true);
        // Task OpenUIThemeBuilderWindow() => WindowManager.OpenWindowAsync<ThemeBuilder>("ThemeBuilder", 850, 400);
    }
}
