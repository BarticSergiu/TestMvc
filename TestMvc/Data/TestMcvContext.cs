using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestMvc.Models;

namespace TestMvc.Data
{
    public class TestMvcContext : DbContext
    {
        public TestMvcContext(DbContextOptions<TestMvcContext> options)
            : base(options)
        {
        }

        public DbSet<TestMvc.Models.Persoana> Persoana { get; set; } = default!;

        public DbSet<TestMvc.Models.Sarcina> Sarcina { get; set; }

        public DbSet<TestMvc.Models.Pontaj> Pontaj { get; set; }
    }
}
