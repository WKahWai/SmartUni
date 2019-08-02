using SmartUni.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartUni.Data
{
    public class SmartUniContext : DbContext
    {
        public SmartUniContext(DbContextOptions<SmartUniContext> options) : base(options)
        {
        }

    }
}
