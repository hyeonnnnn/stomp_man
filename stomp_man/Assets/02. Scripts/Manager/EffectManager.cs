using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;
    public static EffectManager Instance => _instance;

    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private GameObject _lightEffect;
    [SerializeField] private GameObject _scoreEffect;
    [SerializeField] private GameObject _bloodEffect;

    [SerializeField] private float _duration = 2f;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public void PlayEffect(GameObject effectPrefab, Vector3 position)
    {
        GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(effect, _duration);
    }

    public void PlayExplosionEffect(Vector3 position)
    {
        PlayEffect(_explosionEffect, position);
    }

    public void PlayLightEffect(Vector3 position)
    {
        PlayEffect(_lightEffect, position);
    }

    public void PlayScoreEffect(Vector3 position)
    {
        PlayEffect(_scoreEffect, position);
    }

    public void PlayBloodEffect(Vector3 position)
    {
        PlayEffect(_bloodEffect, position);
    }
}
