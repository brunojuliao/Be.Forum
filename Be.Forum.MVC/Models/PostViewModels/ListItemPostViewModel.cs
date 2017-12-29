using Be.Forum.MVC.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Be.Forum.MVC.Models.PostViewModels {
  public class ListItemPostViewModel {
    public ListItemPostViewModel() { }
    public ListItemPostViewModel(Post post) {
      this.CopyDataFromModel(post);
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string UserId { get; set; }
    public string User { get; set; }

    [Display(Name = "Replies")]
    public int ChildrenCount { get; set; }

    public void CopyDataFromModel(Post post) {
      this.Id = post.Id;
      this.Title = post.Title;
      this.UserId = post.UserId;
      this.User = post.User.Nickname ?? post.User.UserName;
      this.Created = post.Created;
      this.Updated = post.Updated;
      this.ChildrenCount = post.Children.Count;
    }
  }
}
