using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Vector2 moveSpeed = new Vector2(0, 0);
    // Update is called once per frame
    bool checkLand()
    {
        Ray ray2D = new Ray(transform.position, new Vector2(0, -1));
        RaycastHit hit;

        if (Physics.Raycast(ray2D, out hit))
        {
            if (hit.distance <= 1.5)
            {
                return true;
            }
        }
        return false;
    }
    void doG()
    {
        if (checkLand())
        {
            moveSpeed.y = 0.0f;
        }
        else
        {
            moveSpeed.y -= 1.0f;
        }
    }
    void Update()
    {
        doG();
    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(moveSpeed.x * 0.0025f, moveSpeed.y * 0.0025f,0.0f);
    }
}
