using System;

namespace Entity
{
    /// <summary>
    /// DTO查询基类
    /// </summary>
    public class BaseDTO
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
