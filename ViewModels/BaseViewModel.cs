namespace BeautySalonService.ViewModels
{
    public abstract class BaseViewModel
    {
        public bool UseHeader { get; set; } = true;
        public bool UseFooter { get; set; } = true;
        public bool UseGlobalCss { get; set; } = true;
        public bool UseIdentityCss { get; set; } = false;
        public bool UseErrorCss { get; set; } = false;
        public bool UseAdminCss { get; set; } = false;
    }
}
