using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace experimental
{
    public class EnemyController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(DelayDestroyEnemy());
        }



        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator DelayDestroyEnemy()
        {
            yield return new WaitForSeconds(60);
            Destroy(gameObject);
        }
    }
}