using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerVFX : MonoBehaviour
{
    [Header("카메라 흔들기 값")]
    [SerializeField] private float _smallShakeValue = 0.2f;
    [SerializeField] private float _bigShakeValue = 0.6f;

    [Header("피격 이펙트")]
    [SerializeField] private float _flashDuration = 0.1f;
    [SerializeField] private Color _flashColor = Color.red;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private CameraShake _cameraShake;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    public void PlayBigHitEffect(Vector3 position)
    {
        _cameraShake.Play(_bigShakeValue);
        SoundManager.Instance.PlaySFX(SoundManager.Sfx.STOMP);
    }

    public void PlaySmallHitEffect(Vector3 position)
    {
        _cameraShake.Play(_smallShakeValue);
        SoundManager.Instance.PlaySFX(SoundManager.Sfx.STOMP);
    }

    public void PlayDamagedEffect(Vector3 position)
    {
        _cameraShake.Play(_smallShakeValue);
        StartCoroutine(Flash());
        EffectManager.Instance.PlayBloodEffect(position);
    }

    private IEnumerator Flash()
    {
        _spriteRenderer.color = _flashColor;
        yield return new WaitForSeconds(_flashDuration);
        _spriteRenderer.color = _originalColor;
    }
}
