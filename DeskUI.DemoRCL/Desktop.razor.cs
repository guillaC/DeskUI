using DeskUI.Services;

namespace DeskUI.DemoRCL
{
    public partial class Desktop
    {
        private Task OpenFirstWindow() => WindowManager.OpenWindowAsync<FirstForm>("FirstComponent", 240, 335);
        private Task OpenSecondWindow() => WindowManager.OpenWindowAsync<SecondForm>("SecondComponent (Modal)", 550, 250, allowClose: false, overlayed: true);
        private Task OpenUIManagerWindow() => WindowManager.OpenWindowAsync<UIManagerForm>("UIManager", 850, 400, singleInstance: true);
        // Task OpenUIThemeBuilderWindow() => WindowManager.OpenWindowAsync<ThemeBuilder>("ThemeBuilder", 850, 400);
    }
}
