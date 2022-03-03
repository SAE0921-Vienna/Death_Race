using UnityEngine;
using PathCreation;

public class AIFollowCurve : MonoBehaviour
{
    public float MaxSpeed;
    public float Speed;

    [SerializeField] private float speedMaxBuff = 22.9f;
    [SerializeField] private float speedMinBuff = 22.45f;

    [SerializeField] private EndOfPathInstruction end;
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private float accelerationConstant;

    private float _distanceTravelled;
    private GameManager _gameManager;
    private bool _canDrive;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.StartOfRace += () => { _canDrive = true; };

        MaxSpeed = Random.Range(speedMinBuff, speedMaxBuff);
    }


    private void FixedUpdate()
    {
        TravelAlongCurve();
        Accelerate();
    }

    private void TravelAlongCurve()
    {
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, end);

        _distanceTravelled += Speed * Time.fixedDeltaTime;
    }

    private void Accelerate()
    {
        if (!_canDrive) return;
        Speed = Mathf.MoveTowards(Speed, MaxSpeed, accelerationConstant);
    }

    public float GetCurrentSpeed()
    {
        return Mathf.InverseLerp(0f, MaxSpeed, Speed);
    }
}
