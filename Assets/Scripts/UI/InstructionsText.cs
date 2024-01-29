using System.Collections;
using UnityEngine;

namespace UI
{
    public class InstructionsText : MonoBehaviour
    {
        public float timeActive = 10f;
        private void Start()
        {
            StartCoroutine(DisableInstructions());
        }

        private IEnumerator DisableInstructions()
        {
            yield return new WaitForSeconds(timeActive);
            gameObject.SetActive(false);
        }
    }
}
