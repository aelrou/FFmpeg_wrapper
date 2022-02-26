using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FFmpeg_wrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ingestFile = "sample-5s.mp4";

            if (ingestFile.Length > 255) {
                Console.WriteLine("Error. Filename contains more than 255 characters.");
                Environment.Exit(1);
            }

            if (ingestFile.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                Console.WriteLine("Error. Filename contains invalid characters.");
                Environment.Exit(1);
            }

            //Regex FILE_EXT = new Regex("^([^\\.].+)\\.([^\\.]+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex FILE_EXT = new Regex("^([^\\.].+)\\.([^\\.]+)$");
            MatchCollection matches = FILE_EXT.Matches(ingestFile);
            if (matches.Count != 1)
            {
                Console.WriteLine("Error. Invalid filename or extention.");
                Environment.Exit(1);
            }

            GroupCollection groups = null;
            foreach (Match match in matches)
            {
                groups = match.Groups;
            }
            string ingestFilename = groups[1].Value;
            string ingestExtention = groups[2].Value;

            string workDir = "C:\\Users\\[username]\\Downloads";
            if (Directory.Exists(workDir) == false)
            {
                Console.WriteLine("Error. Ingest directory cannot be found.");
                Environment.Exit(1);
            }

            if (File.Exists(workDir + "\\" + ingestFilename + "." + ingestExtention) == false)
            {
                Console.WriteLine("Error. Ingest file cannot be found.");
                Environment.Exit(1);
            }

            Console.WriteLine("Ingest: " + ingestFilename + "." + ingestExtention);

            string FORMAT_ISO_8601 = "yyyy-MM-ddTHHmmss";
            //Regex ISO_8601 = new Regex("\\d{4}-\\d{2}-\\d{2}T\\d{2}\\d{2}\\d{2}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            DateTime startDate = DateTime.UtcNow;
            string dateString = startDate.ToString(FORMAT_ISO_8601);
            //DateTime parseDate = DateTime.ParseExact(dateString, FORMAT_ISO_8601, CultureInfo.InvariantCulture);
            //Console.WriteLine(parseDate);
            string transcodeFile = ingestFilename + "_" + dateString + ".mp4";
            Console.WriteLine("FFmpeg transcode: " + transcodeFile);

            string ffmpegexe = "C:\\Users\\Public\\Downloads\\ffmpeg-5.0-full_build\\bin\\ffmpeg.exe";
            string audioBitrate = "144k";
            string videoBitrate = "4M";
            string videoMaxrate = "4M";
            string videoBufsize = "4M";

            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegexe,
                    Arguments = "-i \"" + workDir + "\\" + ingestFilename + "." + ingestExtention + "\" -c:v libx264 -pix_fmt yuv420p -b:v " + videoBitrate + " -maxrate " + videoMaxrate + " -bufsize " + videoBufsize + " -c:a aac -b:a " + audioBitrate + " -y \"" + workDir + transcodeFile + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                Console.WriteLine(line);
            }
            if (proc.ExitCode == 0)
            {
                Console.WriteLine("FFmpeg transcode complete.");
            }
            else
            {
                Console.WriteLine("Error. FFmpeg transcode failed.");
            }
        }
    }
}
