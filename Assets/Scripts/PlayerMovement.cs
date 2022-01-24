using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    CharacterController cc;
    Vector3 moveVec, gravity;

    public float Speed = 5, 
                JumpSpeed = 12;

    int laneNumber = 1,
    lanesCount = 2;

    public float FirstLanePos,
                LaneDistance,
                SideSpeed;     

    void Start()
    {
        cc = GetComponent<CharacterController>();
        moveVec = new Vector3(1, 0, 0);
        gravity = Vector3.zero;
    }


    void Update()
    {
        if (cc.isGrounded)
        {
            gravity = Vector3.zero;

            if (Input.GetAxisRaw("Vertical") > 0)
                gravity.y = JumpSpeed;
        }
        else
            gravity += Physics.gravity * Time.deltaTime * 3;

        moveVec.x = Speed;
        moveVec += gravity;
        moveVec *= Time.deltaTime;

        CheckInput();

        cc.Move(moveVec);

        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, FirstLanePos + (laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;

    }

    void CheckInput()
    {
            int sign = 0;

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                sign = -1;
            else if (Input.GetKeyDown(KeyCode.D) || 
                Input.GetKeyDown(KeyCode.RightArrow))
                sign = 1;
                else
                    return;

            laneNumber += sign;
            laneNumber = Mathf.Clamp(laneNumber, 0, lanesCount);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Trap"))
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
