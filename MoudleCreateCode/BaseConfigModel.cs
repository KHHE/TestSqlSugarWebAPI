using System.Collections.Generic;

namespace MoudleCreateCode
{
    public class BaseConfigModel
    {
        /// <summary>
        /// 数据库表名sys_menu
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 表名首字母大写SysMenu
        /// </summary>
        public string TableNameUpper { get; set; }
        public FileConfigModel FileConfig { get; set; }
        public OutputConfigModel OutputConfig { get; set; }

    }

    public class FileConfigModel
    {
        public string ClassPrefix { get; set; }
        public string ClassDescription { get; set; }
        public string CreateName { get; set; }
        public string CreateDate { get; set; }

        public string EntityName { get; set; }
        public string EntityParamName { get; set; }

        public string BusinessName { get; set; }
        public string ServiceName { get; set; }

        public string ControllerName { get; set; }

    }
    public class OutputConfigModel
    {
        public List<string> ModuleList { get; set; }
        public string OutputModule { get; set; }
        public string OutputEntity { get; set; }
        public string OutputBusiness { get; set; }
        public string OutputWeb { get; set; }
    }

}
