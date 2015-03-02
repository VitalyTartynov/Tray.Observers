namespace ProstoPleerApi
{
    public class ConnectionSettings
    {
        /// <summary>
        /// ID приложения
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// Пароль приложения
        /// </summary>
        public string ClientSecret { get; private set; }
        
        /// <summary>
        /// Путь для получения токена
        /// </summary>
        public string TokenUrl { get; private set; }

        /// <summary>
        /// Путь для выполнения запросов к API
        /// </summary>
        public string ApiUrl { get; private set; }
        
        /// <summary>
        /// Полученный токен подключения или NULL
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        /// Тип авторизации
        /// </summary>
        public string GrantType { get; private set; }

        public ConnectionSettings()
        {
            ClientId = "339052";
            ClientSecret = "100500";
            GrantType = "client_credentials";
            TokenUrl = "http://api.pleer.com/token.php";
            ApiUrl = "http://api.pleer.com/resource.php";
            AccessToken = null;
        }
    }
}