using BaseTemplate.Core.Interface;
using BaseTemplate.Data.Contexts;
using BaseTemplate.Data.Models;

namespace BaseTemplate.Core.Repository {
    internal class UserRepository :Repository<UserModel>, IUserRepository {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db) : base(db) {
            _db = db;
        }
    }
}
