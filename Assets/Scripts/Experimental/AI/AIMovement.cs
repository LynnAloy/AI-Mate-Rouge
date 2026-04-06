using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace experimental
{
    public class AIMovement : MonoBehaviour
    {
        [SerializeField] private float aiSpeed = 4.0f;

        public void FollowPlayer(GameObject target)
        {
            if (target == null) return;
            StopAllCoroutines();
            StartCoroutine(FollowPlayerCoroutine(target));
        }

        private IEnumerator FollowPlayerCoroutine(GameObject target)
        {
            while (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, aiSpeed * Time.deltaTime);
                /*
                if (Vector3.Distance(transform.position, target.transform.position) < 0.8f)
                {
                    break;
                }
                */
                yield return null;
            }
            //StopFollowPlayer();
        }

        private void StopFollowPlayer()
        {
            StopAllCoroutines();
        }
    }
}