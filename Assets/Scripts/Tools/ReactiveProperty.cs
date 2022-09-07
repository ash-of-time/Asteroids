using System;

namespace Tools
{
    public class ReactiveProperty<T>
    {
        private T _value;

        public event Action<T> Changed;

        public ReactiveProperty()
        {
            _value = default(T);
        }

        public ReactiveProperty(T value)
        {
            _value = value;
        }

        public T Get() => _value;

        public void Set(T value)
        {
            _value = value;
            Changed?.Invoke(value);
        }
    }
}