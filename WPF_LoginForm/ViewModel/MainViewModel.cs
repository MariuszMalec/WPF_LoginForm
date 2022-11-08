using FontAwesome.Sharp;
using System.Threading;
using System.Windows.Input;
using WPF_LoginForm.Model;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        //fields
        private UserAccountModel _currentuserAccount{ get; set; }

        //https://youtu.be/76JLBZJR5gE?t=1002
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;


        private IUserRepository _userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get { return _currentuserAccount; }
            set { _currentuserAccount = value; OnPropertyChanged(nameof(CurrentUserAccount)); }
        }

        public ViewModelBase CurrentChildView 
        {
            get { return _currentChildView; }
            set { _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); }
        }
        public string Caption 
        {
            get { return _caption; }
            set { _caption = value; OnPropertyChanged(nameof(Caption)); }
        }
        public IconChar Icon 
        {
            get { return _icon; }
            set { _icon = value; OnPropertyChanged(nameof(Icon)); }
        }

        //commands
        public ICommand ShowHomeViewCommand { get;}
        public ICommand ShowCustomerViewCommand { get; }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);

            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Customers";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {

            //Thread.CurrentPrincipal.Identity.Name//TODO to anuluje nie wiem czemu jak wrzucam do getbyusername

            var user = _userRepository.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
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
