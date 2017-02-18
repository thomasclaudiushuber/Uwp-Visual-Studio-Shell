using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace VisualStudioShell
{
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
      this.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(OnPointerPressed), true);

      SetTitleBar();
      StyleTitleBar();
      SetCodeContent();
      solutionExplorer.PropertyChanged += SolutionExplorer_PropertyChanged;

    }

    private void SetCodeContent()
    {
      richBoxMainPageCodebehind.Document.SetText(Windows.UI.Text.TextSetOptions.None, @"namespace VisualStudioShell
{
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
      this.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(OnPointerPressed), true);

      SetTitleBar();
      StyleTitleBar();
      SetCodeContent();
      solutionExplorer.PropertyChanged += SolutionExplorer_PropertyChanged;
      
    }
...
}");

      richBoxMainPage.Document.SetText(Windows.UI.Text.TextSetOptions.None, @"Nothing here yet. Don't forget to write your code in the real Visual Studio, not in this one.");
    }

    private void SolutionExplorer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(solutionExplorer.IsPinned))
      {
        if (solutionExplorer.IsPinned)
        {
          mainGrid.ColumnDefinitions.Insert(2, columnDefinitionForPinnedSolutionExplorer);
          btnSolutionExplorer.Visibility = Visibility.Collapsed;
        }
        else
        {
          mainGrid.ColumnDefinitions.Remove(columnDefinitionForPinnedSolutionExplorer);
          btnSolutionExplorer.Visibility = Visibility.Visible;
          solutionExplorer.Visibility = Visibility.Collapsed;
        }
      }
    }

    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
      HideElementIfNotPressedInside(toolBox, e);
      HideElementIfNotPressedInside(liveVisualTree, e);
      HideElementIfNotPressedInside(notifications, e);
      HideElementIfNotPressedInside(diagnosticTools, e);

      if (!solutionExplorer.IsPinned)
      {
        HideElementIfNotPressedInside(solutionExplorer, e);
      }
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
      else if (sender == btnSolutionExplorer)
      {
        solutionExplorer.Visibility = Visibility.Visible;
      }
    }
  }
}
