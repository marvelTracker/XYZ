using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compareIT.Data.Model
{
    public class Computer
    {
        public int ComputerID { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string BaseModelProcessor { get; set; }
        public string ProcessorSpeed { get; set; }
        public string Brand { get; set; }
        public string VideoCard { get; set; }
        public string RAM { get; set; }
        public string OperatingSystem { get; set; }
        public string ScreenSize { get; set; }

    }


}
