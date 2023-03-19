namespace ModComponent.Utils;

internal sealed class ModContentCatalog
{
	public string? m_LocatorId { get; set; }
	public ModObjectInitializationData? m_InstanceProviderData { get; set; }
	public ModObjectInitializationData? m_SceneProviderData { get; set; }
	public List<ModObjectInitializationData>? m_ResourceProviderData { get; set; }
	public string[]? m_ProviderIds { get; set; }
	public string[]? m_InternalIds { get; set; }
	public string? m_KeyDataString { get; set; }
	public string? m_BucketDataString { get; set; }
	public string? m_EntryDataString { get; set; }
	public string? m_ExtraDataString { get; set; }
	public ModSerializedType[]? m_resourceTypes { get; set; }
	public string[]? m_InternalIdPrefixes { get; set; }
}