using DeskUI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DeskUI
{
    public partial class FloatingWindow
    {
        [Parameter] public required WindowInstance Instance { get; set; }
        [Parameter] public string Theme { get; set; } = "classic-light";
        [Parameter] public EventCallback OnCloseRequested { get; set; }

        [Parameter] public bool Docked { get; set; } // Paramétrage hors nstance classique, jamais utilisé par le window host

        public string WindowId => $"window-{Instance.Id}";
        private string Style => $"position:fixed; top:{Instance.Top}px; left:{Instance.Left}px; width:{Instance.Width}px; {(Instance.Height > 0 ? "height:" + Instance.Height + "px;" : "")} z-index:{Instance.ZIndex};";

        private void StartResize(MouseEventArgs e)
        {
            WindowManager.StartResize(Instance.Id, (int)e.ClientX, (int)e.ClientY);
        }

        private void StartDrag(MouseEventArgs e)
        {
            WindowManager.StartDrag(Instance.Id, (int)e.ClientX, (int)e.ClientY);
        }

        private async Task BringToFrontAsync(MouseEventArgs _)
        {
            await WindowManager.BringToFrontAsync(Instance.Id);
            await InvokeAsync(StateHasChanged);
        }

        private async Task CloseAsync()
        {
            if (Instance.AllowClose) await OnCloseRequested.InvokeAsync();
        }
    }
}