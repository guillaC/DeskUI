using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DeskUI
{
    public partial class FloatingWindow : IDisposable
    {
        [Parameter] public string Title { get; set; } = "Window";
        [Parameter] public string Theme { get; set; } = "classic-light";
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public Guid Id { get; set; }
        [Parameter] public int ZIndex { get; set; }
        [Parameter] public int Top { get; set; }
        [Parameter] public int Left { get; set; }
        [Parameter] public int Width { get; set; }
        [Parameter] public int Height { get; set; }
        [Parameter] public EventCallback OnCloseRequested { get; set; }
        [Parameter] public bool AllowClose { get; set; }
        [Parameter] public bool Overlayed { get; set; }
        [Parameter] public bool Docked { get; set; }

        public string WindowId => $"window-{Id}";
        private string Style => $"position:fixed; top:{Top}px; left:{Left}px; width:{Width}px; {(Height > 0 ? "height:" + Height + "px;" : "")} z-index:{ZIndex};";

        protected override void OnInitialized()
        {
            WindowManager.OnThemeChanged += OnThemeChanged;
        }

        public void Dispose()
        {
            WindowManager.OnThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged()
        {
            InvokeAsync(StateHasChanged);
        }

        private void StartResize(MouseEventArgs e)
        {
            WindowManager.StartResize(Id, (int)e.ClientX, (int)e.ClientY);
        }

        private void StartDrag(MouseEventArgs e)
        {
            WindowManager.StartDrag(Id, (int)e.ClientX, (int)e.ClientY);
        }

        private async Task BringToFrontAsync(MouseEventArgs _)
        {
            await WindowManager.BringToFrontAsync(Id);
            await InvokeAsync(StateHasChanged);
        }

        private async Task CloseAsync()
        {
            if (AllowClose) await OnCloseRequested.InvokeAsync();
        }
    }
}