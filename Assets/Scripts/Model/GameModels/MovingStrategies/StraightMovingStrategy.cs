using UnityEngine;

namespace Model
{
	public class StraightMovingStrategy : IMovingStrategy
	{
		private IGameModel _gameModel;
		
		public StraightMovingStrategy(IGameModel gameModel)
		{
			_gameModel = gameModel;
		}

		public Vector3 MoveDelta()
		{
			return _gameModel.ForwardDirection * (_gameModel.Settings.MaxVelocity * Time.deltaTime);
		}
	}
}