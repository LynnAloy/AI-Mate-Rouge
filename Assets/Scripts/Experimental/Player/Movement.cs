using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace experimental
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 0.8f;


        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = speed * Time.deltaTime * new Vector3(horizontal, vertical, 0);
            transform.Translate(movement);
        }
    }
}