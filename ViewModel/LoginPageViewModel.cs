
namespace DateScheduler_MAUI.ViewModel;

public partial class LoginPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? username;

    [ObservableProperty]
    private string? password;

    [ObservableProperty]
    private bool rememberMe;

    [ObservableProperty]
    private bool incorrectPassword;

    //private RestService _restService;

    public LoginPageViewModel() : base("LoginPage")
    {
        //_restService = RestService.GetInstance();
        //if(_restService.TryToLogin())
        //{
        //AccelerometerChangedEventArgs chage page;
        //}
    }

    [RelayCommand]
    public void EntryUsernameChanged(string newUsername)
    {
        Username = newUsername;
    }

    [RelayCommand]
    public void EntryPasswordChanged(string newPassword)
    {
        IncorrectPassword = false;
        Password = newPassword;
        //verifikaciq za parolata trqbva da napravish da izpisva che tqrbva glavna malka cifra i simvol ako nqkoe ot tezi ne sa v parolata
        //tva iziskvane shte bade kam register obache ... tuk nqmaa nujda
    }

    [RelayCommand]
    public void RememberSwitchToggled(bool newToggle)
    {
        RememberMe = newToggle;
    }

    [RelayCommand]
    public async Task LogIn()
    {
        IsBusy = true;
        //Task<bool> loginTask = _restService.TryToLogin(Username, Password, RememberMe);
        //bool status = await loginTask;

        if (false) // failed to log-in ... asuume incorrect password ... gotta ask how we do that
        {
            IncorrectPassword = true;
            Password = null;
        }
        else
        {
            //await Navigation.PushAsync()
        }
        //if (status == true)
        //redirect user;

        //IncorrectPassword = !IncorrectPassword;
        IsBusy = false;
    }

    [RelayCommand]
    public async Task SignUp()
    {
        // change page ... blah blah
    }
}