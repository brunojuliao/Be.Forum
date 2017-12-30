using Be.Forum.MVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Be.Forum.MVC.Models.PostViewModels {
  public class DetailPostViewModel : PostViewModel {
    public DetailPostViewModel() : base() { }
    public DetailPostViewModel(Post post) : base(post) {
      this.CopyDataFromModel(post);
    }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string UserId { get; set; }
    public string User { get; set; }

    public List<DetailPostViewModel> Children { get; set; }

    public new void CopyDataFromModel(Post post) {
      if (string.IsNullOrEmpty(this.Title))
        base.CopyDataFromModel(post);

      this.UserId = post.UserId;
      this.User = post.User?.Nickname ?? post.User?.UserName;
      this.Created = post.Created;
      this.Updated = post.Updated;

      this.Children = (post.Children ?? new List<Post>()).Select(p => new DetailPostViewModel(p)).ToList();
    }
  }
}
