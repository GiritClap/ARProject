using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Material[] mat;

    MeshRenderer mesh;
    float move_timer = 0;
    float rot_timer = 0;
    float hitMode_timer = 0;
    float randMoveSpeedY = 0;
    float randMoveSpeedX = 0;
    float randRotSpeed = 0;
    float randHitMode = 5;
    float plusMinusX = 1;
    float plusMinusY = 1;


    bool hitMode = false;

    public AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hitMode);
        this.gameObject.transform.Translate(plusMinusX * randMoveSpeedX * Time.deltaTime, plusMinusY * randMoveSpeedY * Time.deltaTime, 0, Space.World);
        this.gameObject.transform.Rotate(0, 0, randRotSpeed * Time.deltaTime);

        move_timer += Time.deltaTime * 1f;
        if(move_timer > 4)
        {
            randMoveSpeedY = Random.Range(5, 13);
            randMoveSpeedX = Random.Range(5, 13);
            move_timer = 0;
        }

        // 장애물이 맵 밖으로 안나가게하기
        if(this.gameObject.transform.position.y > 10)
        {
            plusMinusY = -1;
        }
        if(this.gameObject.transform.position.y < -7)
        {
            plusMinusY = 1;
        }

        if(this.gameObject.transform.position.x > 6)
        {
            plusMinusX = -1;
        }
        if (this.gameObject.transform.position.x < -6)
        {
            plusMinusX = 1;
        }


        rot_timer += Time.deltaTime * 1f;
        if(rot_timer > 10)
        {
            randRotSpeed = Random.Range(-30, 30);
            rot_timer = 0;
        }

        hitMode_timer += Time.deltaTime * 1f;
        if(hitMode_timer > randHitMode)
        {
            ChangeMode();
            randHitMode = Random.Range(3, 10);
            hitMode_timer = 0;
        }

        //장애물의 색상 변경(빨간색 = 히트모드, 초록색 = 논히트모드)
        if(hitMode == true)
        {
            mesh.material = mat[0];
        }
        else
        {
            mesh.material = mat[1];
        }
        
    }

    void ChangeMode()
    {
        if (hitMode == true)
        {
            hitMode = false;
        }
        else
        {
            hitMode = true;
        }
    }
    
    public bool IsHitMode()
    {
        return hitMode;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player" && hitMode == true)
        {
            audioSource.Play();
        }
    }
}
