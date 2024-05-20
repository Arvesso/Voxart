using MudBlazor;

namespace Voxart.Client.Static
{
    public static class ThemeConfig
    {
        public static readonly MudTheme Theme;
        static ThemeConfig()
        {
            Theme = new();

            Theme.Typography.Default.FontFamily = ["MontserratBold"];
            Theme.Typography.H1.FontSize = "4.4rem";

            Theme.PaletteDark.TextPrimary = "#FFFFFF";
            Theme.PaletteDark.DrawerIcon = "#FFFFFF";
        }
    }
}
