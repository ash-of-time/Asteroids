using UnityEngine;

namespace Model
{
	public class FollowMovingStrategy : IMovingStrategy
	{
		private IGameModel _selfGameModel;
		private IGameModel _followedGameModel;
		
		public FollowMovingStrategy(IGameModel selfGameModel, IGameModel followedGameModel)
		{
			_selfGameModel = selfGameModel;
			_followedGameModel = followedGameModel;
		}

		public Vector3 MoveDelta()
		{
			var direction = (_followedGameModel.Position.Value - _selfGameModel.Position.Value).normalized;
			return direction * (_selfGameModel.Settings.MaxVelocity * Time.deltaTime);
		}
	}
}