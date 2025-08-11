using System.Runtime;

namespace Tests;

using SimpleDictionary;

[TestFixture]
public class OtherTests
{
    private SimpleDict<string, int> _dict;

    [SetUp]
    public void Setup()
    {
        _dict = new();
    }

    [Test]
    public void Clear()
    {
        _dict.Add("apple", 5);
        _dict.Add("banana", 10);
        _dict.Add("orange", 15);
        
        _dict.Clear();
        
        Assert.That(_dict.Length(), Is.EqualTo(0));
    }

    [TestCase("apple", 5)]
    [TestCase("banana", 10)]
    [TestCase("orange", 15)]
    public void GetValue(string key, int value)
    {
        _dict.Add(key, value);
        Assert.That(_dict[key], Is.EqualTo(value));
    }

    [Test]
    public void Keys()
    {
        List<string> keys = new();
        keys.Add("apple");
        keys.Add("banana");
        keys.Add("orange");

        foreach (var key in keys)
        {
            _dict.Add(key, 5);
        }

        List<string> keys2 = _dict.Keys();
        
        keys.Sort();
        keys2.Sort();
        
        Assert.That(keys2, Is.EqualTo(keys));
    }
    
    [Test]
    public void Values()
    {
        List<KeyValuePair<string, int>> pairs = new();
        pairs.Add(new KeyValuePair<string, int>("apple", 5));
        pairs.Add(new KeyValuePair<string, int>("banana", 10));
        pairs.Add(new KeyValuePair<string, int>("orange", 15));

        List<int> values = new();
        
        foreach (var pair in pairs)
        {
            _dict.Add(pair);
            values.Add(pair.Value);
        }

        List<int> values2 = _dict.Values();
        
        values.Sort();
        values2.Sort();
        
        Assert.That(values2, Is.EqualTo(values));
    }

    [TestCase("apple", 5, 10)]
    [TestCase("banana", 15, 20)]
    [TestCase("orange", 25, 30)]
    public void Indexer(string key, int oldValue, int newValue)
    {
        _dict.Add(key, oldValue);
        
        _dict[key] = newValue;
        Assert.That(_dict[key], Is.EqualTo(newValue));
    }

    [Test]
    public void KeyExists()
    {
        _dict.Add("apple", 5);
        
        Assert.That(_dict.KeyExists("apple"), Is.EqualTo(true));
        Assert.That(_dict.KeyExists("banana"), Is.EqualTo(false));
    }

    [Test]
    public void ToList()
    {
        List<KeyValuePair<string, int>> pairs = new();
        pairs.Add(new KeyValuePair<string, int>("apple", 5));
        pairs.Add(new KeyValuePair<string, int>("banana", 10));
        pairs.Add(new KeyValuePair<string, int>("orange", 15));

        foreach (var pair in pairs)
        {
            _dict.Add(pair);
        }
        
        List<KeyValuePair<string, int>> pairs2 = _dict.ToList();
        
        pairs.Sort((a,b) => a.Key.CompareTo(b.Key));
        pairs2.Sort((a,b) => a.Key.CompareTo(b.Key));
        Assert.That(pairs, Is.EqualTo(pairs2));
    }

    [Test]
    public void Length()
    {
        _dict.Add("apple", 5);
        _dict.Add("banana", 10);
        _dict.Add("orange", 15);
        
        Assert.That(_dict.Length(), Is.EqualTo(3));
    }

    [Test]
    public void Enumerable()
    {
        List<KeyValuePair<string, int>> pairs = new();
        pairs.Add(new KeyValuePair<string, int>("apple", 5));
        pairs.Add(new KeyValuePair<string, int>("banana", 10));
        pairs.Add(new KeyValuePair<string, int>("orange", 15));

        foreach (var pair in pairs)
        {
            _dict.Add(pair);
            Assert.That(_dict[pair.Key], Is.EqualTo(pair.Value));
        }
    }
    
    // Keyexists, ToList, Length, Enumerable
}