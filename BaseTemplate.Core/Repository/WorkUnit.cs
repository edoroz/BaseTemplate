using BaseTemplate.Core.Interface;
using BaseTemplate.Data.Contexts;

namespace BaseTemplate.Core.Repository {
    public class WorkUnit : IWorkUnit {
        
        private readonly AppDbContext _db;
        public IUserRepository User { get; private set; }
        public IRoleRepository Role { get; private set; }

        public WorkUnit(AppDbContext db) {
            _db = db;
            User = new UserRepository(_db);
            Role = new RoleRepository(_db);
        }

        public void Dispose() {
            _db.Dispose();
        }

        public void Save() {
            _db?.SaveChanges();
        }
    }
}
