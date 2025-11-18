using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("배경 레이어")]
    [SerializeField] private GameObject[] _foreground;
    [SerializeField] private GameObject[] _midground;
    [SerializeField] private GameObject[] _background;
    [SerializeField] private GameObject[] _particle;
    
    [SerializeField] private GameObject[] _skyForeground;
    [SerializeField] private GameObject[] _skyParticle;

    [Header("속도 설정")]
    [SerializeField] private float _foregroundSpeed = 1f;
    [SerializeField] private float _midgroundSpeed = 0.8f;
    [SerializeField] private float _backgroundSpeed = 0.5f;
    [SerializeField] private float _particleSpeed = 0.2f;

    private GameSpeedController _speed;

    private float _spriteWidth;

    private void Awake()
    {
        _spriteWidth = _foreground[0].GetComponent<SpriteRenderer>().bounds.size.x;
        _speed = FindFirstObjectByType<GameSpeedController>();
    }

    private void Update()
    {
        float speedMultiplier = _speed.CurrentSpeed;

        MoveLayer(_foreground, _foregroundSpeed * speedMultiplier);
        MoveLayer(_midground, _midgroundSpeed * speedMultiplier);
        MoveLayer(_background, _backgroundSpeed * speedMultiplier);
        MoveLayer(_particle, _particleSpeed * speedMultiplier);
        MoveLayer(_skyForeground, _foregroundSpeed * speedMultiplier);
        MoveLayer(_skyParticle, _particleSpeed * speedMultiplier);
    }

    private void MoveLayer(GameObject[] layer, float speed)
    {
        for (int i = 0; i < layer.Length; i++)
        {
            layer[i].transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (layer[0].transform.position.x <= -_spriteWidth)
        {
            Vector3 lastPosition = layer[2].transform.position;
            lastPosition.x += _spriteWidth;

            GameObject first = layer[0];
            layer[0] = layer[1];
            layer[1] = layer[2];
            layer[2] = first;
            
            layer[2].transform.position = lastPosition;
        }
    }
}
