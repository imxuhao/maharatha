namespace CAPS.CORPACCOUNTING.Features
{
    public static class AppFeatures
    {
        // This feature will enable or disable chatting
        public const string ChatFeature = "App.ChatFeature";
        public const string TenantToTenantChatFeature = "App.ChatFeature.TenantToTenant";
        public const string TenantToHostChatFeature = "App.ChatFeature.TenantToHost";

        // This feature is to enable or disable features specific to each tenant type
        public const string TenantType = "App.TenantType";
        public const string TenantTypeCommercial = "App.TenantType.Commercial";
        public const string TenantTypeFeatures = "App.TenantType.Features";
        public const string TenantTypeTelevision = "App.TenantType.Television";
        public const string TenantTypeTelevisionEpisodic = "App.TenantType.Television.Episodic";
        public const string TenantTypeTelevisionNonEpisodic = "App.TenantType.Television.NonEpisodic";
        public const string TenantTypeMusicTour = "App.TenantType.MusicTour";
        public const string TenantTypeFestivalLiveEvent = "App.TenantType.Festival&Events";
        public const string TenantTypeVenues = "App.TenantType.Venues";

        public const string ProjectLimitation = "App.NumberofProject";

        
    }
}