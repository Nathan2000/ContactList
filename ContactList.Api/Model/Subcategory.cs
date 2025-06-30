using ContactList.Shared.Contacts;

namespace ContactList.Api.Model
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Category Category { get; set; }
    }
}
