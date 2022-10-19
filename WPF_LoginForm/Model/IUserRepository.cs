using System.Collections.Generic;
using System.Net;

namespace WPF_LoginForm.Model
{
    public interface IUserRepository
    {
        //https://youtu.be/FGqj4q09NtA?t=1300
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetById(int id);
        UserModel GetByUserName(string? userName);
        IEnumerable<UserModel> GetAll();
    }
}
