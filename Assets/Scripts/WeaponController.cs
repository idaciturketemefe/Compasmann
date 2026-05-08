using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Ateþ Ayarlarý")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; // Mermi çýkýþ noktasý
    [SerializeField] private float fireRate = 0.2f;

    private float fireTimer;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // FirePoint yoksa silahýn ucunu kullan
        if (firePoint == null)
            firePoint = transform;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        // Sol týk basýlý tutunca ateþ et (Soul Knight stili auto-fire)
        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
    }

    void Shoot()
    {
        // Mouse yönü
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 shootDirection = (mousePos - transform.position).normalized;

        // Mermi oluþtur
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Mermiyi yönlendir
        Projectile proj = bullet.GetComponent<Projectile>();
        if (proj != null)
            proj.Setup(shootDirection);

        // Mermiyi mouse yönüne döndür (görsel)
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}