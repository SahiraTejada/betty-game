using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Player;
    public Transform Hand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Vector3 relative = Hand.InverseTransformPoint(Player.transform.position);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        Hand.Rotate(0, 0, angle);
    }
}
