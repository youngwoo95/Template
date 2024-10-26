namespace MDMSender.Models
{
    public class Settings
    {
        /// <summary>
        /// 데이터베이스 IP
        /// </summary>
        public static string? DBIpAddress { get; set; }

        /// <summary>
        /// 데이터베이스 PORT
        /// </summary>
        public static string? DBPort { get; set; }

        /// <summary>
        /// 데이터베이스 NAME
        /// </summary>
        public static string? DBUser { get; set; }

        /// <summary>
        /// 데이터베이스 PW
        /// </summary>
        public static string? DBPW { get; set; }

        /// <summary>
        /// 데이터베이스 NAME
        /// </summary>
        public static string? DBName { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public static string? Destination { get; set; }
    }
}
