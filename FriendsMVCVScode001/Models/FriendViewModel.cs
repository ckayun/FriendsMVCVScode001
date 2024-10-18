using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsMVCVScode001.Models {
    public class FriendViewModel {
        public int Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
    }
}