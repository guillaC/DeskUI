using DeskUI.Services;

namespace DeskUI.DemoServer.Pages
{
    public partial class Index
    {
        async Task OpenFirstWindow()
        {
            await WindowManager.OpenWindowAsync("FirstComponent", builder =>
             {
                 builder.OpenComponent<DemoRCL.FirstForm>(0);
                 builder.CloseComponent();
             }, width: 240, height: 320);
        }

        async Task OpenSecondWindow()
        {
            await WindowManager.OpenWindowAsync("SecondComponent (Modal)", builder =>
            {
                builder.OpenComponent<DemoRCL.SecondForm>(0);
                builder.CloseComponent();
            }, width: 550, height: 250, allowClose: false, overlayed:true);
        }

        async Task OpenUIManagerWindow()
        {
            await WindowManager.OpenWindowAsync("UIManager", builder =>
            {
                builder.OpenComponent<DemoRCL.UIManagerForm>(0);
                builder.CloseComponent();
            }, width: 800, height: 400);
        }
    }
}
