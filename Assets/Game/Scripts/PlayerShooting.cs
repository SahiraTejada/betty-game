using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Joystick joystick;
    public Rigidbody2D rb;
    public Transform Sign;
    public Transform Hand;
    public GameObject Bullet;
    public Transform Tip;

    private Animator animator;
    private bool isShooting = false;
    private float nextFireTime = 0f;
    public float fireRate = 0.1f; // Tiempo entre disparos (0.5 = 2 disparos por segundo)

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si está disparando, disparar automáticamente con cooldown
        if (isShooting)
        {
            if (Time.time >= nextFireTime)
            {
                Shooting();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    // Método para iniciar el disparo - Conecta este al Event Trigger OnPointerDown del joystick
    public void StartShooting()
    {
        Debug.Log("StartShooting llamado");
        isShooting = true;
        // Activar animación de shooting
        if (animator != null)
        {
            animator.SetInteger("Mode", 2);
        }
    }

    // Método para detener el disparo - Conecta este al Event Trigger OnPointerUp del joystick
    public void EndShooting()
    {
        Debug.Log("EndShooting llamado");
        isShooting = false;
        // Volver a idle
        if (animator != null)
        {
            animator.SetInteger("Mode", 0);
        }
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2(joystick.Horizontal, joystick.Vertical);
        Vector2 newPosition =(Vector2)Sign.position  + input * 30 * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -9.8f, +9.8f);
        newPosition.y = Mathf.Clamp(newPosition.y, -4.17f, +4.549f);
        rb.MovePosition(newPosition);

        Vector3 relative = Hand.InverseTransformPoint(Sign.position);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        Hand.Rotate(0, 0, angle);

    }

    public void Shooting(){
       Debug.Log("=== SHOOTING LLAMADO ===");
       Debug.Log("Tip position: " + Tip.position);
       Debug.Log("Sign position: " + Sign.position);

       GameObject bullet = Instantiate(Bullet, Tip.position, Quaternion.identity);
       Debug.Log("Bullet instanciada: " + bullet.name);

       Vector2 direction = (Sign.position - Tip.position).normalized;
       Debug.Log("Direccion: " + direction);

       bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 30;
       Debug.Log("Velocidad aplicada: " + (direction * 30));
    }
}
