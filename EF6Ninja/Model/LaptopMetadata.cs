using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6Ninja.Model
{
    public class LaptopMetaData
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Model { get; set; }
        public string ContentUrl { get; set;} 
       
        //Processor
        public string ProcessorDescription_Processor { get; set; }
        public string ProcessorDescription_Speed { get; set; }
        public string ProcessorDescription_Cache { get; set; }

        //RAM
        public string Memory_RAM { get; set; }
        public string Memory_Type { get; set; }
       

        //HARD
        public string HardDrive_InbuiltHDD { get; set; }
        public string HardDrive_SpeedRPM { get; set; }

        public string DisplayFeatures_ScreenSize { get; set; }
        public string DisplayFeatures_MaximumDisplayResolutiondpi { get; set; }
        public string DisplayFeatures_PanelType { get; set; }
        public string Chipset_GPUModel { get; set; }
        public string Chipset_GPUMemoryshared { get; set; }
        public string Chipset_HDMIPort { get; set; }

        public string Networking_EthernetPortNos { get; set; }
        public string Networking_EthernetType { get; set; }
        public string Networking_WiFiType { get; set; }

        public string Audio_Speakers { get; set; }
        public string Connectivity_USB20ports { get; set; }
        public string Connectivity_USB30ports { get; set; }
        public string Connectivity_Bluetooth { get; set; }
        public string Connectivity_BuiltinCamera { get; set; }

        public string Connectivity_Microphone { get; set; }
        public string Connectivity_DigitalMediaReader { get; set; }
        public string Battery_BatteryType { get; set; }
        public string Battery_BatteryCell { get; set; }

        public string OperatingSystem_OS { get; set; }
        public string Dimensions_Weight { get; set; }
        public string AfterSalesService_WarrantyPeriod { get; set; }

    }

}