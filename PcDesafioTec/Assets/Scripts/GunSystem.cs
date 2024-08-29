using UnityEngine;
using TMPro;

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
    public GameObject muzzleFlash, bulletHoleGraphic;
    public CameraShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    //UI / UX
    public GameObject reloadingPainel;
    public AudioSource shootingAudio;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);

        if(reloading == true)
        {
            reloadingPainel.SetActive(true);
        }
        else
        {
            reloadingPainel.SetActive(false);
        }
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
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = cam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                print("acertou");
                rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        //ShakeCamera
        camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics
        float angle = Vector3.Angle(rayHit.normal, Vector3.forward);
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, angle, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        shootingAudio.Play();

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