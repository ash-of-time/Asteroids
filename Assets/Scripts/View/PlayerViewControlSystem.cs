using Model;
using UnityEngine;

namespace View
{
	public class PlayerViewControlSystem : ViewControlSystem
	{
		public PlayerViewControlSystem(Game game, GameModelControlSystem controlSystem) : base(game, controlSystem)
		{
		}
		
		protected override void OnModelCreated(IGameModel gameModel)
		{
			var gameObject = Object.Instantiate(ControlSystem.GameModelSettings.Prefab, gameModel.Position.Value,
				gameModel.Rotation.Value);
            
			var presenter = gameObject.GetComponent<Presenter>();
			presenter.Initialize(Game, gameModel);
			presenter.ModelDestroyed += OnModelDestroyed;

			var inputController = gameObject.GetComponent<InputController>();
			inputController.Game = Game;
		}
	}
}