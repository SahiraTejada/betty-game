using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   public GameObject Destroy_Particle;

   private void OnTriggerEnter2D(Collider2D collision)
   {

    GameObject particle = Instantiate(Destroy_Particle, this.transform.position, Quaternion.identity);
    Destroy(particle, 1f);
    Destroy(this.gameObject);
   }
}
