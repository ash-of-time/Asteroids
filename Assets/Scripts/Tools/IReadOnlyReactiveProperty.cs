using System;

namespace Tools
{
	public interface IReadOnlyReactiveProperty<T>
	{
		public event Action<T> Changed;

		public T Value { get; }
	}
}