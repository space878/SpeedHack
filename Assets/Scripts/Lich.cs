using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich : MonoBehaviour
{
    Vector3 moveVec;

    float Speed = 5;

    void Start()
    {
        moveVec = new Vector3(0, 0, 1);
    }

    void Update()
    {
        transform.Translate(moveVec * Speed * Time.deltaTime);
    }
}
