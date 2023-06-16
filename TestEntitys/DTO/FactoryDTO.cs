using SqlSugar;
using System;

namespace Entity
{
    /// <summary>
    /// 工厂DTO查询
    /// </summary>
    public class FactoryDTO: BaseDTO
    {
        public string Name { get; set; }

        public string Tel { get; set; }

        public string Addr { get; set; }
    }
}
