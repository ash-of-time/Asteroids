using System.Collections.Generic;

namespace Model
{
    public interface IPlayer
    {
        public float GivenRotation { get; set; }
        public float GivenAcceleration { get; set; }
        public void Fire();
        public bool TryAlternativeFire(List<GameModel> hitModelsList);
    }
}