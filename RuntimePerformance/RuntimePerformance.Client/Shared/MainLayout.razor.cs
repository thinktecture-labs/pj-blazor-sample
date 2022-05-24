using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RuntimePerformance.Client.Shared
{
    public partial class MainLayout
    {
        [Inject] private ILocalStorageService? _localStorage { get; set; }
        [Inject] private NavigationManager? _navigationManager { get; set; }

        private static readonly string ApplicationThemeKey = "application-theme";
        private static readonly MudTheme DefaultTheme = new()
        {
            Palette = new Palette
            {
                Black = "#272c34",
                AppbarBackground = "#ffffff",
                AppbarText = "#ff584f",
                DrawerBackground = "#ff584f",
                DrawerText = "ffffff",
                DrawerIcon = "ffffff",
                Primary = "#ff584f",
                Secondary = "#3d6fb4"
            },
            Typography = new Typography
            {
                Default = new Default() { FontFamily = new string[] { "Montserrat", "Helvetica", "Arial", "sans-serif" } }
            }
        };
        private static readonly MudTheme DarkTheme = new()
        {
            Palette = new Palette
            {
                Black = "#27272f",
                Background = "#32333d",
                BackgroundGrey = "#27272f",
                Surface = "#373740",
                DrawerBackground = "#27272f",
                DrawerText = "rgba(255,255,255, 0.50)",
                DrawerIcon = "rgba(255,255,255, 0.50)",
                AppbarBackground = "#27272f",
                AppbarText = "rgba(255,255,255, 0.70)",
                TextPrimary = "rgba(255,255,255, 0.70)",
                TextSecondary = "rgba(255,255,255, 0.50)",
                ActionDefault = "#adadb1",
                ActionDisabled = "rgba(255,255,255, 0.26)",
                ActionDisabledBackground = "rgba(255,255,255, 0.12)",
                Divider = "rgba(255,255,255, 0.12)",
                DividerLight = "rgba(255,255,255, 0.06)",
                TableLines = "rgba(255,255,255, 0.12)",
                LinesDefault = "rgba(255,255,255, 0.12)",
                LinesInputs = "rgba(255,255,255, 0.3)",
                TextDisabled = "rgba(255,255,255, 0.2)",
                Secondary = "#ff584f",
                Primary = "#3d6fb4"
            }
        };
        private bool _isInDarkMode;
        private bool _isOpen;
        private string _themeIcon = Icons.Material.Filled.LightMode;
        private MudTheme _currentTheme = DefaultTheme;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (_localStorage is not null)
                {
                    var applicationTheme = await _localStorage.GetItemAsync<string>(ApplicationThemeKey);
                    Console.WriteLine($"Current Theme: {applicationTheme}");
                    _isInDarkMode = applicationTheme == "dark";
                    SetTheme();
                }
                await InvokeAsync(StateHasChanged);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        private async Task SwitchTheme()
        {
            _isInDarkMode = !_isInDarkMode;
            SetTheme();
            await InvokeAsync(StateHasChanged);
            if (_localStorage is not null)
            {
                // TODO: write own code, because lib save string as "{value}"
                await _localStorage.SetItemAsync(ApplicationThemeKey, _isInDarkMode ? "dark" : "light");
            }
        }
        private void SetTheme()
        {
            if (_isInDarkMode)
            {
                _themeIcon = Icons.Material.Filled.DarkMode;
                _currentTheme = DarkTheme;
            }
            else
            {
                _themeIcon = Icons.Material.Filled.LightMode;
                _currentTheme = DefaultTheme;
            }
        }

        private void ToggleDrawer()
        {
            _isOpen = !_isOpen;
        }
    }
}