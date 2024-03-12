namespace DateScheduler_MAUI.Service;

public class RestService
{
    private HttpClient _client;

    private RestService()
    {
        _client = new HttpClient();
    }

    public async Task<bool> TryToLogin(string u, string p, bool r)
    {
        const string LoginAPI_URL = "https://taskmanager20240106194805.azurewebsites.net/api/Account/Login";

        // dali imash token v secure storage?
        // ako da - validen li e?
        // da probvam da vzema task s nego za proverka?
        // ako uspeq - validen e
        // 

        //string authToken = SecureStorage.GetAsync("AuthToken").Result;
        //if (authToken != )// try to get token and compare the 2 tokens ... gotta ask how i do that
        //   return;   /// should i log in again? and compare the 2 tokens? how do i know if
        // this token is no longer valid??????
        // maybe a functionality to get whether the token is valid should be implemented
        // that way i can also check if the user is valid before doing requests like getting tasks???
        // .... dont log in if user token is valid after remember me is set to true
        // lets pretend thats done somehow

        // token user id ... 
        // remember me - token

        //

        // token has run out ... force the user to log-in
        var json_data = new { username = u, password = p, rememberMe = r };
        string jsonBody = JsonSerializer.Serialize(json_data);

        StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        Debug.WriteLine($"user={u}, pass={p} and remember={r}");

        try
        {
            HttpResponseMessage response = await _client.PostAsync(LoginAPI_URL, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JsonDocument jsonResponse = JsonDocument.Parse(responseContent);

                // Get the values of various properties
                string UserID = jsonResponse.RootElement.GetProperty("userId").GetString()!;
                string Username = jsonResponse.RootElement.GetProperty("username").GetString()!;
                string Message = jsonResponse.RootElement.GetProperty("message").GetString()!;
                string Token = jsonResponse.RootElement.GetProperty("token").GetString()!;
                string Email = jsonResponse.RootElement.GetProperty("email").GetString()!;

                jsonResponse.Dispose();

                // save all user data to device, cuz next time i may need the user data,
                // but if automatic login is enabled i have to have the data saved
                // cuz the data is taken only when logging in
                await SecureStorage.SetAsync("UserID", UserID);
                await SecureStorage.SetAsync("Username", Username);
                await SecureStorage.SetAsync("Message", Message);
                await SecureStorage.SetAsync("Token", Token);
                await SecureStorage.SetAsync("Email", Email);

                Debug.WriteLine(responseContent);
                return true;
            }
            else
            {
                Debug.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
            return false;
        }
    }

    public async Task<ObservableCollection<IDailyTask>?> TryToGetTasksByDate(DateTime date)
    {
        //string authToken = SecureStorage.GetAsync("AuthToken").Result;
        // use token to try to get tastks from a certain date ... if error occurs - token expired
        // redirect to login page


        // if all good ... get the tasks
        // --- lets say the tasks are taken??
        // what does interface for task contain? -- message? task name? task priority? something else?
        //ObservableCollection<ITask> callendarCollection = new ObservableCollection<ITask>();
        // callendarCollection.Add(DateTime from task, part of current month (true/false))
        // after all tasks are added to the collection, return it
        // return callendarCollection;

        string due_date = $"{date.Year}-{date.Month}-{date.Day}";
        string API_URL = $"https://taskmanager20240106194805.azurewebsites.net/api/Task/GetAllIncompleted?&DueDate={due_date}";

            try
            {
                // Retrieve the stored Bearer token
                string? token = await SecureStorage.GetAsync("Token");

                if (string.IsNullOrEmpty(token))
                {
                    Debug.WriteLine("Token not found. Please log in.");
                    // redirect to login page
                    return null;
                }

                // Attach the Bearer token to the HttpClient headers
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync(API_URL);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    //ObservableCollection<ITask> callendarCollection = JsonConvert.DeserializeObject<ObservableCollection<ITask>>(responseContent);
                    Debug.WriteLine($"Tasks: {responseContent}");

                    var taskImplementations = JsonSerializer.Deserialize<ObservableCollection<DailyTask>>(responseContent);

                    // Assuming callendarCollection is initially of type ObservableCollection<ITask>
                    ObservableCollection<IDailyTask> callendarCollection = new ObservableCollection<IDailyTask>(
                        taskImplementations.Cast<IDailyTask>()
                    );

                    return callendarCollection;
                }
                else
                {
                    Debug.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
}
