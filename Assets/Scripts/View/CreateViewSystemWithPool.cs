using Model;
using UnityEngine;
using UnityEngine.Pool;

namespace View
{
    public class CreateViewSystemWithPool : CreateViewSystem
    {
        private readonly ObjectPool<GameObject> _pool;

        public CreateViewSystemWithPool(Game game, GameModelControlSystem controlSystem) : base(game, controlSystem)
        {
            _pool = new ObjectPool<GameObject>(InstantiateView, GetView, ReleaseView, DestroyView);
        }

        protected override void OnModelCreated(GameModel model)
        {
            var presenter = _pool.Get().GetComponent<Presenter>();
            presenter.Model = model;
            presenter.ModelDestroyed += OnModelDestroyed;
        }
        
        protected override void OnModelDestroyed(Presenter presenter)
        {
            presenter.ModelDestroyed -= OnModelDestroyed;
            _pool.Release(presenter.gameObject);
        }

        private GameObject InstantiateView()
        {
            return Object.Instantiate(ControlSystem.GameModelSettings.Prefab);
        }

        private void GetView(GameObject view)
        {
            view.SetActive(true);
        }

        private void ReleaseView(GameObject view)
        {
            view.SetActive(false);
        }

        private void DestroyView(GameObject view)
        {
            Object.Destroy(view);
        }
    }
}