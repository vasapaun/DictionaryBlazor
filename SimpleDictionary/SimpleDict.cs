using System.Collections;

namespace SimpleDictionary;

public class Constants
{
    public const int InitialBucketCount = 8;
    public const int SizeIncreaseStep = 4;
}

public class SimpleDict<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private LinkedList<KeyValuePair<TKey, TValue>>[] _buckets;
    private int _bucketCount = Constants.InitialBucketCount;

    // ----------------- Constructors --------------------------------------
    public SimpleDict()
    {
        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[_bucketCount];
    }

    public SimpleDict(SimpleDict<TKey, TValue> other)
    {
        _bucketCount = other.Length();
        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[_bucketCount];
        
        foreach (KeyValuePair<TKey, TValue> entry in other)
        {
            Add(entry);
        }
    }
    // ----------------- Constructors --------------------------------------

    // ----------------- CRUD ----------------------------------------------
    public void Add(TKey key, TValue value)
    {
        if (key == null || value == null) throw new ArgumentNullException();
        if (KeyExists(key)) throw new ArgumentException();
        
        // Decide what bucket the entry goes to
        int bucketIndex = GetBucketIndex(key);
        var bucket = _buckets[bucketIndex];
        
        // If bucket is empty, initialise it first, before adding an element to it
        if (bucket == null)
        {
            bucket = new LinkedList<KeyValuePair<TKey, TValue>>();
            _buckets[bucketIndex] = bucket;
        }
        
        // AddFirst (since AddLast is O(n), and bucket order doesn't matter)
        bucket.AddFirst(new KeyValuePair<TKey, TValue>(key, value));
        
        // If buckets are 'crowded', increase bucket count
        if (Length() >= _bucketCount)
        {
            IncreaseSize();
        }
    }

    public void Add(KeyValuePair<TKey, TValue> pair)
    {
        Add(pair.Key, pair.Value);
    }

    public void Update(KeyValuePair<TKey, TValue> pair)
    {
        if (pair.Key == null || pair.Value == null) throw new ArgumentNullException();
        if (!KeyExists(pair.Key)) throw new KeyNotFoundException();
        
        int bucketIndex = GetBucketIndex(pair.Key);
        var bucket = _buckets[bucketIndex];

        // Iterate through bucket manually, since elements are immutable in foreach
        LinkedListNode<KeyValuePair<TKey, TValue>> bucketNode = bucket.First;
        while(bucketNode != null)
        {
            if (bucketNode.Value.Key.Equals(pair.Key))
            {
                bucketNode.Value = new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
                break;
            }
            bucketNode = bucketNode.Next;
        }
    }

    public void Update(TKey key, TValue value)
    {
        Update(new KeyValuePair<TKey, TValue>(key, value));
    }

    public void Remove(TKey key)
    {
        if (key == null) throw new ArgumentNullException();
        if (!KeyExists(key)) throw new KeyNotFoundException();
        
        int bucketIndex = GetBucketIndex(key);
        var bucket = _buckets[bucketIndex];

        LinkedListNode<KeyValuePair<TKey, TValue>> bucketNode = bucket.First;
        while(bucketNode != null)
        {
            if (bucketNode.Value.Key.Equals(key))
            {
                bucket.Remove(bucketNode);
                break;
            }
            bucketNode = bucketNode.Next;
        }
    }

    public void Remove(KeyValuePair<TKey, TValue> pair)
    {
        Remove(pair.Key);
    }

    public void Remove(TKey key, TValue value)
    {
        Remove(key);
    }

    public void Clear()
    {
        foreach (var bucket in _buckets)
        {
            if (bucket == null) continue;
            
            bucket.Clear();
        }
        
        _bucketCount = Constants.InitialBucketCount;
        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[_bucketCount];
    }

    public TValue GetValue(TKey key)
    {
        if (!KeyExists(key)) throw new KeyNotFoundException();
        
        int bucketIndex = GetBucketIndex(key);
        var bucket = _buckets[bucketIndex];

        foreach (var pair in bucket)
        {
            if(pair.Key.Equals(key))
                return pair.Value;
        }
        
        return default(TValue);
    }

    public List<TKey> Keys()
    {
        List<TKey> keys = new List<TKey>();

        List<KeyValuePair<TKey, TValue>> pairs = ToList();

        foreach (var pair in pairs)
        {
            keys.Add(pair.Key);
        }

        return keys;
    }
    
    public List<TValue> Values()
    {
        List<TValue> values = new List<TValue>();

        List<KeyValuePair<TKey, TValue>> pairs = ToList();

        foreach (var pair in pairs)
        {
            values.Add(pair.Value);
        }

        return values;
    }
    
    // ----------------- CRUD ----------------------------------------------

    public TValue this[TKey key]
    {
        get { return GetValue(key); }
        set { Update(key, value); }
    }

    public bool KeyExists(TKey key)
    {
        int bucketIndex = GetBucketIndex(key);
        var bucket = _buckets[bucketIndex];
        
        if (bucket == null) return false; // Bucket is empty, so it doesn't contain the key

        foreach (var entry in bucket)
        {
            if (entry.Key.Equals(key)) return true;
        }

        return false;
    }

    public List<KeyValuePair<TKey, TValue>> ToList()
    {
        List<KeyValuePair<TKey, TValue>> retList = new();

        foreach (var bucket in _buckets)
        {
            if(bucket == null) continue;

            foreach (var kvp in bucket)
            {
                retList.Add(kvp);
            }
        }

        return retList;
    }

    public int Length()
    {
        int count = 0;
        
        foreach (var bucket in _buckets)
        {
            if (bucket == null) continue;
            count += bucket.Count;
        }
        
        return count;
    }

    private int BucketCount()
    {
        return  _bucketCount;
    }

    private void IncreaseSize() // Create larger array, redistribute elements into new array, swap old array with new.
    {
        var newBucketCount = Constants.SizeIncreaseStep * _bucketCount;
        var newBuckets = new LinkedList<KeyValuePair<TKey, TValue>>[newBucketCount];
        
        foreach (var bucket in _buckets)
        {
            if (bucket == null) continue;

            foreach (var kvp in bucket)
            {
                int bucketIndex =  GetBucketIndex(kvp.Key, newBucketCount);
                
                var newBucket = newBuckets[bucketIndex];
                if (newBucket == null)
                {
                    newBucket = new LinkedList<KeyValuePair<TKey, TValue>>();
                    newBuckets[bucketIndex] = newBucket;
                }
                newBucket.AddFirst(new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value));
            }
        }
        
        _buckets = newBuckets;
        _bucketCount = newBucketCount;
    }

    private int GetBucketIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % _bucketCount;
    }

    private int GetBucketIndex(TKey key, int bucketCount)
    {
        return key.GetHashCode() % bucketCount;
    }

    // --------- IEnumerable Implementation --------------
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (var kvp in ToList())
        {
            yield return kvp;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    // --------- IEnumerable Implementation --------------
}