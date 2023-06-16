using SqlSugar;

namespace Entity
{
    /// <summary>
    /// 工厂
    /// </summary>
    [SugarTable("Factory")]
    public class Factory : BaseEntity
    {

        [SugarColumn]
        public string Name { get; set; }

        [SugarColumn]
        public string Tel { get; set; }

        [SugarColumn]
        public string Addr { get; set; }
    }
}
