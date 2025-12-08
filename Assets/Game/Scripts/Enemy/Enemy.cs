using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Player;
    public Transform GunPivot;
    public GameObject Bullet;
    public Transform Tip;

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
        Vector3 relative = GunPivot.InverseTransformPoint(Player.transform.position);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        GunPivot.Rotate(0, 0, angle);
        if(this.transform.position.x - Player.transform.position.x > 0){
            GunPivot.rotation = Quaternion.Euler(0, 180, GunPivot.rotation.eulerAngles.z);
        }else{
            GunPivot.rotation = Quaternion.Euler(0, 0, GunPivot.rotation.eulerAngles.z);
        }
    }

    public void Shooting_Enemy(){
        GameObject E_bullet = Instantiate(Bullet, Tip.position, Quaternion.identity);
        Debug.Log("Bullet instanciada: " + E_bullet.name);

       Vector2 direction = (Player.transform.position - Tip.position).normalized;
       Debug.Log("Direccion: " + direction);

       E_bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 15;
       Debug.Log("Velocidad aplicada: " + (direction * 30));
    }   
}
