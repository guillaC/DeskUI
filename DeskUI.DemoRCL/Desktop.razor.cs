using DeskUI.Services;

namespace DeskUI.DemoRCL
{
    public partial class Desktop
    {
        async Task OpenFirstWindow()
        {
            await WindowManager.OpenWindowAsync("FirstComponent", builder =>
            {
                builder.OpenComponent<FirstForm>(0);
                builder.CloseComponent();
            }, width: 240, height: 335);
        }

        async Task OpenSecondWindow()
        {
            await WindowManager.OpenWindowAsync("SecondComponent (Modal)", builder =>
            {
                builder.OpenComponent<SecondForm>(0);
                builder.CloseComponent();
            }, width: 550, height: 250, allowClose: false, overlayed: true);
        }

        async Task OpenUIManagerWindow()
        {
            await WindowManager.OpenWindowAsync("UIManager", builder =>
            {
                builder.OpenComponent<UIManagerForm>(0);
                builder.CloseComponent();
            }, width: 850, height: 400);
        }
    }
}
