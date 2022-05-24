using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().enabled = false;
            GameStateManager.Instance.OnFail();
        }
    }
}
