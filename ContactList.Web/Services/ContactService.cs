using ContactList.Shared.Contacts;
using System.Net;
using System.Net.Http.Json;

namespace ContactList.Web.Services
{
    public class ContactService
    {
        private readonly HttpClient _http;

        public ContactService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ContactDto>> GetContactsAsync()
        {
            return await _http.GetFromJsonAsync<List<ContactDto>>("api/contacts") ?? [];
        }

        public async Task<ContactDto?> GetContactByIdAsync(int id)
        {
            var response = await _http.GetAsync($"api/contacts/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ContactDto>();
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            var msg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Failed to fetch contact: {msg}");
        }

        public async Task<int> CreateContactAsync(ContactDto contact)
        {
            var response = await _http.PostAsJsonAsync("api/contacts", contact);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<ContactDto>();
            return created!.Id;
        }

        public async Task UpdateContactAsync(ContactDto contact)
        {
            var response = await _http.PutAsJsonAsync($"api/contacts/{contact.Id}", contact);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteContactAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/contacts/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<SubcategoryDto>> GetSubcategoriesAsync(Category? category = null)
        {
            if (category != null)
            {
                return await _http.GetFromJsonAsync<List<SubcategoryDto>>($"api/subcategories?category={category}") ?? [];
            }
            else
            {
                return await _http.GetFromJsonAsync<List<SubcategoryDto>>($"api/subcategories") ?? [];
            }
        }
    }
}
