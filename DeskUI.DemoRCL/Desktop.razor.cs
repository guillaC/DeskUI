using DeskUI.Services;

namespace DeskUI.DemoRCL
{
    public partial class Desktop
    {
        Task OpenFirstWindow() => WindowManager.OpenWindowAsync<FirstForm>("FirstComponent", 240, 335);
        Task OpenSecondWindow() => WindowManager.OpenWindowAsync<SecondForm>("SecondComponent (Modal)", 550, 250, allowClose: false, overlayed: true);
        Task OpenUIManagerWindow() => WindowManager.OpenWindowAsync<UIManagerForm>("UIManager", 850, 400);
        Task OpenUIThemeBuilderWindow() => WindowManager.OpenWindowAsync<ThemeBuilder>("ThemeBuilder", 850, 400);
    }
}
