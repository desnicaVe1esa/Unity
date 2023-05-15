using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IDamagable
{
    public bool ApplyDamage(Rigidbody rigidbody, float force)
    {
        Debug.Log("I box");
        return true;
    }
}
