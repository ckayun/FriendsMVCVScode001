using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsMVCVScode001.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FriendsMVCVScode001.Controllers {
    [Route("[controller]/{action=Index}/{Id=0}")]
    public class FriendController : Controller {
        private readonly FriendDbContext _context;
        public FriendController(FriendDbContext friendsDbContext) {
            _context = friendsDbContext;
        }
        // GET:FriendController
        [HttpGet]
        public async Task<ActionResult> Index() {
            var friendList = await _context.Friends.ToListAsync();
            return View(friendList);
        }
        // GET:FriendController/Create
        [HttpGet]
        public async Task<ActionResult> Create() {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(FriendViewModel addFriendViewModel) {
            try {
                FriendViewModel friendViewModel = new FriendViewModel() {
                    Id = addFriendViewModel.Id,
                    Firstname = addFriendViewModel.Firstname,
                    Lastname = addFriendViewModel.Lastname,
                    Mobile = addFriendViewModel.Mobile,
                    Email = addFriendViewModel.Email
                };
                await _context.Friends.AddAsync(friendViewModel);
                await _context.SaveChangesAsync();
                TempData["successMessage"] = "New Friend Created.";
                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
                return View();
            }
        }
        // GET:FriendController/Edit/4
        [HttpGet]
        public async Task<ActionResult> Edit(int id) {
            try {
                var friend = await _context.Friends.SingleOrDefaultAsync(f => f.Id == id);
                return View(friend);
            } catch (Exception ex) {
                TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(FriendViewModel friendViewModel) {
            try {
                var friend = await _context.Friends.SingleOrDefaultAsync(f => f.Id == friendViewModel.Id);
                if (friend == null) {
                    TempData["errorMessage"] = $"Friend Not Found with Id {friend.Id}";
                    return View();
                } else {
                    friend.Firstname = friendViewModel.Firstname;
                    friend.Lastname = friendViewModel.Lastname;
                    friend.Mobile = friendViewModel.Mobile;
                    friend.Email = friendViewModel.Email;
                    await _context.SaveChangesAsync();
                    TempData["successMessage"] = "Friend Record was Edited.";
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception ex) {
                TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
                return View();
            }
        }
        // GET:FriendController/Delete/4
        [HttpGet]
        public async Task<ActionResult> Delete(int id) {
            try {
                var friend = await _context.Friends.SingleOrDefaultAsync(f => f.Id == id);
                return View(friend);
            } catch (Exception ex) {
                TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Delete(FriendViewModel friendViewModel) {
            try {
                var friend = await _context.Friends.SingleOrDefaultAsync(f => f.Id == friendViewModel.Id);
                if (friend == null) {
                    TempData["errorMessage"] = $"Friend Not Found with Id {friend.Id}";
                    return View();
                } else {
                    _context.Friends.Remove(friend);
                    await _context.SaveChangesAsync();
                    TempData["successMessage"] = "Friend Record was Deleted.";
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception ex) {
                TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
                return View();
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View("Error!");
        }
    }
}