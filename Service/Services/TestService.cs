using Entitys.Interface;
using Service.Interface.Test;

namespace Service.Services
{
    public class TestService : ITestService
    {

        public IDbPool DbPool { get; set; }

        public int GetAll()
        {
            return DbPool == null ? 0 : 1;
        }
    }
}