using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.RightsManagement;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Bahtiar.Model;

namespace Bahtiar.ViewModel
{
    public class BahtiarViewModel : EntityBase
    {
        public CategoryGroup Categories { get; private set; }

        public BahtiarViewModel()
        {
            Categories = new CategoryGroup();
        }

        public void LoadData()
        {
            var uri = "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_subsections_by_ids&ids=all";
            var xml = new XmlDocument();
            var data = GetData(uri);
            if (string.IsNullOrEmpty(data))
                return;

            var reader = new XmlTextReader(uri);
            var ser = new XmlSerializer(typeof(CategoryGroup));
            Categories = ser.Deserialize(reader) as CategoryGroup;
        }

        private string GetData(string uri)
        {
            string res;
            using (var wc = new WebClient())
            {
                try
                {
                    res = wc.DownloadString(uri);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return res;
        }
    }
}
