using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace VisualStudioShell.Controls
{
  public sealed class MenuFlyoutImageItem : MenuFlyoutItem
  {
    public MenuFlyoutImageItem()
    {
      this.DefaultStyleKey = typeof(MenuFlyoutImageItem);
    }

    public ImageSource Icon
    {
      get { return (ImageSource)GetValue(IconProperty); }
      set { SetValue(IconProperty, value); }
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(ImageSource), typeof(MenuFlyoutImageItem), new PropertyMetadata(null));
  }
}
