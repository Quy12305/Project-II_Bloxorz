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
    [SerializeField] private LayerMask layerMask;
    bool isMoving;

    // Update is called once per frame
    void Update()
    {
        // Lấy vị trí của khối hộp
        Vector3 cubePosition = transform.position;

        // Mảng chứa các hướng của các tia raycast
        Vector3[] directions = {
            Vector3.up,         // Lên trên
            Vector3.down,       // Xuống dưới
            Vector3.forward,    // Về phía trước
            Vector3.back,       // Về phía sau
            Vector3.left,       // Sang trái
            Vector3.right       // Sang phải
        };

        // Vòng lặp để bắn ra các tia raycast từ mỗi mặt của khối hộp
        foreach (Vector3 direction in directions)
        {
            // Bắn ra raycast từ vị trí của khối hộp theo từng hướng
            RaycastHit hit;
            if (Physics.Raycast(cubePosition, direction, out hit, Mathf.Infinity, layerMask))
            {
                // Xử lý khi raycast va chạm với một đối tượng
                Debug.DrawRay(cubePosition + new Vector3(0, 0.5f, 0), direction * hit.distance, Color.yellow);
                Debug.DrawRay(cubePosition - new Vector3(0, 0.5f, 0), direction * hit.distance, Color.yellow);
            }
            else
            {
                // Xử lý khi raycast không va chạm với bất kỳ đối tượng nào
                Debug.DrawRay(cubePosition + new Vector3(0, 0.5f, 0), direction * 10, Color.white);
                Debug.DrawRay(cubePosition - new Vector3(0, 0.5f, 0), direction * 10, Color.white);
            }
        }


        //Xu ly di chuyen
        if (isMoving) return;

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
            if (isVertical)
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

    public void CheckLose()
    {

    }
}
