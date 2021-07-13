using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject[] targets;
    public Collider spawnVolume;
    public int spawnAmount = 1;

    public Material[] mats;

    GameObject projector;
    GameObject newTarget;

    
    int randomInt;
    int randomMat;

    public void Start() 
    {
        projector = GameObject.Find("Projector");
    }


    // checks and spawns targets
    public void SpawnTarget()
    {
        // checks wether projector is turned on
        if(projector.GetComponent<Projector>().isOn == false)
        {
            return;
        }

        // instantiates targets
        for (int i = 0; i < spawnAmount; i++)
        {
            randomInt = Random.Range(0, targets.Length);
            randomMat = Random.Range(0, mats.Length);
            
            newTarget = Instantiate(targets[randomInt], randomSpawnPos(spawnVolume.bounds), new Quaternion(0,0,0,0));
            newTarget.GetComponent<Renderer>().sharedMaterial = mats[randomMat];
        }
    }


    // calculates random position within bounds
    private Vector3 randomSpawnPos(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

}
