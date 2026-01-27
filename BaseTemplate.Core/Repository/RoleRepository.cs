using BaseTemplate.Core.Interface;
using BaseTemplate.Data.Contexts;
using BaseTemplate.Data.Models;

namespace BaseTemplate.Core.Repository {
    public class RoleRepository : Repository<RoleModel>, IRoleRepository {
        private readonly AppDbContext _db;

        public RoleRepository(AppDbContext db) : base(db) {
            _db = db;
        }
    }
}
