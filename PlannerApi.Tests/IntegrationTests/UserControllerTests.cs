﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using events_planner.Deserializers;
using events_planner.Models;
using Newtonsoft.Json;
using PlannerApi.Tests.Fixtures;

namespace PlannerApi.Tests.IntegrationTests
{
    public partial class UserControllerTests : IClassFixture<ServerFixtures>
    {
        protected HttpClient HttpClient { get; set; }

        public UserControllerTests(ServerFixtures server) {
            HttpClient = server.Client;
        }

        public partial class GetToken : UserControllerTests {

            public GetToken(ServerFixtures server) : base(server) { }
            
            [Theory]
            [InlineData("Student", "123456789")]
            public async Task EnsureTokenIsReturned(string login, string password) {
                string fakeJson = JsonConvert.SerializeObject(new UserConnectionDeserializer() { Login = login, Password = password });

                HttpResponseMessage response = await HttpClient.PostAsync("api/user/token", new StringContent(fakeJson, System.Text.Encoding.UTF8, "application/json"));
                string token = await response.Content.ReadAsStringAsync();

                Console.WriteLine(token);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotNull(token);
            }
        }

        public partial class CreateUser : UserControllerTests {
            public CreateUser(ServerFixtures server) : base(server) { }

            [Fact]
            public async Task ShouldCreateUser() {
                UserCreationDeserializer user = new UserCreationDeserializer()
                {
                    FirstName = "Jhon",
                    LastName = "Smith",
                    Email = (DateTimeOffset.Now.ToUnixTimeSeconds().ToString()) + "jhon@mymail.com",
                    Password = "123456789",
                    PhoneNumber = "0619198793",
                    UserName = "JhonSmith",
                    RoleName = "Student"
                };

                string json = JsonConvert.SerializeObject(user);

                HttpResponseMessage response = await HttpClient.PostAsync("api/user", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                string stringResult = await response.Content.ReadAsStringAsync();
                User obj = JsonConvert.DeserializeObject<User>(stringResult);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.NotNull(obj);
                Assert.Equal(obj.Email, user.Email);
                Assert.Equal(obj.Role.Name, "Student");
            }
        }

        public partial class ReadUser : UserControllerTests {
            public ReadUser(ServerFixtures server) : base(server) { }

            [Fact]
            public async Task ShouldReturnTheUser() {
                string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKdWxpZW4iLCJnaXZlbl9uYW1lIjoiQm95ZXIiLCJlbWFpbCI6Imp1bGllbkBnbWFpbC5jb20iLCJqdGkiOiI2ODc0MDkwMC01YmRmLTQyNDUtYjBjZC1mYzhmNzhiYThjMjkiLCJyb2xlcyI6IlN0dWRlbnQiLCJuYmYiOjE1MTk2NjU4NDQsImV4cCI6MTUxOTgzODY0NCwiaXNzIjoibG9jYWxob3N0OjUwMDAiLCJhdWQiOiJsb2NhbGhvc3Q6NTAwMCJ9.AVKp-3LhwcmvbBQ73LHQ3uy2ss0lvmuwCw9pgsZ9dk8";

                HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await HttpClient.GetAsync("api/user", HttpCompletionOption.ResponseContentRead);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
