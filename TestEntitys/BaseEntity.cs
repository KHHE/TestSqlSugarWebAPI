using SqlSugar;
using System;

namespace Entity
{
    /// <summary>
    /// 工厂
    /// </summary>
    public class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(InsertSql = "getdate()")] //生成   getdate()
        public DateTime CreateTime { get; set; }

        [SugarColumn(InsertSql = "getdate()")] //生成   getdate()
        public DateTime UpdateTime { get; set; }

        [SugarColumn(InsertSql = "1")] //生成   1
        public int Vertion { get; set; }

        public void Modify()
        {
            this.UpdateTime = DateTime.Now;
        }
    }
}
