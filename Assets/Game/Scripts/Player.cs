using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private List<GameObject> cube;
    [SerializeField] private bool index = false;
    [SerializeField] private bool isVertical;
    bool isMoving;

    // Update is called once per frame
    void Update()
    {
        if(isMoving) return;

        if (Mathf.Abs(cube[0].transform.position.x - cube[1].transform.position.x) < 0.01f && Mathf.Abs(cube[0].transform.position.z - cube[1].transform.position.z) < 0.01f)
        {
            isVertical = true;
        }
        else
        {
            isVertical = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if(isVertical)
            {
                Assemble(Vector3.left, Convert.ToInt32(DirectionVertical()));
            }
            else
            {
                Assemble(Vector3.left, Convert.ToInt32(!DirectionHorizontal()));
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isVertical)
            {
                Assemble(Vector3.right, Convert.ToInt32(DirectionVertical()));
            }
            else
            {
                Assemble(Vector3.right, Convert.ToInt32(DirectionHorizontal()));
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isVertical)
            {
                Assemble(Vector3.forward, Convert.ToInt32(DirectionVertical()));
            }
            else
            {
                Assemble(Vector3.forward, Convert.ToInt32(DirectionHorizontal()));
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isVertical)
            {
                Assemble(Vector3.back, Convert.ToInt32(DirectionVertical()));
            }
            else
            {
                Assemble(Vector3.back, Convert.ToInt32(!DirectionHorizontal()));
            }
        }
    }

    private void Assemble(Vector3 dir, int id)
    {
        var anchor = cube[id].transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(Roll(anchor, axis));
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis)
    {
        isMoving = true;
        for(int i = 0; i < (90 / speed); i++)
        {
            transform.RotateAround(anchor, axis, speed);
            yield return new WaitForSeconds(0.01f);
        }

        isMoving = false;
    }

    //Xu ly truong hop dung
    private bool DirectionVertical()
    {
        if (cube[0].transform.position.y < cube[1].transform.position.y)
        {
            index = false;
        }
        else
        {
            index = true;
        }
        return index;
    }

    //Xu ly truong hop nam, tra ve khoi nam ben phai hoac ben tren
    private bool DirectionHorizontal()
    {
        if(cube[0].transform.position.x - cube[1].transform.position.x > 0.5f || cube[0].transform.position.z - cube[1].transform.position.z > 0.5f)
        {
            index = false;
        }
        else
        {
            index = true;
        }
        return index;
    }
}
