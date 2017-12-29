using Be.Forum.MVC.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Be.Forum.MVC.Models.PostViewModels {
  public class PostViewModel {
    public PostViewModel() { }
    public PostViewModel(Post post) {
      CopyDataFromModel(post);
    }

    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; }

    [Required]
    [StringLength(1000)]
    [DataType(DataType.MultilineText)]
    public string Content { get; set; }

    public void CopyDataToModel(Post post) {
      post.Title = this.Title;
      post.Content = this.Content;
    }

    public void CopyDataFromModel(Post post) {
      this.Id = post.Id;
      this.Title = post.Title;
      this.Content = post.Content;
    }
  }
}
