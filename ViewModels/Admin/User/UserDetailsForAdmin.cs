using System;

namespace BeautySalonService.ViewModels.Admin.User
{
    public sealed class UserDetailsForAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int BooksCount { get; set; }
        public string MobileNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string Role { get; set; }
    }
}
