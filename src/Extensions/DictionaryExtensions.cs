namespace AoC.Extensions;

static class DictionaryExtensions
{
    extension<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        where TKey : notnull
    {
        public TValue FluentAdd(TKey key, TValue value)
        {
            dictionary.Add(key, value);
            return value;
        }
    }
}
