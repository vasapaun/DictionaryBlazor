# DictionaryBlazor

DictionaryBlazor is an example server app made with MudBlazor and implementing a dictionary.

<img width="429" height="374" alt="screenshot_unos" src="https://github.com/user-attachments/assets/d9b4e8d4-7005-4490-8a65-7edba4ac389e" />

<img width="607" height="467" alt="screenshot_prikaz" src="https://github.com/user-attachments/assets/c8bf9860-fdc9-4da7-8fa0-41f1f6aeb80c" />

## Installation and running
### IDE
```
git clone https://github.com/vasapaun/DictionaryBlazor.git
```
Open the solution in your IDE and run **Server: http**
### CLI
``` 
git clone https://github.com/vasapaun/DictionaryBlazor.git
cd DictionaryBlazor
dotnet restore
dotnet build
dotnet run --project Server
```

If the browser doesn't automatically open:

Look for "Now listening on: http://localhost:XXXX" in the run command output.

Open http://localhost:XXXX in your browser.

## Testing

### IDE
Run **Tests** (You may need to go to View->Tool Windows->Tests, depending on the IDE)

### CLI
```
dotnet test
```
