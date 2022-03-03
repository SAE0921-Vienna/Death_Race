using Core;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{

    public Timer timer;
    public GameObject gameobject;

    private void Start()
    {
        timer.CreateTimer(5f, () => { gameobject.SetActive(true); });
    }

}
