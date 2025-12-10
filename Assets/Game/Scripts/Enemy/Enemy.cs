using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Player;
    public Transform GunPivot;
    public GameObject Bullet;
    public Transform Tip;
    public float Health = 100;
    public Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("Shooting_Enemy", 1, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        // Calcular la dirección desde el arma hacia el jugador
        Vector2 direction = (Player.transform.position - GunPivot.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplicar la rotación directamente
        GunPivot.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Shooting_Enemy(){
        GameObject E_bullet = Instantiate(Bullet, Tip.position, Quaternion.identity);
        // Debug.Log("Bullet instanciada: " + E_bullet.name);

       Vector2 direction = (Player.transform.position - Tip.position).normalized;
    //    Debug.Log("Direccion: " + direction);

       E_bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 15;
    //    Debug.Log("Velocidad aplicada: " + (direction * 30));
    }   

    public void Damage(int dmg, Vector2 impactDirection = default, float impactForce = 0f){
        Health -= dmg;
        slider.value = Health/100;

        // Apply physics impact if force is provided
        if(impactForce > 0 && impactDirection != Vector2.zero){
            rb.AddForce(impactDirection.normalized * impactForce, ForceMode2D.Impulse);
        }

        if(Health <= 0){
            print("Die");
            Destroy(this.gameObject);
        }
        // else{
        //     slider.value = Health/100;
        // }
    }
}
