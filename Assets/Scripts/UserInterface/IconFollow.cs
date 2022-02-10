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
        if (followTarget == null)
        {
            icon.GetComponent<MeshRenderer>().enabled = false;
        }

    }

    private void Update()
    {
        if (followTarget != null)
        {
            FollowObject(followTarget);
            icon.GetComponent<MeshRenderer>().enabled = true;
        }


    }


    public void FollowObject(GameObject _followTarget)
    {
        iconPos.x = _followTarget.transform.position.x;
        iconPos.z = _followTarget.transform.position.z;
        iconPos.y = 9912.577f;
        transform.position = iconPos;
    }
}
