namespace DateScheduler_MAUI.Model;

public class DailyTask : IDailyTask
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("importanceLevel")]
    public ImportanceLevel Priority { get; set; }

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; set; }
}