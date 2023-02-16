using BeautySalonService.Models.Identity;
using BeautySalonService.ViewModels.Base;
using System.Collections.Generic;

namespace BeautySalonService.ViewModels.Admin.User
{
    public sealed class AdminUserIndexViewModel : BaseViewModel
    {
        public List<ClientDetialsModel> ClientsDetails { get; set; }
    }
}
