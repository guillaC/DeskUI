using DeskUI.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DeskUI
{
    public partial class WindowHost
    {
        private DotNetObjectReference<WindowHost>? _dotNetRef;

        [Parameter] public string Theme { get; set; } = "classic-light";

        protected override void OnInitialized()
        {
            WindowManager.OnChange += async () => await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetRef = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeVoidAsync("trackMouseMove", _dotNetRef);
            }
        }

        [JSInvokable]
        public Task HandleMouseMoveAsync(int x, int y)
        {
            if (WindowManager.IsDragging) return WindowManager.HandleMouseMoveAsync(x, y);
            if (WindowManager.IsResizing) return WindowManager.HandleResizeAsync(x, y);
            return Task.CompletedTask;
        }

        private void OnMouseUp()
        {
            WindowManager.StopDrag();
            WindowManager.StopResize();
        }
    }
}