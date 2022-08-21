using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InternalSystems.Models;

    public class AccessContext : DbContext
    {
        public AccessContext(DbContextOptions<AccessContext> options)
            : base(options)
        {
        }

        public DbSet<AccessModel> AccessModel { get; set; } = default!;
    }
