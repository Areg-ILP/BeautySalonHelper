using BeautySalonService.Models.Identity;
using System.Collections;
using System.Collections.Generic;

namespace BeautySalonService.ViewModels
{
    public sealed class AdminViewModel : BaseViewModel
    {
        public List<ClientDetialsModel> ClientsDetails { get; set; }
    }
}
