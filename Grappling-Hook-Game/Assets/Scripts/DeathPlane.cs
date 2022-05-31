using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private float playerPosBuffer = 100f;

    private void Update()
    {
        if (playerTransform != null)
        {
            if ((playerTransform.position.z - gameObject.transform.position.z) > playerPosBuffer)
            {
                gameObject.transform.position = new Vector3(0, -10, playerTransform.position.z);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().enabled = false;
            GameStateManager.Instance.OnFail();
        }
    }
}
