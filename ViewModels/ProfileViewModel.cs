namespace BeautySalonService.ViewModels
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
