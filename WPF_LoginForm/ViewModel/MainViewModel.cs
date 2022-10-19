using System.Threading;
using WPF_LoginForm.Model;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        //fields
        private UserAccountModel _currentuserAccount{ get; set; }
        private IUserRepository _userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get { return _currentuserAccount; }
            set { _currentuserAccount = value; OnPropertyChanged(nameof(CurrentUserAccount)); }
        }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {

            //Thread.CurrentPrincipal.Identity.Name//TODO to nuluje nie wiem czemu jak wrzucam do getbyusername

            var user = _userRepository.GetByUserName("");
            if (user!=null)
            {
                CurrentUserAccount.UserName = user.UserName;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                //https://youtu.be/FGqj4q09NtA?t=2125
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                //Hide;
            }
        }
    }
}
