using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    public GameObject[] wallSets;
    public float wallDist = 32;

    private float currentZPos;
    private float startingZPos;

    private Vector3 newWallPos;

    private float incTracker;
    private float updatingIncTracker;

    private float wallZPos;



    // Start is called before the first frame update
    void Start() 
    {
        Vector3 startingPos = transform.position;
        startingZPos = transform.position.z;

        newWallPos = new Vector3(startingPos.x, startingPos.y + 10, startingPos.z + 20);
        for (int i = 0; i < 2; i++)
        {
            wallZPos = wallDist * i;
            Instantiate(wallSets[0], new Vector3(newWallPos.x, newWallPos.y, newWallPos.z + wallZPos), Quaternion.identity);
        }

        incTracker = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentZPos = transform.position.z - startingZPos;

        //tracking if the player has moved a certain increment forward
        updatingIncTracker = Mathf.Floor(currentZPos / wallDist);

        if (incTracker < updatingIncTracker)
        {
            for (int i = 0; i < (updatingIncTracker - incTracker); i++)
            {
                wallZPos += wallDist;

                int randomNum = Random.Range(0, wallSets.Length);
                Instantiate(wallSets[randomNum], new Vector3(newWallPos.x, newWallPos.y, newWallPos.z + wallZPos), Quaternion.identity);
            }

            incTracker = updatingIncTracker;
        }

    }

    public float GetPlayerPosition()
    {
        return currentZPos; 
    }

    public float GetStartingPos()
    {
        return startingZPos;
    }
}
