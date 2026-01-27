using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Core.Interface {
    public interface IWorkUnit : IDisposable 
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }

        void Save();
    }
}
