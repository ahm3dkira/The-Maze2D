using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;

    private bool touchStart = false;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)){
            touchStartPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        if(Input.GetMouseButton(0)){
            touchStart = true;
            touchEndPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        } else {
            touchStart = false;
        }


        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0f);
        GetComponent<Rigidbody2D>().velocity = movement * moveSpeed ;
    }
    private void FixedUpdate(){
        // dont move faster than moveSpeed
        if(touchStart){
            Vector2 offset = touchEndPos - touchStartPos;
            // Vector2 direction = Vector2.ClampMagnitude(offset, moveSpeed);
            GetComponent<Rigidbody2D>().velocity = offset.normalized * moveSpeed;
            // GetComponent<Rigidbody2D>().velocity = directio * moveSpeed 
        }

    }
}
