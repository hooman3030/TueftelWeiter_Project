using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public float fireRate = .25f;
    public Transform gunBarrel;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource shotSound;
    private LineRenderer laserLine;
    private float nextFire;


    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        shotSound = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("shoot") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        }
    }


    private IEnumerator ShotEffect()
    {
        shotSound.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        //laserLine enabled = false;
        
    }
}
