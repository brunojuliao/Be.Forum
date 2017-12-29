using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Be.Forum.MVC.Data;
using Be.Forum.MVC.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Be.Forum.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Be.Forum.MVC.Models.PostViewModels;

namespace Be.Forum.MVC.Controllers {
  [Authorize]
  public class PostController : Controller {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PostController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
      _context = context;
      _userManager = userManager;
    }

    // GET: Post
    public async Task<IActionResult> Index() {
      return View(await _context.Posts
        .Include(p => p.User)
        .Include(p => p.Children)
        .Where(p => p.ParentId == null)
        .OrderByDescending(p => p.Updated)
        .Select(p => new ListItemPostViewModel(p))
        .ToListAsync());
    }

    // GET: Post/Details/5
    public async Task<IActionResult> Details(int? id) {
      if (id == null) {
        return NotFound();
      }

      var post = await _context.Posts
          .Include(p => p.User)
          .Include(p => p.Children)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (post == null) {
        return NotFound();
      }

      var postView = new DetailPostViewModel(post);
      return View(postView);
    }

    // GET: Post/Create
    public IActionResult Create(int? ParentId) {
      ViewBag.ParentId = ParentId;
      return View();
    }

    // POST: Post/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int? ParentId, [Bind("Id,Title,Content")] PostViewModel postView) {
      ViewBag.ParentId = ParentId;

      if (!ModelState.IsValid)
        return View(postView);

      var post = new Post();
      postView.CopyDataToModel(post);
      post.Created = post.Updated = DateTime.Now;
      post.UserId = _userManager.GetUserId(User);
      post.ParentId = ParentId;

      _context.Add(post);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    // GET: Post/Edit/5
    public async Task<IActionResult> Edit(int? id) {
      if (id == null) {
        return NotFound();
      }

      var post = await _context.Posts.Select(p => new PostViewModel(p)).SingleOrDefaultAsync(m => m.Id == id);
      if (post == null) {
        return NotFound();
      }
      return View(post);
    }

    // POST: Post/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] PostViewModel postView) {
      if (id != postView.Id) {
        return NotFound();
      }

      if (!ModelState.IsValid)
        return View(postView);

      try {
        var post = await _context.Posts.Where(p => id.Equals(p.Id)).SingleOrDefaultAsync();

        if (post == null) {
          return NotFound();
        }

        if (_userManager.GetUserId(User) != post.UserId) {
          return Forbid();
        }

        postView.CopyDataToModel(post);
        post.Updated = DateTime.Now;

        _context.Update(post);
        await _context.SaveChangesAsync();
      } catch (DbUpdateConcurrencyException) {
        if (!PostExists(postView.Id)) {
          return NotFound();
        } else {
          throw;
        }
      }
      return RedirectToAction(nameof(Index));
    }

    // GET: Post/Delete/5
    public async Task<IActionResult> Delete(int? id) {
      if (id == null) {
        return NotFound();
      }

      var post = await _context.Posts
          .SingleOrDefaultAsync(m => m.Id == id);

      if (post == null) {
        return NotFound();
      }

      if (_userManager.GetUserId(User) != post.UserId) {
        return Forbid();
      }

      return View(new PostViewModel(post));
    }

    // POST: Post/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);

      if (_userManager.GetUserId(User) != post.UserId) {
        return Forbid();
      }

      _context.Posts.Remove(post);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool PostExists(int id) {
      return _context.Posts.Any(e => e.Id == id);
    }
  }
}
