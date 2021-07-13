using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletHole;
    public LineRenderer beam;

    public AudioClip laserZap;
    public AudioClip impactSound;


    public void Shoot()
    {
        //muzzleFlash.Play();
        RaycastHit hit;

        if(Physics.Raycast(bulletHole.transform.position, bulletHole.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            GetComponent<AudioSource>().PlayOneShot(laserZap);

            beam.enabled = true;
            beam.SetPosition(0, bulletHole.transform.position);
            beam.SetPosition(1, hit.point);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.getShot();
                GetComponent<AudioSource>().PlayOneShot(impactSound);
            }

        } 
        else
        {
            beam.enabled = false;
        }
    }

    public void ReleaseTrigger()
    {
        beam.enabled = false;
    }
}
