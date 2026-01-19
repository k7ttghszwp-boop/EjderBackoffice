namespace Ejder.Mobile;

public partial class MainPage : ContentPage
{
    private readonly HttpClient _http;

    public MainPage(HttpClient http)
    {
        InitializeComponent();
        _http = http;
    }

    private async void OnHealthClicked(object sender, EventArgs e)
    {
        try
        {
            var json = await _http.GetStringAsync("api/health");
            ResultLabel.Text = json;
        }
        catch (Exception ex)
        {
            ResultLabel.Text = ex.Message;
        }
    }
}
