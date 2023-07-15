using System.Collections.Generic;

namespace Model
{
    public interface IPlayer : IGameModel
    {
        public float GivenRotation { get; set; }
        public float GivenAcceleration { get; set; }
        public void Fire();
        public bool TryAlternativeFire(IEnumerable<IGameModel> hitModelsList);
    }
}