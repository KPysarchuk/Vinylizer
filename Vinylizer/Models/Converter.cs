using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vinylizer.Models
{
    public class Converter
    {
        public static void Merge(string fileName)
        {
            Random rnd = new Random();
            string input = string.Format("Uploads/{0}/{1}", fileName, fileName);
            string filter = string.Format("Uploads/{0}/filter.mp3", fileName); ;
            string UfileName = string.Format("Converted{0}", fileName);
            string output = string.Format("Uploads/{0}/", UfileName);
            string command = string.Format("-i {0} -i {1} -filter_complex amix=inputs=2:duration=first:dropout_transition=2 {2}", input, filter, output);
            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.Invoke(command);
        }

        public static void ChangeVolume(int volumePart, string fileName)
        {
            Random rnd = new Random();
            string filter = "filter.mp3";
            string output = string.Format("Uploads/{0}/filter.mp3", fileName);
            string command = string.Format(@"-i {0} -af ""volume={1}"" {2}", filter, volumePart, output).Replace("\"", string.Empty);
            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.Invoke(command);
        }

        public static string ChangeVolumeForTest(int volumePart, string fileName)
        {
            Random rnd = new Random();
            string filter = "filter.mp3";
            string outputName = string.Format("filter{0}.mp3", rnd.Next(1000000).ToString());
            string output = string.Format("audio/{0}", outputName);
            string command = string.Format(@"-i {0} -af ""volume={1}"" {2}", filter, volumePart, output).Replace("\"", string.Empty);
            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.Invoke(command);

            return outputName;
        }
    }
}