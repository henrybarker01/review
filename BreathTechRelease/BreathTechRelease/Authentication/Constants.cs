namespace OAuthNativeFlow
{
    public static class Constants
    {
        public static string AppName = "OAuthNativeFlow";

        //public static string SupportEmailId = "info@breathtech.com";
        public static string SupportEmailId = "info@breathtechapp.com";
       
        // OAuth
        // For Google LoginTemplate, configure at https://console.developers.google.com/
        public static string iOSClientId = "<insert IOS client ID here>";
        public static string AndroidClientId = "404477101244-179ui03uo9o6d2f7dfuuu6grcpf1rcve.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "<insert IOS redirect URL here>:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.404477101244-179ui03uo9o6d2f7dfuuu6grcpf1rcve:/oauth2redirect";
    }
}
