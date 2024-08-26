using System;
using System.Collections;
using UnityEngine;

namespace Ecs.Services
{
    public class CoroutineRunner : MonoBehaviour
    {
        public void TimerExecute(float time, Action action)
        {
            StartCoroutine(TimerExecuteCoroutine(time, action));
        }

        private IEnumerator TimerExecuteCoroutine(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
    }
}