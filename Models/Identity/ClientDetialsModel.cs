namespace BeautySalonService.Models.Identity
{
    public sealed class ClientDetialsModel
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public RoleDetailsModel Role { get; set; }
    }
}
