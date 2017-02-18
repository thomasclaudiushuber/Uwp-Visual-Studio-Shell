using TreeViewControl;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace VisualStudioShell.Controls
{
  public sealed partial class LiveVisualTree : UserControl
  {
    public LiveVisualTree()
    {
      this.InitializeComponent();
      this.Loaded += LiveVisualTree_Loaded;
    }

    private void LiveVisualTree_Loaded(object sender, RoutedEventArgs e)
    {
      var rootElement = Window.Current.Content;
      int level = 0;
      var rootNode = new TreeNode { Data = new TreeNodeDataBase(rootElement.GetType().Name), IsExpanded = true };
      AddChildren(rootNode, rootElement,level);

      treeView.RootNode.Add(rootNode);
    }

    private void AddChildren(TreeNode parentNode, UIElement parentElement, int level)
    {
      level++;
      var count = VisualTreeHelper.GetChildrenCount(parentElement);
      for (int i = 0; i < count; i++)
      {
        var childElement = VisualTreeHelper.GetChild(parentElement, i) as UIElement;
        if (childElement != null)
        {
          var childNode = new TreeNode { Data = new TreeNodeDataBase(ElementToString(childElement)) };

          // Don't expand all, as it takes ages. :-)
          if(level<6)
          {
            childNode.IsExpanded = true;
          }
          parentNode.Add(childNode);
          AddChildren(childNode, childElement, level);
        }
      }
    }

    private string ElementToString(UIElement childElement)
    {
      var s = "";

      var f = childElement as FrameworkElement;
      if (f != null)
      {
        s = f.Name + " ";
      }
      s += $"[{childElement.GetType().Name}]";

      return s;
    }
  }
}
