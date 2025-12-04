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
        rb.linearVelocity = new Vector2(joystick.Horizontal * 500 * Time.deltaTime, this.rb.linearVelocity.y);

        // Clamp para mantener el player dentro del canvas
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -6.1f, 6.1f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -4.17f, 4.549f);
        transform.position = clampedPosition;

        // Debug.Log("clampedPosition: " + clampedPosition);
        // Debug.Log("clampedPosition.x: " + clampedPosition.x);
        // Debug.Log("clampedPosition.y: " + clampedPosition.y);

        if (input.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Animator>().SetInteger("Mode", 1);
        }
        else if (input.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
             GetComponent<Animator>().SetInteger("Mode", 1);
        }else{
            GetComponent<Animator>().SetInteger("Mode", 0);
        }
    }
    public void Jump(){
        rb.linearVelocity = new Vector2(0, 15);
    }
}
