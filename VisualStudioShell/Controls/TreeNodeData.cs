namespace VisualStudioShell.Controls
{
  public class TreeNodeDataBase
  {
    public TreeNodeDataBase(string name)
    {
      Name = name;
    }
    public string Name { get; set; }
  }
  public class TreeNodeData : TreeNodeDataBase
  {
    public TreeNodeData(string name, bool isFolder, string image = null) : base(name)
    {
      IsFolder = isFolder;
      ImagePath = "/Images/SolutionExplorer/" + image;
    }
    public string ImagePath { get; set; }
    public bool IsFolder { get; set; }
  }
}
