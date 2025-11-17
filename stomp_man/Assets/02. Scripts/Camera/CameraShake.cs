using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _shakeAmount = 0.2f;
    [SerializeField] private float _shakeTime = 0.4f;

    private Vector3 _originalLocalPos;

    private void Awake()
    {
        _originalLocalPos = transform.localPosition;
    }

    public void Play(float shakeAmount)
    {
        StopAllCoroutines();
        StartCoroutine(Shake(shakeAmount));
    }

    private IEnumerator Shake(float shakeAmount)
    {
        float timer = 0;

        while (timer < _shakeTime)
        {
            Vector2 offset = Random.insideUnitCircle * shakeAmount;

            transform.localPosition = _originalLocalPos + new Vector3(offset.x, offset.y, 0);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _originalLocalPos;
    }
}
