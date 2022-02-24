using System.Collections.Generic;
using Shop.Domain;
using System.Data;
using Shop.DAL;
using System.Linq;
using System;
using System.Data.Entity;
using Shop.Service.Cache;

namespace Shop.Service
{
    public class UserBLL : BLLBase
    {

        private ItblUserDAL _userDAL;

        public UserBLL(string connectionString = "") : base(connectionString)
        {
            _userDAL = new tblUserDAL(this.DatabaseFactory);
        }
        public List<UserModel> GetListUser()
        {
            var query = _userDAL.GetAll();
            var data = query.Select(k => new UserModel {
                    Id = k.Id,
                    Username = k.Username,
                    Password = k.Password,
                    Address = k.Address,
                    Age = k.Age
                }).ToList();
            return data;
        }
        public long AddUser(UserModel data)
        {
            var user = new tblUser();
            user.Username = data.Username;
            user.Password = data.Password;
            user.Address = data.Address;
            user.Age = data.Age;
            _userDAL.Add(user);
            this.SaveChanges();
            return user.Id;
        }

    }
}