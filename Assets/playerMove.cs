using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
        Ray2D ray2D = new Ray2D(transform.position, new Vector2(0, -1));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1));
        if (hit.collider.name != "")
        {
            if (hit.distance <= 0.8f)
            {
                this.transform.position +=new Vector3(0.0f, 0.8f - hit.distance,0.0f);
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
