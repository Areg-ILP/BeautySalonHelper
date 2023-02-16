using BeautySalonService.ViewModels.Base;

namespace BeautySalonService.ViewModels.Identity
{
    public sealed class ProfileViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string RoleName { get; set; }
    }
}
