using GiveAwayInsider_API.Service.IService;
using GiveAwayInsider_DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace GiveAwayInsider_API.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;

        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
