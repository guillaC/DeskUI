# DeskUI
OS‑style multi‑window UI for Blazor.  
Draggable. Resizable. Themed. Simple.

## Setup (Blazor Server / WebAssembly)
- Import the namespace in `_Imports.razor`
```razor
@using DeskUI
```
- Register the WindowManager service in Program.cs
```csharp
builder.Services.AddScoped<WindowManager>();
```
- Add the window host in your main page (e.g. `Index.razor`) ClassicLight is the default theme, but you can specify another one:
```html
<WindowHost Theme="@Themes.ClassicLight" />
```
- Import the theme stylesheet:
```html
<link rel="stylesheet" href="_content/DeskUI/themes.css" />
```
- Register scripts:
```html
<script src="_content/DeskUI/drag.js"/>
```
- Inject the WindowManager where you need to open windows
```razor
@inject WindowManager WindowManager
```
## Usage
```csharp
Task OpenFirstWindow() => WindowManager.OpenWindowAsync<FirstForm>("FirstComponent", 240, 335);
Task OpenSecondWindow() => WindowManager.OpenWindowAsync<SecondForm>("SecondComponent (Modal)", 550, 250, allowClose: false, overlayed: true);
```
## Theme Support
DeskUI includes a full CSS-variable based theming system.
You can choose one of the built‑in themes or define your own.

Available built‑in themes:
- Themes.ClassicLight
- Themes.ClassicDark
- Themes.WindowsCE
### Create your own theme
You can override any of the theme variables defined in `themes.css`.
```css
.my-custom-theme {
    --win-bg: #fafafa;
    --header-bg: #e8e8e8;
    --close-color: #444;
    /* ...override any variables you want... */
}
```
Then apply it:
```html
<WindowHost Theme="my-custom-theme" />
```