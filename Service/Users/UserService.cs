using Core.Data;
using Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.User
{
    public partial class UserService : IUserService
    {
        private readonly IRepository<Users> _userRepository;

        public UserService(IRepository<Users> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Deleteuser(Users user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.Status = true;
            _userRepository.Update(user);
        }

        public Users GetUserById(long userId)
        {
            if (userId == 0)
            {
                return null;

            }
            return _userRepository.GetById(userId);
        }

        public Users IsValidUser(string username, string password)
        {
            Users user = GetAllUserQueryable().Where<Users>(u => u.Username == username && u.Password == password).FirstOrDefault();
            return user;
        }

        public bool IsUserNameExists(string username)
        {
            Users user = GetAllUserQueryable().Where<Users>(u => u.Username == username ).FirstOrDefault();
            if(user!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<Users> GetAllUser()
        {
            return _userRepository.Table.ToList<Users>();
        }

        public Users InsertUser(Users user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            _userRepository.Insert(user);
            return user;
        }

        public Users UpdateUser(Users user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
           
            _userRepository.Update(user);
            return user;
        }

        public IQueryable<Users> GetAllUserQueryable()
        {
            return _userRepository.Table;
        }

        public bool CheckDuplicateUserName(string username, string password)
        {
            throw new NotImplementedException();
        }


        public Users GetUserByUserName(string username)
        {
            Users user = GetAllUserQueryable().Where<Users>(u => u.Username == username).FirstOrDefault();
            return user;
        }
    }
}
