using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TreeViewControl;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace VisualStudioShell.Controls
{
  public sealed partial class SolutionExplorer : UserControl
  {
    public SolutionExplorer()
    {
      this.InitializeComponent();
      this.Loaded += SolutionExplorer_Loaded;
    }

    private void SolutionExplorer_Loaded(object sender, RoutedEventArgs e)
    {
      BuildTreeView();
    }

    private void BuildTreeView()
    {
      
      var rootNode = CreateTreeNode("Solution 'VisualStudioShell'", false,"Solution.png");
      var csharpProjectUI = CreateTreeNode("VisualStudioShell (Universal Windows)", false, "ProjectCSharp.png");
      var csharpProjectControls = CreateTreeNode("VisualStudioShell.Controls (Universal Windows)", false, "ProjectCSharp.png");
      rootNode.Add(csharpProjectUI);
      rootNode.Add(csharpProjectControls);

      var referencesUI = CreateTreeNode("References", false, "References.png");
      referencesUI.Add(CreateTreeNode("TreeViewControl", false, "References.png"));
      referencesUI.Add(CreateTreeNode("Universal Windows", false, "References.png"));
      referencesUI.Add(CreateTreeNode("VisualStudioShell.Controls", false, "References.png"));
      csharpProjectUI.Add(referencesUI);

      var referencesControls = CreateTreeNode("References", false, "References.png");
      referencesControls.Add(CreateTreeNode("Universal Windows", false, "References.png"));
      csharpProjectControls.Add(referencesControls);


      var converter = CreateTreeNode("Converter", true);
      converter.Add(CreateTreeNode("BooleanToVisibilityConverter", false, "FileCs.png"));
      converter.Add(CreateTreeNode("GlyphConverter", false, "FileCs.png"));
      csharpProjectUI.Add(converter);

      var appXaml = CreateTreeNode("App.xaml", false, "FileXaml.png");
      appXaml.Add(CreateTreeNode("App.xaml.cs", false, "FileXamlCs.png"));
      csharpProjectUI.Add(appXaml);

      var mainXaml = CreateTreeNode("Main.xaml", false, "FileXaml.png");
      mainXaml.Add(CreateTreeNode("Main.xaml.cs", false, "FileXamlCs.png"));
      csharpProjectUI.Add(mainXaml);


      rootNode.IsExpanded = true;
      csharpProjectUI.IsExpanded = true;
      
      treeView.RootNode.Add(rootNode);
    }

    private TreeNode CreateTreeNode(string name, bool isFolder, string image = null)
    {
      return new TreeNode
      {
        Data = new TreeNodeData(name,isFolder, image)
      };
    }
  }
  public class TreeNodeData
  {
    public TreeNodeData(string name, bool isFolder, string image = null)
    {
      Name = name;
      IsFolder = isFolder;
      ImagePath = "/Images/SolutionExplorer/"+ image;
    }
    public string ImagePath { get; set; }
    public string Name { get; set; }
    public bool IsFolder { get; set; }
  }
}
