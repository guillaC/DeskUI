using Microsoft.JSInterop;
using System.Text.RegularExpressions;

namespace DeskUI.DemoRCL
{
    public partial class ThemeBuilder
    {
        private record Var
        {
            public string Key { get; init; }
            public string RawValue { get; set; }
            public InputType InputType { get; init; }

            public double? Number { get; set; }
            public string? Unit { get; set; }

            public Var(string key, string rawValue, InputType inputType)
            {
                Key = key;
                RawValue = rawValue;
                InputType = inputType;

                if (inputType == InputType.Number)
                {
                    var match = Regex.Match(rawValue, @"^\s*([0-9]*\.?[0-9]+)\s*([a-zA-Z%]+)\s*;?\s*$");
                    if (match.Success)
                    {
                        Number = double.Parse(match.Groups[1].Value);
                        Unit = match.Groups[2].Value;
                    }
                }
            }
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
            var v = cssVars.First(x => x.Key == key);

            if (v.InputType == InputType.Number)
            {
                v.Number = double.TryParse(value, out var num) ? num : 0;
                v.RawValue = $"{v.Number}{v.Unit}";
            }
            else
            {
                v.RawValue = value ?? "";
            }
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
