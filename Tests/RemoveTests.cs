namespace Tests;

using SimpleDictionary;

[TestFixture]
public class RemoveTests
{
    private SimpleDict<string, int> _dict;

    [SetUp]
    public void Setup()
    {
        _dict = new SimpleDict<string, int>();
    }

    [TestCase("apple")]
    [TestCase("banana")]
    [TestCase("orange")]
    public void Remove_ShouldRemove(string key)
    {
        _dict.Add(key, 1);
        _dict.Remove(key);
        
        Assert.That(_dict.KeyExists(key), Is.EqualTo(false));
    }

    [Test]
    public void Remove_ShouldOnlyRemoveSpecifiedKey()
    {
        _dict.Add("apple", 5);
        _dict.Add("banana", 10);
        _dict.Add("orange", 15);
        
        _dict.Remove("apple");
        
        Assert.That(_dict["banana"], Is.EqualTo(10));
        Assert.That(_dict["orange"], Is.EqualTo(15));
    }

    [Test]
    public void Remove_ShouldThrowIfKeyDoesNotExist()
    {
        Assert.Throws<KeyNotFoundException>(() => _dict.Remove("apple"));
    }
}