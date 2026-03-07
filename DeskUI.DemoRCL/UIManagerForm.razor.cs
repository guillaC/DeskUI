using DeskUI.Services;

namespace DeskUI.DemoRCL
{
    public partial class UIManagerForm : IDisposable
    {
        private List<string> notifications = [];

        protected override void OnInitialized()
        {
            WindowManager.OnChange += () => InvokeAsync(StateHasChanged);
            WindowManager.WindowChanged += OnWindowChanged;
        }

        private void OnWindowChanged(object? sender, WindowChangedEventArgs e)
        {
            var msg = $"[{e.Timestamp:HH:mm:ss}] {e.Action} - {e.Window.Title} ({e.Window?.ComponentType?.Name})";
            notifications.Insert(0, msg);
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            WindowManager.WindowChanged -= OnWindowChanged;
            GC.SuppressFinalize(this);
        }
    }
}
