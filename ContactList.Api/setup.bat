dotnet user-secrets init
dotnet user-secrets set "JwtSettings:Secret" "twój-ultra-super-tajny-klucz"
dotnet dev-certs https --trust
dotnet tool restore
dotnet ef database update
dotnet run --launch-profile "https"
pause