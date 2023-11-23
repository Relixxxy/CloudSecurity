namespace CloudSecurity;

public class CloudSecuritySettings
{
    [ConfigurationKeyName("BLOB_CONTAINER_NAME")]
    public string BlobContainerName { get; init; } = null!;
}
