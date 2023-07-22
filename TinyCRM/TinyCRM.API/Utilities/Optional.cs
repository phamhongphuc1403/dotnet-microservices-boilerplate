using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Utilities
{
    public class Optional<T> where T : GuidBaseEntity
    {
        private readonly T? _instance;

        private Optional(T? instance)
        {
            _instance = instance;
        }

        public static Optional<T> Of(T? instance)
        {
            return new Optional<T>(instance);
        }

        public Optional<T> ThrowIfPresent(Exception exception)
        {
            return _instance != null ? throw exception : this;
        }

        public Optional<T> ThrowIfNotPresent(Exception exception)
        {
            return _instance == null ? throw exception : this;
        }

        public T Get()
        {
            return _instance;
        }
    }
}
