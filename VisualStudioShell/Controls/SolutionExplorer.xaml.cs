using System.ComponentModel;
using TreeViewControl;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VisualStudioShell.Controls
{
  public sealed partial class SolutionExplorer : UserControl, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;
    private bool _isPinned = true;

    public SolutionExplorer()
    {
      this.InitializeComponent();
      this.Loaded += SolutionExplorer_Loaded;
    }

    public bool IsPinned
    {
      get { return _isPinned; }
      set
      {
        _isPinned = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPinned)));
      }
    }

    private void SolutionExplorer_Loaded(object sender, RoutedEventArgs e)
    {
      BuildTreeView();
    }

    private void BuildTreeView()
    {

      var rootNode = CreateTreeNode("Solution 'VisualStudioShell'", false, "Solution.png");
      var uiProjectNode = CreateUIProjectNode();
      var controlsProjectNode = CreateControlsProjectNode();
      rootNode.Add(uiProjectNode);
      rootNode.Add(controlsProjectNode);

      rootNode.IsExpanded = true;

      treeView.RootNode.Add(rootNode);
    }

    private TreeNode CreateUIProjectNode()
    {
      var projectRootNode = CreateTreeNode("VisualStudioShell (Universal Windows)", false, "ProjectCSharp.png");
      var referencesUI = CreateTreeNode("References", false, "References.png");
      referencesUI.Add(CreateTreeNode("TreeViewControl", false, "References.png"));
      referencesUI.Add(CreateTreeNode("Universal Windows", false, "References.png"));
      referencesUI.Add(CreateTreeNode("VisualStudioShell.Controls", false, "References.png"));
      projectRootNode.Add(referencesUI);


      var converter = CreateTreeNode("Converter", true);
      converter.Add(CreateTreeNode("BooleanToVisibilityConverter", false, "FileCs.png"));
      converter.Add(CreateTreeNode("GlyphConverter", false, "FileCs.png"));
      projectRootNode.Add(converter);

      var appXaml = CreateTreeNode("App.xaml", false, "FileXaml.png");
      appXaml.Add(CreateTreeNode("App.xaml.cs", false, "FileXamlCs.png"));
      projectRootNode.Add(appXaml);

      var mainXaml = CreateTreeNode("Main.xaml", false, "FileXaml.png");
      mainXaml.Add(CreateTreeNode("Main.xaml.cs", false, "FileXamlCs.png"));
      projectRootNode.Add(mainXaml);


      projectRootNode.IsExpanded = true;
      referencesUI.IsExpanded = true;
      converter.IsExpanded = true;
      appXaml.IsExpanded = true;
      mainXaml.IsExpanded = true;

      return projectRootNode;
    }

    private TreeNode CreateControlsProjectNode()
    {
      var projectRootNode = CreateTreeNode("VisualStudioShell.Controls (Universal Windows)", false, "ProjectCSharp.png");
      var referencesControls = CreateTreeNode("References", false, "References.png");
      referencesControls.Add(CreateTreeNode("Universal Windows", false, "References.png"));
      projectRootNode.Add(referencesControls);

      var themesNode = CreateTreeNode("Themes", true);
      themesNode.Add(CreateTreeNode("Generic.xaml", false, "FileXaml.png"));
      projectRootNode.Add(themesNode);

      projectRootNode.Add(CreateTreeNode("MenuFlyoutImageItem", false, "FileCs.png"));

      return projectRootNode;
    }

    private TreeNode CreateTreeNode(string name, bool isFolder, string image = null)
    {
      return new TreeNode
      {
        Data = new TreeNodeData(name, isFolder, image)
      };
    }

    private void SolutionExplorerPinButton_Click(object sender, RoutedEventArgs e)
    {
      IsPinned = !IsPinned;
      imgPinned.Visibility = IsPinned ? Visibility.Visible : Visibility.Collapsed;
      imgUnpinned.Visibility = !IsPinned ? Visibility.Visible : Visibility.Collapsed;
    }
  }
}
