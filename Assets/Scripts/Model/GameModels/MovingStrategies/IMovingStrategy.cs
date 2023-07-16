using UnityEngine;

namespace Model
{
	public interface IMovingStrategy
	{
		Vector3 MoveDelta();
	}
}