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