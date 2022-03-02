using UnityEngine;
using Weapons;
using AI;
using System.Collections;

public class BombTrigger : MonoBehaviour
{
    public bool bombHasBeenActivated;
    public bool rocketHasBeenActivated;
    public bool bombInstantCollision;

    private ParticleSystem _boomEffect;
    [SerializeField] private float bombTimer = 5f;
    public Vector3 BombScale = new Vector3(5f, 5f, 5f);

    private IExplosion _explosion;

    private void Awake()
    {
        if (gameObject != null)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        rocketHasBeenActivated = false;

        _explosion = gameObject.GetComponent<IExplosion>();

        _boomEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        _boomEffect.Stop();
    }

    private void Start()
    {
        StartCoroutine(CheckBomb());
    }

    private void Update()
    {
        BombTimer();
    }

    /// <summary>
    /// Checks if the BombTimer has expired and the bomb should explode
    /// </summary>
    private void BombTimer()
    {
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0 && (bombHasBeenActivated || rocketHasBeenActivated))
        {
            _explosion.Explode();
            _boomEffect.Play();
            _boomEffect.GetComponent<DestroyParticle>().DestroyParticleGameobject();
        }
    }

    /// <summary>
    /// Checks if a vehicle collides with the bomb.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (bombHasBeenActivated && bombInstantCollision && collision.transform.GetComponent<BaseVehicleManager>())
        {
            _explosion.Explode();
            _boomEffect.Play();
            _boomEffect.GetComponent<DestroyParticle>().DestroyParticleGameobject();
        }
    }

    /// <summary>
    /// Checks if the bomb is hit by a projectile.
    /// Checks if the Rocket launcher projectile hits: Player, AI, Wall, Road, Environment.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!rocketHasBeenActivated && (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ships")))
            StartCoroutine(CheckRocketProjectile());
        else
            rocketHasBeenActivated = true;

        if (bombHasBeenActivated && other.transform.CompareTag("Bullet") || (rocketHasBeenActivated && (other.gameObject.layer == LayerMask.NameToLayer("Roadtrack") || other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ships") || other.transform.CompareTag("Bomb") || other.transform.CompareTag("Environment"))))
        {
            _explosion.Explode();
            _boomEffect.Play();
            _boomEffect.GetComponent<DestroyParticle>().DestroyParticleGameobject();
        }
    }
    /// <summary>
    /// Ensures that the projectile does not instantly hit this vehicle.
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckRocketProjectile()
    {
        yield return new WaitForSeconds(0.5f);
        rocketHasBeenActivated = true;
    }

    /// <summary>
    /// Ensures that the bomb does not instantly explode
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckBomb()
    {
        yield return new WaitForSeconds(0.5f);
        bombInstantCollision = true;
    }
}

