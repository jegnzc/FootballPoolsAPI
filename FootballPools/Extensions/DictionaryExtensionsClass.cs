namespace FootballPools.Extensions;

public static class DictionaryExtensionsClass
{
    public static void RemoveKeywords(this Dictionary<string, string> dictionary, List<string> keywords)
    {
        var toRemove = dictionary.Where(pair => keywords.Contains(pair.Key))
            .Select(pair => pair.Key)
            .ToList();

        foreach (var key in toRemove)
        {
            dictionary.Remove(key);
        }
    }

    //public static Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>
    //    (this Dictionary<TKey, TValue> original) where TValue : ICloneable
    //{
    //    Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
    //        original.Comparer);
    //    foreach (KeyValuePair<TKey, TValue> entry in original)
    //    {
    //        ret.Add(entry.Key, (TValue)entry.Value.Clone());
    //    }
    //    return ret;
    //}
}