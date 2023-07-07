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
    void doGravity()
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
    void getKeyDown()
    {
        float msX = 0.0f;
        float msY = 0.0f;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) msX -= 1.0f;
        if (Input.GetKeyDown(KeyCode.RightArrow)) msX += 1.0f;
        moveSpeed = new Vector2(msX+moveSpeed.x, msY+moveSpeed.y);
    }
    void Update()
    {
        doGravity();
        getKeyDown();
    }
    void doMove() {
        transform.position += new Vector3(moveSpeed.x * 0.0025f, moveSpeed.y * 0.0025f, 0.0f);
    }
    private void FixedUpdate()
    {
        doMove();
    }
}
