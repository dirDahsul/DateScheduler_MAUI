namespace DateScheduler_MAUI.Model;

public enum ImportanceLevel
{
    High,
    Medium,
    Low
}

public interface IDailyTask
{
    string Name { get; set; }

    string Description { get; set; }

    ImportanceLevel Priority { get; set; }

    bool IsCompleted { get; set; }
}