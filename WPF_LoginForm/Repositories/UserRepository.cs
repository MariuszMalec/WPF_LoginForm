using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using WPF_LoginForm.Model;

namespace WPF_LoginForm.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {

        private static IEnumerable<UserModel> Users = new List<UserModel>()
        {
            new UserModel() {Id=1, Email="mario@example.com",FirstName="Mariusz",LastName="Malec", UserName="qwerty", Password="qwerty" }
        };
        public IEnumerable<UserModel> GetAllUsers()
        {
            return Users;
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;

            var users = GetAllUsers();

            validUser = users.Where(u => u.UserName == credential.UserName).Where(u => u.Password == credential.Password).Any();
                             ;

            //using (var connection = GetConnection())
            //using (var command=new SqlCommand())
            //{
            //    connection.Open();
            //    command.Connection = connection;
            //    command.CommandText = "select *from [User] where username=@username and [Password]=@password";
            //    command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value=credential.UserName;
            //    command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = credential.Password;
            //    validUser = command.ExecuteScalar() == null ? false : true;
            //}

            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUserName(string userName)
        {
            UserModel user = null;

            var users = GetAllUsers();

            if (users.Any())
            {
                var userModel = users.Where(u=>u.UserName == userName).FirstOrDefault();
                return userModel;
            }

            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
