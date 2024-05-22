using System.Collections;
using UnityEngine;

namespace Source.Root
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator routine);

        public void StopCoroutine(Coroutine coroutine);
    }
}
