using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDeletion : MonoBehaviour
{

    private PlayerLocation pLocation;
    private float wallPos;
    private float playerDist;

    private void OnEnable()
    {
        pLocation = GameObject.FindObjectOfType<PlayerLocation>();
        wallPos = CalculateWallPosition();
        Debug.Log(CalculatePlayerDist());
    }

    // Update is called once per frame
    void Update()
    {
        playerDist = CalculatePlayerDist();

        if (playerDist > 100)
        {
            Destroy(gameObject);
        }
    }

    private float CalculateWallPosition()
    {
        return transform.position.z - pLocation.GetStartingPos();
    }

    private float CalculatePlayerDist()
    {
        return pLocation.GetPlayerPosition() - wallPos;
    }
}
