using Microsoft.JSInterop;

namespace DeskUI.DemoRCL
{
    public partial class ThemeBuilder
    {

        private Dictionary<string, string> cssVars = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                cssVars = await JS.InvokeAsync<Dictionary<string, string>>(
                    "themeBuilder.getCssVariables",
                    ".classic-light" // theme par défaut
                );

                StateHasChanged();
            }
        }

        void UpdateVar(string key, string? value) => cssVars[key] = value ?? "";
    }
}
