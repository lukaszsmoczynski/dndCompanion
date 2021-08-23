using dndCompanion.Externals.DndDatabase.DTOs.Spells;
using dndCompanion.Models.Spells;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp.Authenticators;
using System.Linq;
using dndCompanion.Helpers;

namespace dndCompanion.Externals.DndDatabase
{
    class DndDatabaseClient : IDndDatabaseClient
    {
        private readonly RestClient Client = new("http://185.18.141.147:5006/");
        public async Task<IEnumerable<Spell>> DownloadSpellsAsync()
        {
            var request = new RestRequest("spells", DataFormat.Json);
            var spellDtos = await Client.GetAsync<IEnumerable<SpellDto>>(request);
            return spellDtos.Select(spellDto => spellDto.AsModel());
        }
        public async Task LoginAsync()
        {
            var request = new RestRequest("users/authenticate", DataFormat.Json);
            request.AddJsonBody("{\"username\":\"test\",\"password\":\"test\"}");
            var response = await Client.PostAsync<string>(request);

            string token = JObject.Parse(response).GetValue("jwtToken").Value<string>();

            Client.Authenticator = new JwtAuthenticator(token);
            await GetUsersAsync(token);

            await Task.CompletedTask;
        }

        public async Task GetUsersAsync(string token)
        {
            var request = new RestRequest("users", DataFormat.Json);
            //request.AddHeader("Authorization", $"Bearer {token}");
            var response = await Client.GetAsync<string>(request);

            await Task.CompletedTask;
        }
    }
}
