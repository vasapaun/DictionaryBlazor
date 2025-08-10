using SimpleDictionary;

SimpleDict<int, int> dict = new();

for (int i = 1; i <= 2000; i++)
{
    dict.Add(i, i*10);
}

Console.WriteLine($"Dictionary length: {dict.Length()}");

dict.Update(new KeyValuePair<int, int>(2000, 7));
dict.Remove(1999);

dict[1997] = 13;
dict[1996] = dict[1997];

var pairs = new List<KeyValuePair<int, int>>();
pairs = dict.ToList();

foreach (var item in pairs)
{
    Console.WriteLine(item);
}

var keys = dict.Keys();
var values = dict.Values();

foreach (var key in keys)
{
    Console.WriteLine(key);
}

foreach (var value in values)
{
    Console.WriteLine(value);
}