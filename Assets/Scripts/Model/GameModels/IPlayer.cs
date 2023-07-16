using System.Collections.Generic;
using Tools;

namespace Model
{
    public interface IPlayer : IGameModel
    {
        public float GivenRotation { get; set; }
        public float GivenAcceleration { get; set; }
        public IReadOnlyReactiveProperty<float> Velocity { get; }
        public IReadOnlyReactiveProperty<int> LaserCharges { get; }
        public IReadOnlyReactiveProperty<float> LaserReloadTime { get; }
        public void Fire();
        public bool TryAlternativeFire(IEnumerable<IGameModel> hitModelsList);
    }
}