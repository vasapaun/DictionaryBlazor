namespace Tests;

using SimpleDictionary;

[TestFixture]
public class UpdateTests
{
    private SimpleDict<string, int> _dict;
    
    [SetUp]
    public void SetUp()
    {
        _dict = new();
    }

    [TestCase("apple", 5, 10)]
    [TestCase("banana", 15, 20)]
    [TestCase("orange", 25, 30)]
    public void Update_ShouldUpdateValue(string key, int oldValue, int newValue)
    {
        _dict.Add(key, oldValue);
        _dict.Update(key, newValue);
        Assert.That(_dict[key], Is.EqualTo(newValue));
    }

    [Test]
    public void Update_ShouldKeepCount()
    {
        _dict.Add("apple", 5);
        _dict.Update("apple", 10);
        Assert.That(_dict.Length(), Is.EqualTo(1));
    }

    [Test]
    public void Update_ShouldThrowWhenKeyDoesNotExist()
    {
        Assert.Throws<KeyNotFoundException>(() => _dict.Update("apple", 10));
    }
    
    [Test]
    public void Update_ShouldOnlyUpdateSpecifiedKey()
    {
        _dict.Add("apple", 5);
        _dict.Add("banana", 10);
        _dict.Add("orange", 15);
        
        _dict.Update("apple", 20);
        
        Assert.That(_dict["banana"], Is.EqualTo(10));
        Assert.That(_dict["orange"], Is.EqualTo(15));
    }
}