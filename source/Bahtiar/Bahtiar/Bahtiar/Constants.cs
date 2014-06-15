using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bahtiar
{
    public static class Constants
    {
        public const string UriGetCategories =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_subsections_by_ids&ids={0}";

        public const string UriGetBrands =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_brands_by_ids&id_s={0}";

        public const string UriGetProducts =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_goods_goods_vendors_by_ids&id_s={0}&id_b={1}&id_v={2}&from={3}&limit={4}";
    }
}
