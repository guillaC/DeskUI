# DeskUI  
![Blazor](https://img.shields.io/badge/blazor-purple?logo=blazor&logoColor=white) ![NuGet](https://img.shields.io/nuget/vpre/DeskUI?label=nuget&logo=nuget) ![Stars](https://img.shields.io/github/stars/guillaC/BlazorDesk?style=flat) ![License](https://img.shields.io/badge/license-MIT-green)
![Last Deployment](https://img.shields.io/github/actions/workflow/status/guillaC/DeskUI/azure-static-web-apps-wonderful-coast-0bc3fab10.yml?label=deployment)
![Azure Static Web Apps](https://img.shields.io/badge/Azure%20Static%20Web%20Apps-Deployed-0078D4?logo=microsoft-azure&logoColor=white)


**OS-like multi window system for Blazor applications.**  
A Razor Class Library for draggable, resizable, focusable windows, just like a real desktop environment, without any external dependencies.

## Features
- Draggable, resizable, focusable multi-window system
- Modal & overlay support with zero external dependencies
- Built-in theming (light, dark, GlassBrown) + full CSS variable customization
- Works with Blazor Server and WebAssembly (Blazor Hybrid support has not been tested yet).

## Demo
<img src="https://github.com/user-attachments/assets/ab36802b-7475-4d1c-aabb-e98e3ff782a6" width="650">

## Projects
- **DeskUI** core RCL providing the full windowing system  
- **DeskUI.DemoRCL** reusable demo RCL consumed by the demo apps  
- **DeskUI.DemoServer** Blazor Server demo  
- **DeskUI.DemoWASM** Blazor WebAssembly demo 

## Setup (Blazor Server / WebAssembly)
- Import the namespace in `_Imports.razor`
```razor
@using DeskUI
```
- Register the WindowManager service in `Program.cs`
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
Task OpenFirstWindow() => WindowManager.OpenWindowAsync<FirstForm>("FirstComponent", 240, 335, singleInstance: true);
Task OpenSecondWindow() => WindowManager.OpenWindowAsync<SecondForm>("SecondComponent (Modal)", 550, 250, allowClose: false, overlayed: true);
```

## Theme Support
DeskUI includes a full CSS variable based theming system.
You can choose one of the built-in themes or define your own.

Available built-in themes:
- Themes.ClassicLight
- Themes.ClassicDark
- Themes.GlassBrown
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

## NuGet
DeskUI is available on [NuGet.org](https://www.nuget.org/packages/DeskUI/)

Install via .NET CLI:
```bash
dotnet add package DeskUI
```

## License
This project is licensed under the **MIT License** – see the [LICENSE](LICENSE.txt) file for details.
