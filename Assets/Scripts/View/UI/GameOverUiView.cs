using TMPro;
using UnityEngine;

namespace View
{
    public class GameOverUiView : UiView
    {
        public GameOverUiPresenter Presenter { get; set; }

        [field: SerializeField] public TextMeshProUGUI Points { get; set; }

        public void ButtonClick()
        {
            Presenter.ButtonClick();
        }
    }
}