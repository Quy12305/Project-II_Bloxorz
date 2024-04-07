using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    bool isMoving;

    // Update is called once per frame
    void Update()
    {
        if(isMoving) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Assemble(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Assemble(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Assemble(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Assemble(Vector3.back);
        }
    }

    private void Assemble(Vector3 dir)
    {
        var anchor = transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(Roll(anchor, axis));
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis)
    {
        isMoving = true;
        for(int i = 0; i <(90 / speed); i++)
        {
            transform.RotateAround(anchor, axis, speed);
            yield return new WaitForSeconds(0.01f);
        }

        isMoving = false;
    }
}
