using BeautySalonService.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonService.BusinessLayer.Helpers
{
    public static class ClientHelper
    {
        private static ClientDetialsModel _client;

        public static bool IsAuthorized { get; set; }
        public static ClientDetialsModel ClientDetails => _client;

        public static void Authorize(ClientDetialsModel client)
        {
            _client = client;
            IsAuthorized = true;
        }

        public static void UnAuthorize(int clientId)
        {
            if (_client.ClientId == clientId)
            {
                _client = null;
                IsAuthorized = false;
            }
        }
    }
}
