using UnityEngine;
using Weapons;

public class BombTrigger : MonoBehaviour
{
    public bool hasBeenActivated;
    
    private ParticleSystem _boomEffect;
    private float _bombTimer;

    private PlayerManager _playerManager;
    private IExplosion _explosion;
    
    private void Awake()
    {
        _explosion = GetComponent<IExplosion>();
        
        _playerManager = FindObjectOfType<PlayerManager>();
        _bombTimer = _playerManager.bombTimer;
        
        _boomEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        _boomEffect.Stop();
    }

    private void Update()
    {
        _bombTimer -= Time.deltaTime;
        if (_bombTimer <= 0 && hasBeenActivated)
            _explosion.Explode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((hasBeenActivated && collision.transform.CompareTag("Player") && !_playerManager.isImmortal) || hasBeenActivated && collision.transform.CompareTag("Bullet"))
            _explosion.Explode();
    }
}
