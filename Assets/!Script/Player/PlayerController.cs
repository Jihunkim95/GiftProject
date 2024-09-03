using UnityEngine;
using System.Collections;

// Controls player movement and rotation.
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.

    private Rigidbody rb; // Reference to player's Rigidbody.
    public GameObject onCollectEffect;

    public float jumpForce = 0.5f;
    private bool vfxTriggered = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {
        //Jump는 기본적으로 spacebar임
        if (Input.GetButtonDown("Jump")){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        if (FindObjectsOfType<Collections>().Length == 0 && !vfxTriggered){
            
            //지연함수 코루틴 실행
            StartCoroutine(TriggerVFXWithDelay(0.5f));

            vfxTriggered = true;
        }
    }
    private IEnumerator TriggerVFXWithDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        // VFX 효과 실행
        Instantiate(onCollectEffect, transform.position, Quaternion.identity);
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Move player based on vertical input.
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}