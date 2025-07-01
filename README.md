# Solucja ContactList

ContactList jest aplikacj� zarz�dzaj�c� list� kontakt�w. Sk�ada si� z backendu napisanego w ASP.NET Core Web API i frontendu napisanego w Blazor WebAssembly.

---
## Uruchomienie

### Wymagania
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)

Aby uruchomi� projekt, prosz� otworzy� folder ContactList.Api i uruchomi� plik `setup.bat`, kt�ry zainstaluje potrzebne narz�dzia, zbuduje API i je uruchomi. Nast�pnie, prosz� uruchomi� identycznie nazwany plik `setup.bat` w folderze ContactList.Web.

Strona internetowa powinna by� dost�pna pod adresem [https://localhost:7003](https://localhost:7003). Swagger jest pod adresem [https://localhost:7288/swagger](https://localhost:7288/swagger).

## Projekty

### ContactList.Api

**Opis**: Backend napisany w ASP.NET Core Web API, udost�pniaj�cy dost�p do bazy danych SQLite poprzez Entity Framework Core. Logowanie jest obs�ugiwane poprzez JWT. Do projektu jest do��czony Swagger w celu debuggowania API.

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

**Opis**: Frontend napisany w Blazor WebAssembly do zarz�dzania kontaktami. Korzysta z biblioteki Blazored, kt�re dodaje mo�liwo�� korzystania z LocalStorage przegl�darki.

#### Strony
- `Home` - Wita u�ytkownika.
- `Account/Login.razor` - Umo�liwia logowanie u�ytkownik�w.
- `Account/Details.razor` - Umo�liwia rejestracj� nowych u�ytkownik�w.
- `Contacts/List.razor` - Wy�wietla dane wszystkich kontakt�w.
- `Contacts/Details.razor` - Umo�liwia tworzenie nowych kontakt�w oraz wy�wietla dane istniej�cych, umo�liwiaj�c tak�e ich edycj� i usuni�cie. Ta strona jest dost�pna tylko dla zalogowanych u�ytkownik�w.

#### Serwisy
- `ContactService` - U�atwia korzystanie z Web API, udost�pniaj�c metody, kt�re ��cz� si� z nim i konwertuj� nag��wki HTTP na DTO.
  
#### Klasy
- `AppAuthStateProvider` - Klasa zarz�dzaj�ca stanem zalogowania u�ytkownika.
- `JwtAuthorizationMessageHandler` - Klasa do��czaj�ca token JWT do zapyta� HTTP klienta.
---

### ContactList.Shared

**Opis**: Biblioteka u�ywana przez obydwa projekty. Zawiera DTO przesy�ane mi�dzy backendem i frontendem.

#### Klasy
- `ContactDto`
- `SubcategoryDto`
- `ContactCategory` (enum)

---
