using System.Windows.Input;

namespace DateScheduler_MAUI.ViewModel;

public partial class MainPageViewModel : BaseViewModel
{
	public MainPageViewModel(RestService restService) : base("MainPage")
	{
        _restService = restService;
        HC = new HorizontalCalendar();
        SelectedItem = HC.Collection[0];
    }

    public readonly HorizontalCalendar HC { get; }

    private readonly RestService _restService;

    [ObservableProperty]
    private ObservableCollection<IDailyTask> tasksFromSelectedDate;

    [ObservableProperty]
    private ICalendarDate selectedItem;

    /*public ICalendarDate StartingItem
    {
        get { return HCCollection[0]; }
    }*/

    /*public ObservableCollection<ICalendarDate> HCCollection
    {
        get { return HCC.HCalendarCollection; }
    }*/

    public void OpenCalendar(object sender, EventArgs e)
    {

    }

    // need to test the two-way ui - view model updating how works .... if it works as expected
    // there would be no need tor SelectedItem = selectedItem; ....
    [RelayCommand]
    public async Task DateChanged(ICalendarDate selectedItem)
    {
        SelectedItem = selectedItem;
        var collection = await _restService.TryToGetTasksByDate(SelectedItem.Date);

        TasksFromSelectedDate = collection;
    }
}