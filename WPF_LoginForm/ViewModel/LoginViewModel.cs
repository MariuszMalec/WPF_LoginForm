using System;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Windows.Input;
using WPF_LoginForm.Model;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _userName;
        private SecureString _password;
        private string _email;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository _userRepository;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(nameof(UserName)); }
        }
        public SecureString Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        //commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        //constructor
        public LoginViewModel()
        {
            _userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPasswordCommand("",""));
        }

        private void ExecuteRecoverPasswordCommand(string userName, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validateData;
            if (string.IsNullOrWhiteSpace(UserName) || UserName.Length < 3 || Password == null || Password.Length < 3)
                validateData = false;
            else
                validateData = true;
            return validateData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            //https://youtu.be/FGqj4q09NtA?t=1529

            var isValidUser = _userRepository.AuthenticateUser(new System.Net.NetworkCredential(UserName,Password));

            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(UserName), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "*Invalid user name or password!";
            }
            
        }
    }

}
