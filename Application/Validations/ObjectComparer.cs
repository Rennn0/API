using System.Diagnostics.CodeAnalysis;

namespace Application.Validations
{
    public sealed class ObjectComparer<T> : IEqualityComparer<T>
        where T : class
    {
        public bool Equals(T? x, T? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;

            var type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                if (!object.Equals(prop.GetValue(x), prop.GetValue(y)))
                    return false;
            }
            return true;
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            unchecked
            {
                var type = typeof(T);
                var hashCode = 0;
                foreach (var prop in type.GetProperties())
                {
                    var value = prop.GetValue(obj);
                    if (value != null)
                        hashCode = (hashCode * 397) ^ value.GetHashCode();
                }
                return hashCode;
            }
        }
    }
}