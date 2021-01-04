using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Business.Session
{
    public interface ISessionStorage <T>
    {
        public void Save(string key, T model);

        public T Get(string key);
    }
}
