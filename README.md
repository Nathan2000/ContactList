# Solucja ContactList

ContactList jest aplikacj¹ zarz¹dzaj¹c¹ list¹ kontaktów. Sk³ada siê z backendu napisanego w ASP.NET Core Web API i frontendu napisanego w Blazor WebAssembly.

---
## Uruchomienie

### Wymagania
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)

Aby uruchomiæ projekt, proszê otworzyæ folder ContactList.Api i uruchomiæ plik `setup.bat`, który zainstaluje potrzebne narzêdzia, zbuduje API i je uruchomi. Nastêpnie, proszê uruchomiæ identycznie nazwany plik `setup.bat` w folderze ContactList.Web.

Strona internetowa powinna byæ dostêpna pod adresem [https://localhost:7003](https://localhost:7003). Swagger jest pod adresem [https://localhost:7288/swagger](https://localhost:7288/swagger).

## Projekty

### ContactList.Api

**Opis**: Backend napisany w ASP.NET Core Web API, udostêpniaj¹cy dostêp do bazy danych SQLite poprzez Entity Framework Core. Logowanie jest obs³ugiwane poprzez JWT. Do projektu jest do³¹czony Swagger w celu debuggowania API.

#### Kontrolery
- `AccountController`
  - `Register(RegisterDto registerDto)`
  - `Login(LoginDto loginDto)`
  - `Me()`
- `ContactsController`
  - `Get()`
  - `GetById(int id)`
  - `Post(ContactDto dto)`
  - `Put(ContactDto dto)`
  - `Delete(int id)`
- `SubcategoriesController`
  - `GetByCategory(Category category)`
  - `Get(int id)`

#### Klasy
- `ContactDbContext` - Kontekst Entity Framework Core. Konfiguruje model i seeduje startowymi danymi.
- Klasy biznesowe
  - `ApplicationUser`
  - `Contact`
  - `Subcategory`
- `JwtSettings` - Klasa pomocnicza do serializowania opcji w `appsettings.json`

---

### ContactList.Client

**Opis**: Frontend napisany w Blazor WebAssembly do zarz¹dzania kontaktami. Korzysta z biblioteki Blazored, które dodaje mo¿liwoœæ korzystania z LocalStorage przegl¹darki.

#### Strony
- `Home` - Wita u¿ytkownika.
- `Account/Login.razor` - Umo¿liwia logowanie u¿ytkowników.
- `Account/Details.razor` - Umo¿liwia rejestracjê nowych u¿ytkowników.
- `Contacts/List.razor` - Wyœwietla dane wszystkich kontaktów.
- `Contacts/Details.razor` - Umo¿liwia tworzenie nowych kontaktów oraz wyœwietla dane istniej¹cych, umo¿liwiaj¹c tak¿e ich edycjê i usuniêcie. Ta strona jest dostêpna tylko dla zalogowanych u¿ytkowników.

#### Serwisy
- `ContactService` - U³atwia korzystanie z Web API, udostêpniaj¹c metody, które ³¹cz¹ siê z nim i konwertuj¹ nag³ówki HTTP na DTO.
  
#### Klasy
- `AppAuthStateProvider` - Klasa zarz¹dzaj¹ca stanem zalogowania u¿ytkownika.
- `JwtAuthorizationMessageHandler` - Klasa do³¹czaj¹ca token JWT do zapytañ HTTP klienta.
---

### ContactList.Shared

**Opis**: Biblioteka u¿ywana przez obydwa projekty. Zawiera DTO przesy³ane miêdzy backendem i frontendem.

#### Klasy
- `ContactDto`
- `SubcategoryDto`
- `ContactCategory` (enum)

---
