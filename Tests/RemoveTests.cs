namespace Tests;

using SimpleDictionary;

[TestFixture]
public class RemoveTests
{
    private SimpleDict<string, int> _dict;
    private SimpleDict<int, int> _dictints;

    [SetUp]
    public void Setup()
    {
        _dict = new SimpleDict<string, int>();
        _dictints = new SimpleDict<int, int>();
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
    public void Remove_ShouldResize()
    {
        for (int i = 0; i < 100; i++)
        {
            _dictints.Add(i, i);
        }

        for (int i = 0; i < 90; i++)
        {
            _dictints.Remove(i);
        }
        
        Assert.That(_dictints.Size(), Is.Not.EqualTo(128));
        Assert.That(_dictints.Size(), Is.EqualTo(32));
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