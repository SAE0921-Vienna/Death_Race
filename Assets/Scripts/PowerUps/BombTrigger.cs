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

    private BaseVehicleManager _baseVehicleManager;
    private IExplosion _explosion;

    private void Awake()
    {
        if (gameObject != null)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        _explosion = gameObject.GetComponent<IExplosion>();

        //_baseVehicleManager = FindObjectOfType<BaseVehicleManager>();
        //_bombTimer = _baseVehicleManager.bombTimer;

        _boomEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        _boomEffect.Stop();
    }
    private void Start()
    {
        StartCoroutine(CheckBomb());
    }

    private void Update()
    {
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0 && (bombHasBeenActivated || rocketHasBeenActivated))
        {
            _explosion.Explode();
            _boomEffect.Play();
            _boomEffect.GetComponent<DestroyParticle>().DestroyParticleGameobject();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (bombHasBeenActivated && (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Ships")))
        //{
        //    bombWhenNotOnTrigger = false;
        //    StartCoroutine(CheckBomb());
        //}
        //else
        //    bombWhenNotOnTrigger = true;

        Debug.Log("OCE" + collision.gameObject.name);

        if (bombHasBeenActivated && bombInstantCollision && collision.transform.GetComponent<BaseVehicleManager>())
        {
            _explosion.Explode();
            _boomEffect.Play();
            _boomEffect.GetComponent<DestroyParticle>().DestroyParticleGameobject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!rocketHasBeenActivated && (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ships")))
            StartCoroutine(CheckRocketProjectile());
        else
            rocketHasBeenActivated = true;

        if (bombHasBeenActivated && other.transform.CompareTag("Bullet") || (rocketHasBeenActivated && (other.gameObject.layer == LayerMask.NameToLayer("Roadtrack") || other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ships") || other.transform.CompareTag("Bomb"))))
        {
            _explosion.Explode();
            _boomEffect.Play();
            _boomEffect.GetComponent<DestroyParticle>().DestroyParticleGameobject();
        }
    }
    public IEnumerator CheckRocketProjectile()
    {
        yield return new WaitForSeconds(0.5f);
        rocketHasBeenActivated = true;
    }
    public IEnumerator CheckBomb()
    {
        yield return new WaitForSeconds(0.5f);
        bombInstantCollision = true;
    }
}

