using EntityContext;
using System;

namespace BusinessLogic
{
    public class BusinessBase : IDisposable
    {
        public BusinessBase(TestContext context)
        {
            _db = context;
        }
        private TestContext _db;
        protected TestContext Database
        {
            get
            {
                return _db;
            }
        }
        public void Dispose()
        {
            //_db?.Dispose();
        }
    }
}
