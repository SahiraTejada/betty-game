using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Joystick joystick;
    public Rigidbody2D rb;
    public Transform Sign;
    public Transform Hand;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
