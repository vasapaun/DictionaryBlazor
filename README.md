# DictionaryBlazor

DictionaryBlazor is an example server app made with MudBlazor and implementing a dictionary.

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
