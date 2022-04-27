#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticeNotice.Models;

namespace PracticeNotice.Data
{
    public class PracticeNoticeContext : DbContext
    {
        public PracticeNoticeContext (DbContextOptions<PracticeNoticeContext> options)
            : base(options)
        {
        }

        public DbSet<PracticeNotice.Models.Notice> Notice { get; set; }
    }
}
