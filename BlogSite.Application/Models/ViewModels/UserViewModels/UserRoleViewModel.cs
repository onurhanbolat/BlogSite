namespace BlogSite.Application.Models.ViewModels.UserViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; } = new();
        public bool IsDeletable { get; set; } = true;
    }

}
