using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FFmpeg_wrapper
{
    public class LZVideoContext : DbContext
    {
        public LZVideoContext()
            :base("name=LZVideoContext")
        {
        }
        public DbSet<VideoJob> VideoJobs { get; set; }
    }
}
