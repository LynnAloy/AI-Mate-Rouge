using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace experimental
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseCanvas;

        private void Awake()
        {
            pauseCanvas.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                pauseCanvas.SetActive(!pauseCanvas.activeSelf);
            }
        }
    }
}