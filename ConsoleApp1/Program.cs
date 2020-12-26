using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string t1 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QHRlc3QuZGUiLCJqdGkiOiJiODlhOTJkMy1lZmQ2LTRkMTAtYWMxZC01ZWNkYjc0ODE4NDgiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE4NGU3MDBjLWM5NzUtNDFmZi05OTc4LWE5ZTg1Y2Y0ZTE2YyIsImV4cCI6MTYwOTA4ODA4M30.xlPxzJANOAAkKTaMSuEz5G_V-br4r-P_BDL9uqFzgr8";
            string t2 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QHRlc3QuZGUiLCJqdGkiOiJiODlhOTJkMy1lZmQ2LTRkMTAtYWMxZC01ZWNkYjc0ODE4NDgiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE4NGU3MDBjLWM5NzUtNDFmZi05OTc4LWE5ZTg1Y2Y0ZTE2YyIsImV4cCI6MTYwOTA4ODA4M30.xlPxzJANOAAkKTaMSuEz5G_V-br4r-P_BDL9uqFzgr8";

            if (t1 == t2)
            {

            }

            TestApi();

            Console.ReadLine();
        }

        static async void TestApi()
        {
            //CreateUser();
            var token = await GetToken();
            await GetUserInfo(token);
        }

        static async void CreateUser()
        {
            LoginViewModel model = new LoginViewModel() { Email = "test@test.de", Password = "Test123!" };
            var json = System.Text.Json.JsonSerializer.Serialize(model);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var res = await client.PostAsync("https://localhost:5001/api/auth/Register", content);
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var la = 123;
            }
        }

        static async Task<TokenViewModel> GetToken()
        {
            LoginViewModel model = new LoginViewModel() { Email = "test@test.de", Password = "Test123!" };
            var json = System.Text.Json.JsonSerializer.Serialize(model);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var resp = await client.PostAsync("https://localhost:5001/api/auth/login", content);
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                json = await resp.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<TokenViewModel>(json, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            return null;
        }

        static async Task GetUserInfo(TokenViewModel model)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);

            var resp = await client.GetAsync("https://localhost:5001/api/auth/test");
        }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime TokenExpire { get; set; }
    }
}
