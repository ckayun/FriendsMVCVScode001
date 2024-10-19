using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsWebAPIVScode001.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FriendsWebAPIVScode001.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FriendController : Controller {
        private readonly FriendDbContext _context;
        public FriendController(FriendDbContext friendsDbContext) {
            _context = friendsDbContext;
        }
        // GET:FriendController
        // [HttpGet]
        // public async Task<ActionResult> Index() {
        //     var friendList = await _context.Friends.ToListAsync();
        //     return View(friendList);
        // }
        [HttpGet]
        public async Task<IEnumerable<FriendViewModel>> Get() {
            return await _context.Friends.ToListAsync();
        }
        // GET:FriendController/Edit/4
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) {
            try {
                var friend = await _context.Friends.SingleOrDefaultAsync(f => f.Id == id);
                if (friend == null) {
                    return NotFound("Id: " + id);
                }
                return Ok(friend);
            } catch (Exception ex) {
                string strError = "Message: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace;
                return BadRequest(strError);
            }
        }
        // // GET:FriendController/Create
        // [HttpGet]
        // public async Task<ActionResult> Create() {
        //     return View();
        // }
        // [HttpPost]
        // public async Task<ActionResult> Create(FriendViewModel addFriendViewModel) {
        //     try {
        //         FriendViewModel friendViewModel = new FriendViewModel() {
        //             Id = addFriendViewModel.Id,
        //             Firstname = addFriendViewModel.Firstname,
        //             Lastname = addFriendViewModel.Lastname,
        //             Mobile = addFriendViewModel.Mobile,
        //             Email = addFriendViewModel.Email
        //         };
        //         await _context.Friends.AddAsync(friendViewModel);
        //         await _context.SaveChangesAsync();
        //         TempData["successMessage"] = "New Friend Created.";
        //         return RedirectToAction(nameof(Index));
        //     } catch (Exception ex) {
        //         TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
        //         return View();
        //     }
        // }
        [HttpPost]
        public async Task<ActionResult> Post(FriendViewModel addFriendViewModel) {
            try {
                FriendViewModel friendViewModel = new FriendViewModel() {
                    Firstname = addFriendViewModel.Firstname,
                    Lastname = addFriendViewModel.Lastname,
                    Mobile = addFriendViewModel.Mobile,
                    Email = addFriendViewModel.Email
                };
                await _context.Friends.AddAsync(friendViewModel);
                await _context.SaveChangesAsync();
                return Ok("Friend added: " + friendViewModel.Firstname + " " + friendViewModel.Lastname);
            } catch (Exception ex) {
                string strError = "Message: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace;
                return BadRequest(strError);
            }
        }
        // // GET:FriendController/Edit/4
        // [HttpGet]
        // public async Task<ActionResult> Edit(int id) {
        //     try {
        //         var friend = await _context.Friends.SingleOrDefaultAsync(f => f.Id == id);
        //         return View(friend);
        //     } catch (Exception ex) {
        //         TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
        //         return View();
        //     }
        // }
        // [HttpPost]
        // public async Task<ActionResult> Edit(FriendViewModel friendViewModel) {
        //     try {
        //         var friend = await _context.Friends.SingleOrDefaultAsync(f => f.Id == friendViewModel.Id);
        //         if (friend == null) {
        //             TempData["errorMessage"] = $"Friend Not Found with Id {friend.Id}";
        //             return View();
        //         } else {
        //             friend.Firstname = friendViewModel.Firstname;
        //             friend.Lastname = friendViewModel.Lastname;
        //             friend.Mobile = friendViewModel.Mobile;
        //             friend.Email = friendViewModel.Email;
        //             await _context.SaveChangesAsync();
        //             TempData["successMessage"] = "Friend Record was Edited.";
        //             return RedirectToAction(nameof(Index));
        //         }
        //     } catch (Exception ex) {
        //         TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
        //         return View();
        //     }
        // }
        [HttpPut]
        public async Task<ActionResult> Put(FriendViewModel editFriendViewModel) {
            try {
                var friendViewMode = await _context.Friends.SingleOrDefaultAsync(f => f.Id == editFriendViewModel.Id);
                if (friendViewMode == null) {
                    string strNotFound = "Friend Not Found with Id: " + editFriendViewModel.Id;
                    return BadRequest(strNotFound);
                } else {
                    friendViewMode.Firstname = editFriendViewModel.Firstname;
                    friendViewMode.Lastname = editFriendViewModel.Lastname;
                    friendViewMode.Mobile = editFriendViewModel.Mobile;
                    friendViewMode.Email = editFriendViewModel.Email;
                    await _context.SaveChangesAsync();
                    return Ok("Friend edited: " + friendViewMode.Firstname + " " + friendViewMode.Lastname);
                }
            } catch (Exception ex) {
                string strError = "Message: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace;
                return BadRequest(strError);
            }
        }
        // // GET:FriendController/Delete/4
        // [HttpGet]
        // public async Task<ActionResult> Delete(int id) {
        //     try {
        //         var friend = await _context.Friends.SingleOrDefaultAsync(f => f.Id == id);
        //         return View(friend);
        //     } catch (Exception ex) {
        //         TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
        //         return View();
        //     }
        // }
        // [HttpPost]
        // public async Task<ActionResult> Delete(FriendViewModel friendViewModel) {
        //     try {
        //         var friend = await _context.Friends.SingleOrDefaultAsync(f => f.Id == friendViewModel.Id);
        //         if (friend == null) {
        //             TempData["errorMessage"] = $"Friend Not Found with Id {friend.Id}";
        //             return View();
        //         } else {
        //             _context.Friends.Remove(friend);
        //             await _context.SaveChangesAsync();
        //             TempData["successMessage"] = "Friend Record was Deleted.";
        //             return RedirectToAction(nameof(Index));
        //         }
        //     } catch (Exception ex) {
        //         TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
        //         return View();
        //     }
        // }

        [HttpDelete]
        public async Task<ActionResult> Delete(FriendViewModel deleteFriendViewModel) {
            try {
                var friendViewModel = await _context.Friends.SingleOrDefaultAsync(f => f.Id == deleteFriendViewModel.Id);
                if (friendViewModel == null) {
                    string strNotFound = "Friend Not Found with Id: " + deleteFriendViewModel.Id;
                    return BadRequest(strNotFound);
                } else {
                    _context.Friends.Remove(friendViewModel);
                    await _context.SaveChangesAsync();
                    return Ok("Friend deleted: " + friendViewModel.Firstname + " " + friendViewModel.Lastname);
                }
            } catch (Exception ex) {
                TempData["errorMessage"] = ex.Message + "<br/>" + ex.StackTrace;
                return View();
            }
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error() {
        //     return View("Error!");
        // }
    }
}