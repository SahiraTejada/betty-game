using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Joystick joystick;
    public Rigidbody2D rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2(joystick.Horizontal, 0);
        rb.MovePosition((Vector2)transform.position  + input * 10 * Time.deltaTime);

        if (input.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Animator>().SetInteger("Mode", 1);
        }
        else if (input.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
             GetComponent<Animator>().SetInteger("Mode", 1);
        }
    }
}
