using System;
using Tools;
using UnityEngine;

namespace Model
{
	public interface IGameModel : IUpdatable
	{
		public event Action<IGameModel, bool> Destroyed;
		
		public ReactiveProperty<Vector3> Position { get; }

		public ReactiveProperty<Quaternion> Rotation { get; }
		
		public Vector3 ForwardDirection { get; }

		public void Collide(IGameModel gameModel);

		public void Destroy(bool totally);
	}
}