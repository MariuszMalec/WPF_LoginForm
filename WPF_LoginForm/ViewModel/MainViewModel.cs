using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = _userRepository.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
            if (user!=null)
            {
                CurrentUserAccount = new UserAccountModel()
                {
                    UserName = user.UserName,
                    DisplayName = $"Welcome {user.FirstName} {user.LastName}",
                    ProfilePicture=null
                };
            }
            else
            {
                MessageBox.Show("Invalid user, not logged in");
                Application.Current.Shutdown();
            }
        }
    }
}
