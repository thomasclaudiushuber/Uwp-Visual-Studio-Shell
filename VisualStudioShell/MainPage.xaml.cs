using System;
using System.Linq;
using VisualStudioShell.Controls;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace VisualStudioShell
{
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
      CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;
      this.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(OnPointerPressed), true);

      SetTitleBar();
      StyleTitleBar();
    }


    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
      HideElementIfNotPressedInside(toolBox, e);
      HideElementIfNotPressedInside(liveVisualTree, e);
      HideElementIfNotPressedInside(notifications, e);
      HideElementIfNotPressedInside(diagnosticTools, e);
    }

    private void HideElementIfNotPressedInside(UIElement elementToHide, PointerRoutedEventArgs e)
    {
      if (elementToHide.Visibility == Visibility.Visible)
      {
        var point = e.GetCurrentPoint(this).Position;
        var elements = VisualTreeHelper.FindElementsInHostCoordinates(point, this);

        // If the Toolbox itself is not clicked, hide it
        if (elements.All(el => el != elementToHide))
        {
          elementToHide.Visibility = Visibility.Collapsed;
        }
      }
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

    private void SidebarButton_Click(object sender, RoutedEventArgs e)
    {
      if (sender == btnToolbox)
      {
        toolBox.Visibility = Visibility.Visible;
      }
      else if (sender == btnLiveVisualTree)
      {
        liveVisualTree.Visibility = Visibility.Visible;
      }
      else if (sender == btnNotifications)
      {
        notifications.Visibility = Visibility.Visible;
      }
      else if (sender == btnDiagnosticTools)
      {
        diagnosticTools.Visibility = Visibility.Visible;
      }
    }
  }
}
