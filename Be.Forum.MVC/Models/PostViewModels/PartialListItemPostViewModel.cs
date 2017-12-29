namespace Be.Forum.MVC.Models.PostViewModels {
  public class PartialListItemPostViewModel {
    public PartialListItemPostViewModel() { }
    public PartialListItemPostViewModel(int index, ListItemPostViewModel item) {
      this.Index = index;
      this.Item = item;
    }

    public int Index { get; set; }
    public ListItemPostViewModel Item { get; set; }
  }
}
