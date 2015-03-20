using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EF6Ninja.Model
{
    public class DataMapper
    {
        public static LaptopMetaData MapData(IList<KeyValuePair<string, string>> extractedData)
        {

            LaptopMetaData laptopMetaData = new LaptopMetaData();

            foreach (KeyValuePair<string, string> extractedItem in extractedData)
            {
                PropertyInfo propertyInfo = laptopMetaData.GetType().GetProperty(extractedItem.Key);

                if (propertyInfo != null)
                    propertyInfo.SetValue(laptopMetaData, extractedItem.Value, null);
            }


            return laptopMetaData;
        }
    }
}
