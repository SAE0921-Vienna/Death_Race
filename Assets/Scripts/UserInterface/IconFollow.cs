using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconFollow : MonoBehaviour
{

    public GameObject icon;
    public Vector3 iconPos;
    public GameObject followTarget;

    private void Start()
    {
        icon = this.gameObject;
    }

    private void Update()
    {
        FollowObject(followTarget);
    }


    public void FollowObject(GameObject _followTarget)
    {
        iconPos.x = _followTarget.transform.position.x;
        iconPos.z = _followTarget.transform.position.z;
        iconPos.y = 9912.577f;
        transform.position = iconPos;
    }
}
