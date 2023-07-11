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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1));
        if ( hit.collider != null && hit.collider.name != "")
        {
            if (hit.distance <= 0.1f)
            {
                this.transform.position +=new Vector3(0.0f, 0.1f - hit.distance,0.0f);
                return true;
            }
        }
        return false;
    }

    int checkWall()//0：沒牆、1：樓梯斜坡等、2：地下道、3：牆壁、4：沒有牆，前方也沒有地板(第4個是給NPC的)
    {
        for (int i =  0; i <= 4; i++)
        {
            for (float j = 0.0f; j <= 0.2f; j += 0.02f)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, moveSpeed);
                if (hit.distance == 0.0f)
                {
                    return i;
                }
            }
        }
        return 0;
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
        transform.position += new Vector3(moveSpeed.x*Time.deltaTime, moveSpeed.y * Time.deltaTime, 0.0f);
    }
    private void FixedUpdate()
    {
        doMove();
    }
}
