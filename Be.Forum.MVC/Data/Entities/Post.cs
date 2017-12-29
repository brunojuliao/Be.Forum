using Be.Forum.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Be.Forum.MVC.Data.Entities {
  public class Post {
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; }

    [Required]
    [StringLength(1000)]
    [DataType(DataType.MultilineText)]
    public string Content { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public DateTime Updated { get; set; }

    [Required]
    public string UserId { get; set; }

    public int? ParentId { get; set; }

    public Post Parent { get; set; }
    public List<Post> Children { get; set; }
    public ApplicationUser User { get; set; }
  }
}