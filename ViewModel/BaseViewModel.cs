namespace DateScheduler_MAUI.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel(string t)
    {
        SetProperty(ref Title, t);
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;

    public readonly string Title;
}
