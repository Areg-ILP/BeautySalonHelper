using BeautySalonService.Models.Identity;

namespace BeautySalonService.BusinessLayer.Helpers
{
    public static class ClientHelper
    {
        public static bool IsAuthorized { get; set; }
        public static ClientDetialsModel ClientDetails { get; set; }

        public static void Authorize(ClientDetialsModel client)
        {
            ClientDetails = client;
            IsAuthorized = true;
        }

        public static void UnAuthorize(int clientId)
        {
            if (ClientDetails.ClientId == clientId)
            {
                ClientDetails = null;
                IsAuthorized = false;
            }
        }
    }
}
