using System.Collections;
using UnityEngine;

namespace Tools
{
    public class DelayedDisable : MonoBehaviour
    {
        [SerializeField] private float Delay;
        
        private void OnEnable()
        {
            StartCoroutine(DisableCoroutine());
        }

        private IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(Delay);
            gameObject.SetActive(false);
        }
    }
}