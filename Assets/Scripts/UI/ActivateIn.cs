using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ActivateIn : MonoBehaviour
{
    [SerializeField]
    private float time = 0.1f;

    [SerializeField]
    private UnityEvent onEvent;

    private Coroutine coroutine;

    public void OnEnable()
    {
        coroutine = StartCoroutine(WaitAndDeactivate());
    }

    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(time);
        onEvent.Invoke();
    }

    public void Stop()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = null;
    }

    public void Handle()
    {
        onEvent.Invoke();
    }

    private void OnDisable()
    {
        Stop();
    }
}
