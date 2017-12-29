namespace Be.Forum.MVC.Models.PostViewModels {
  public class PartialDetailPostViewModel {
    public PartialDetailPostViewModel() { }
    public PartialDetailPostViewModel(int index, DetailPostViewModel item) {
      this.Index = index;
      this.Item = item;
    }

    public int Index { get; set; }
    public DetailPostViewModel Item { get; set; }
  }
}
