using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FriendsWebAPIVScode001.Models {
    public class FriendDbContext : DbContext {
        public FriendDbContext(DbContextOptions<FriendDbContext> options) : base(options) { }
        // Friends is Db's Table Name
        public DbSet<FriendViewModel> Friends { get; set; }
    }
}