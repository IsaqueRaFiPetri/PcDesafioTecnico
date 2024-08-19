using UnityEngine;

public class GunSystem : MonoBehaviour
{
    //Gun Stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHolds;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;

    //References
    public Camera cam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    //public CamShake

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        if(allowButtonHolds)
        {
            shooting = Input.GetButton("Fire1");
        }
        else
        {
            shooting = Input.GetButtonDown("Fire1");
        }
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) 
        {
            Reload();
        }

        //Shoot
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread


        //RayCast
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                //rayHit.collider.GetComponent<ShootingAI>().TakeDamage(damage);
            }
        }

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShoot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft> 0)
        {
            Invoke("ResetShoot", timeBetweenShooting);
        }
    }
    private void ResetShoot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
//https://www.youtube.com/watch?v=bqNW08Tac0Y