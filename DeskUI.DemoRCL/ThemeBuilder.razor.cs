using Microsoft.JSInterop;
using System.Text.RegularExpressions;

namespace DeskUI.DemoRCL
{
    public partial class ThemeBuilder
    {
        private record Var(string Key, string Value, InputType InputType)
        {
            public string Value { get; set; } = Value;
        }

        private readonly List<Var> cssVars = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var cssVarsDic = await JS.InvokeAsync<Dictionary<string, string>>(
                    "themeBuilder.getCssVariables",
                    ".classic-light" // theme par défaut
                );

                foreach (var cssVar in cssVarsDic)
                {
                    InputType inputType = InputType.Text;
                    if (IsColor(cssVar.Value))
                    {
                        inputType = InputType.Color;
                    }
                    else if (double.TryParse(cssVar.Value.Replace("px", ""), out _))
                    {
                        inputType = InputType.Number;
                    }
                    cssVars.Add(new Var(cssVar.Key, cssVar.Value, inputType));
                }

                StateHasChanged();
            }
        }

        private void UpdateVar(string key, string? value)
        {
            var v = cssVars.FirstOrDefault(x => x.Key == key);
            if (v is not null) v.Value = value ?? "";
        }

        private static bool IsColor(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            if (Regex.IsMatch(value, "^#([0-9a-fA-F]{3}|[0-9a-fA-F]{6})$"))return true;
            if (Regex.IsMatch(value, @"^rgba?\(\s*\d+\s*,\s*\d+\s*,\s*\d+(\s*,\s*(\d+(\.\d+)?))?\s*\)$")) return true;
            if (Regex.IsMatch(value, @"^hsla?\(.+\)$")) return true;
            return false;
        }

        private enum InputType
        {
            Color, Number, Text
        }
    }
}
