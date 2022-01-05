using UnityEngine;
using Weapons;

public class BombTrigger : MonoBehaviour
{
    private PlayerManager _playerManager;
    private ParticleSystem _boomEffect;
    public bool hasBeenActivated = false;
    public float bombTimer;

    private IExplosion _explosion;
    
    private void Awake()
    {
        _explosion = GetComponent<IExplosion>();
        
        _playerManager = FindObjectOfType<PlayerManager>();
        bombTimer = _playerManager.bombTimer;
        
        _boomEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        _boomEffect.Stop();
    }

    private void Update()
    {
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0 && hasBeenActivated)
        {
            _explosion.Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((hasBeenActivated && collision.transform.CompareTag("Player") && !_playerManager.isImmortal) || hasBeenActivated && collision.transform.CompareTag("Bullet"))
        {
            _explosion.Explode();
        }
    }
}
