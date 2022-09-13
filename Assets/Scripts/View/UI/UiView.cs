using UnityEngine;

namespace View
{
    public abstract class UiView : MonoBehaviour
    {
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}