using EF6Ninja.Crawler;
using EF6Ninja.Model;
using EntityFrameworkNinja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6Ninja.Repository
{
    public class LaptopMetadataRepository:IContentExtractorRepository
    {
        public void Save(LaptopMetaData laptopMetadata )
        {
            if (string.IsNullOrEmpty(laptopMetadata.Model) || string.IsNullOrEmpty(laptopMetadata.ProcessorDescription_Processor)) return;

            using(NinjaContext context = new NinjaContext())
            {
                context.LaptopMetaDataList.Add(laptopMetadata);
                context.SaveChanges();
            }
        }

        public void Save(IList<KeyValuePair<string, string>> content)
        {
            (new LaptopMetadataRepository()).Save(DataMapper.MapData(content));

            LaptopMetaData laptopMetaData = DataMapper.MapData(content);

            if (string.IsNullOrEmpty(laptopMetaData.Model) || string.IsNullOrEmpty(laptopMetaData.ProcessorDescription_Processor)) return;

            using (NinjaContext context = new NinjaContext())
            {
                context.LaptopMetaDataList.Add(laptopMetaData);
                context.SaveChanges();
            }
        }
    }
}
