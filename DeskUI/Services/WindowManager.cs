using System;
using Microsoft.AspNetCore.Components;

namespace DeskUI.Services
{
    public class WindowManager
    {
        public record DragContext(WindowInstance Window, int StartX, int StartY, int InitialLeft, int InitialTop);
        public record ResizeContext(WindowInstance Window, int StartX, int StartY, int InitialWidth, int InitialHeight);
        public event Func<Task>? OnChange;
        public event EventHandler<WindowChangedEventArgs>? WindowChanged;
        public List<WindowInstance> Windows { get; } = [];
        public DragContext? Dragged { get; private set; }
        public ResizeContext? Resizing { get; private set; }
        public WindowInstance? GetWindow(Guid id) => Windows.FirstOrDefault(w => w.Id == id);
        public bool IsDragging => Dragged is not null;
        public bool IsResizing => Resizing is not null;
        public void StopDrag() => Dragged = null;
        public void StopResize() => Resizing = null;
        private int _zCounter = 1000;

        private WindowInstance? FindExistingSingleInstance<T>() where T : IComponent => Windows.FirstOrDefault(w => w.SingleInstance && w.ComponentType == typeof(T));

        private void NotifyWindowChanged(WindowAction action, WindowInstance win)
        {
            WindowChanged?.Invoke(this, new WindowChangedEventArgs
            {
                Timestamp = DateTime.Now,
                Action = action,
                Window = win
            });
        }

        public async Task OpenWindowAsync<T>(string title, int width = 600, int height = 400, int top = 100, int left = 100, 
                                             bool allowClose = true, bool overlayed = false, bool singleInstance = false, 
                                             bool titleBarVisible = true, bool allowContentDrag = false) where T : IComponent
        {
            var existing = FindExistingSingleInstance<T>();
            if (existing is not null)
            {
                await BringToFrontAsync(existing.Id);
                return;
            }

            static void content(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
            {
                builder.OpenComponent<T>(0);
                builder.CloseComponent();
            }

            var id = Guid.NewGuid();

            var win = new WindowInstance
            {
                Id = id,
                Title = title,
                Content = content,
                ComponentType = typeof(T),
                Width = width,
                Height = height,
                Top = top,
                Left = left,
                ZIndex = ++_zCounter,
                AllowClose = allowClose,
                Overlayed = overlayed,
                SingleInstance = singleInstance,
                TitleBarVisible = titleBarVisible,
                AllowContentDrag = allowContentDrag
            };

            Windows.Add(win);
            NotifyWindowChanged(WindowAction.Opened, win);
            if (OnChange != null) await OnChange.Invoke();
        }

        public void StartDrag(Guid id, int startX, int startY)
        {
            var win = GetWindow(id);
            if (win is null) return;

            Dragged = new DragContext(win, startX, startY, win.Left, win.Top);
        }

        public async Task HandleMouseMoveAsync(int clientX, int clientY)
        {
            if (Dragged is null) return;

            var dx = clientX - Dragged.StartX;
            var dy = clientY - Dragged.StartY;

            Dragged.Window.Left = Dragged.InitialLeft + dx;
            Dragged.Window.Top = Dragged.InitialTop + dy;

            if (OnChange != null) await OnChange.Invoke();
        }

        public async Task BringToFrontAsync(Guid id)
        {
            var win = GetWindow(id);
            if (win != null)
            {
                win.ZIndex = ++_zCounter;
                if (OnChange != null) await OnChange.Invoke();
            }
        }

        public void StartResize(Guid id, int startX, int startY)
        {
            var win = GetWindow(id);
            if (win is null) return;

            Resizing = new ResizeContext(win, startX, startY, win.Width, win.Height);
        }

        public async Task HandleResizeAsync(int clientX, int clientY)
        {
            if (Resizing?.Window is null) return;

            var dx = clientX - Resizing.StartX;
            var newWidth = Resizing.InitialWidth + dx;
            var newHeight = clientY - Resizing.Window.Top + 10;

            Resizing.Window.Width = Math.Max(150, newWidth);
            Resizing.Window.Height = Math.Max(100, newHeight);
            if (OnChange is not null) await OnChange.Invoke();
        }

        public void Close(Guid id)
        {
            var win = GetWindow(id);
            if (win != null)
            {
                Windows.RemoveAll(w => w.Id == id);
                NotifyWindowChanged(WindowAction.Closed, win);
            }
            OnChange?.Invoke();
        }

        public async Task UpdatePositionAsync(Guid id, int top, int left, int? width = null)
        {
            var win = GetWindow(id);
            if (win is not null)
            {
                win.Top = top;
                win.Left = left;
                if (width is not null) win.Width = width.Value;
                if (OnChange != null) await OnChange.Invoke();
            }
        }
    }

    public class WindowInstance
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public RenderFragment? Content { get; set; }
        public Type? ComponentType { get; set; }
        public int ZIndex { get; set; }
        public int Width { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public bool AllowClose { get; set; }
        public bool Overlayed { get; set; }
        public bool SingleInstance { get; set; }
        public bool TitleBarVisible { get; set; }
        public bool AllowContentDrag { get; set; }
    }

    public class WindowChangedEventArgs : EventArgs
    {
        public DateTime Timestamp { get; set; }
        public WindowAction Action { get; set; }
        public WindowInstance Window { get; set; } = null!;
    }

    public enum WindowAction
    {
        Opened,
        Closed
    }
}