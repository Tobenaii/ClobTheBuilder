using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Item
{
    [SerializeField]
    private float m_force;
    [SerializeField]
    private float m_exitForceMultiplier;

    public override void OnStay(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * m_force, ForceMode.Acceleration);
    }

    public override void OnExit(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().velocity *= m_exitForceMultiplier;
    }
}
