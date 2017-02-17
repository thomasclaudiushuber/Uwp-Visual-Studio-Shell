using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace VisualStudioShell
{
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
      var brush = (SolidColorBrush)App.Current.Resources["VsMainBackground"];
      var titleBar = ApplicationView.GetForCurrentView().TitleBar;
      if (titleBar != null)
      {
        titleBar.ButtonBackgroundColor = brush.Color;
        titleBar.ButtonForegroundColor = Colors.White;
        titleBar.BackgroundColor = brush.Color;
        titleBar.ForegroundColor = Colors.White;
      }
    }
  }
}
