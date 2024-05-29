using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    EnemyMove enemyMove;
    public MiniGameManager manager;
    public VariableJoystick joy;


    public bool isTouchTop;
    public bool isTouchLeft;
    public bool isTouchBottom;
    public bool isTouchRight;


    public GameObject exploseParticle;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float h = joy.Horizontal;
        if ((isTouchRight && h > 0f) || (isTouchLeft && h < -0f))
        {
            h = 0;
        }
        float v = joy.Vertical;
        if ((isTouchTop && v > 0f) || (isTouchBottom && v < -0f))
        {
            v = 0;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyMove = other.GetComponent<EnemyMove>();
            if (enemyMove.IsHitMode() == true)
            {
                GameObject explose = Instantiate(exploseParticle, transform.position, transform.rotation);

                Destroy(this.gameObject);
                manager.GameOver();
            }
        }

        if (other.gameObject.tag == "Wall")
        {
            switch (other.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            switch (other.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }

}
