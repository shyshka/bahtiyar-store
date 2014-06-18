namespace Bahtiar.Helper
{
    public static class Constants
    {
        public const string UriGetCategories =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_subsections_by_ids&ids={0}";

        public const string UriGetBrands =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_brands_by_ids&id_s={0}";

        public const string UriGetProducts =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_goods_goods_vendors_by_ids&id_s={0}&id_b={1}&id_v={2}&from={3}&limit={4}";

        public const string UriGetSelletInfoByHash =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_vendors_by_hash&hash={0}";

        public const string UriGetConnectedCategories =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_vendors_subsections_by_id&id_v={0}";

        public const string UriLockCategories =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=lock_vendors_subsections_by_id&id_v={0}&id_s={1}";

        public const string UriUnlockCategories =
            "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=unlock_vendors_subsections_by_id&id_v={0}&id_s={1}";



        public static string TmpHash = "6d6fa635612d7613c73229488bc022b7";
    }
}
