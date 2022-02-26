using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FFmpeg_wrapper
{
    public class VideoJob
    {
        public int Id { get; set; }
        public int Author { get; set; }

        [Required]
        [StringLength(255)]
        public string Filename { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public DateTime Ingest { get; set; }

        public byte? TranscodeAttempt { get; set; }

        [StringLength(255)]
        public string TranscodeFilename { get; set; }
        public DateTime? TranscodeSuccess { get; set; }

        public byte? PublishAttempt { get; set; }

        [StringLength(255)]
        public string PublishLocation { get; set; }
        public DateTime? PublishSuccess { get; set; }
        public DateTime? Cleanup { get; set; }
    }

}
