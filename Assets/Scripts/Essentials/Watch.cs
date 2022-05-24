using System;

namespace Essentials
{
    public struct Watch<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (_value == null)
                {
                    if (value == null)
                        return;
                    PreviousValue = _value;
                    _value = value;
                    OnChanged?.Invoke(PreviousValue, _value);
                }
                else if (!_value.Equals(value))
                {
                    PreviousValue = _value;
                    _value = value;
                    OnChanged?.Invoke(PreviousValue, _value);
                }
            }
        }

        public T PreviousValue { get; private set; }

        public Action<T, T> OnChanged;

        public T this[int index]
        {
            set
            {
                if (index != 0)
                    throw new Exception("Only 0 index is allowed");
                Value = value;
            }
        }

        public override string ToString() => Value.ToString();
        public override bool Equals(object obj) => Value.Equals(obj);
        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator T(Watch<T> me) => me.Value;
    }

}