namespace Lis.Service.ServiceImpl.IntialData
{
    public class Version
    {
        public string VersionNo { get; set; }
        public string VersionId { get; set; }
        public string Description { get; set; }
        public Version(string vid, string versionNo)
            : this(vid, versionNo, null)
        {
        }
        public Version(string vid, string versionNo, string description)
        {
            VersionNo = versionNo;
            VersionId = vid;
            Description = description;
        }

        public static readonly Version V1001 = new Version("E8BD70BA-BD74-47CB-A3DA-5B87178BF1EC", "1.0.0.1", "initial data");
        public static readonly Version V1002 = new Version("8D864389-3322-4EFA-A2DC-54AEE3F04EC3", "1.0.0.2", "add admin");
        public static readonly Version V1003 = new Version("22D8326C-C1D2-4063-92F7-830ED154FFE9", "1.0.0.3", "update password");
    }
}
