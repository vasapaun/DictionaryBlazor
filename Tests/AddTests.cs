using System.IO.Pipes;

namespace Tests;

using SimpleDictionary;

public class AddTests
{
    private SimpleDict<string, int> _dict;
    
    [SetUp]
    public void Setup()
    {
        _dict = new SimpleDict<string, int>();
    }

    [Test]
    public void Add_ShouldAddEntry()
    {
        _dict.Add("apple", 5);
        _dict.Add("banana", 10);
        _dict.Add("orange", 15);
        
        Assert.That(_dict.Length(), Is.EqualTo(3));
    }

    [Test]
    public void Add_ShouldResize()
    {
        List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
        
        list.Add(new KeyValuePair<string, int>("apple", 5));
        list.Add(new KeyValuePair<string, int>("banana", 10));
        list.Add(new KeyValuePair<string, int>("orange", 15));
        list.Add(new KeyValuePair<string, int>("blueberry", 20));
        list.Add(new KeyValuePair<string, int>("raspberry", 25));
        list.Add(new KeyValuePair<string, int>("watermelon", 30));
        list.Add(new KeyValuePair<string, int>("grapes", 35));
        list.Add(new KeyValuePair<string, int>("grapefruit", 40));
        list.Add(new KeyValuePair<string, int>("strawberry", 45));
        list.Add(new KeyValuePair<string, int>("mango", 50));

        foreach (var pair in list)
        {
            _dict.Add(pair);
        }
        
        Assert.That(_dict.Size(), Is.EqualTo(32));

        foreach (var pair in list)
        {
            Assert.That(_dict[pair.Key], Is.EqualTo(pair.Value));
        }
    }

    [TestCase("apple", 5)]
    [TestCase("banana", 10)]
    [TestCase("orange", 15)]
    public void Add_ShouldStoreValue(string  key, int value)
    {
        _dict.Add(key, value);
        Assert.That((_dict[key]), Is.EqualTo(value));
    }

    [Test]
    public void Add_ShouldThrowIfKeyExists()
    {
        _dict.Add("apple", 5);
        
        Assert.Throws<ArgumentException>(() => _dict.Add("apple", 10));
    }

    [Test]
    public void Add_ShouldThrowIfKeyIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _dict.Add(null, 10));
    }
}