using System;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace VisualStudioShell
{
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
      CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;
      SetTitleBar();
      StyleTitleBar();
    }

    private void SetTitleBar()
    {
      var titleBar = CoreApplication.GetCurrentView().TitleBar;
      titleBar.ExtendViewIntoTitleBar = true;

      Window.Current.SetTitleBar(backgroundElement);
    }

    private static void StyleTitleBar()
    {
      var brush = (SolidColorBrush)App.Current.Resources["VsMainBackground"];
      var appView = ApplicationView.GetForCurrentView();
      var appViewTitleBar = appView.TitleBar;
      if (appViewTitleBar != null)
      {
        appViewTitleBar.ButtonBackgroundColor = brush.Color;
        appViewTitleBar.ButtonForegroundColor = Colors.White;
        appViewTitleBar.BackgroundColor = brush.Color;
        appViewTitleBar.ForegroundColor = Colors.White;
      }
    }
  }
}
