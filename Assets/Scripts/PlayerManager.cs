using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    static public PlayerManager Instance;

    private Rigidbody2D rb;

    public LayerMask layerMask;

    public string currentMapName;
    public float speed;
    public float jumpPower;
    private float plusJumpPower = 1f;
    protected bool jumpChargeFlag = false;
    protected bool jumpFlag = false;
    protected string directionFlag = "";
    private bool canMove = true;

    private Animator anim;

    void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //������ ����Ű�� ������ �ִ� ���¸� ������ �밢�� ����
    //���� ����Ű�� ������ �ִ� ���¸� ���� �밢�� ����
    void Jump()
    {
        anim.SetBool("Charging", false);
        anim.SetBool("Jumping", true);
        jumpChargeFlag = false;
        jumpFlag = true;
        rb.AddForce(Vector2.up * jumpPower * plusJumpPower, ForceMode2D.Impulse);
        if (directionFlag == "RIGHT")
            rb.AddForce(Vector2.right * (jumpPower / 2) * plusJumpPower, ForceMode2D.Impulse);
        if (directionFlag == "LEFT")
            rb.AddForce(Vector2.left * (jumpPower / 2) * plusJumpPower, ForceMode2D.Impulse);
        JumpPowerInit();
    }

    //���� ���ӵ� �� ���� ������ �ʱ�ȭ
    void JumpPowerInit()
    {
        plusJumpPower = 1f;
        jumpPower = 150f;
    }

    void JumpCharging()
    {
        anim.SetBool("Charging", true);
        jumpPower = jumpPower + (100f * Time.deltaTime);
        plusJumpPower = plusJumpPower + (0.5f * Time.deltaTime);
    }

    //���� ���� ��� �ٽ� Jump �� �� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Bound":
                anim.SetBool("Jumping", false);
                jumpFlag = false;
                break;
        }
    }


    void Update()
    {
        anim.SetBool("Walking", false);
        if (rb.velocity.y < 0f)
        {
            JumpPowerInit();
            canMove = false;
        }
        else
            canMove = true;
        if(canMove)
        {
            //����, ������ ����Ű�� ������ ��� �÷��̾� �̵�
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                directionFlag = "LEFT";
                {
                    if (!jumpFlag)
                    {
                        anim.SetFloat("DirX", -1f);
                        if (!jumpChargeFlag) // ������ �غ����� ���� ��� �̵�
                        {
                            anim.SetBool("Walking", true);
                            rb.velocity = new Vector2(-speed, rb.velocity.y); //transform.Translate(-speed * Time.deltaTime, 0, 0);
                        }
                    }
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                directionFlag = "RIGHT";
                {
                    if (!jumpFlag)
                    {
                        anim.SetFloat("DirX", 1f);
                        if (!jumpChargeFlag)
                        {
                            anim.SetBool("Walking", true);
                            rb.velocity = new Vector2(speed, rb.velocity.y);
                            //transform.Translate(speed * Time.deltaTime, 0, 0);
                        }
                    }

                }

            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                directionFlag = "";
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                directionFlag = "";
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpChargeFlag = true;
            }

            if (jumpChargeFlag)
            {
                JumpCharging();
                if (plusJumpPower > 1.5f)
                {
                    plusJumpPower = 1.5f;
                    Jump();
                }
            }
            if (Input.GetKeyUp(KeyCode.Space) && jumpChargeFlag)
            {
                if (plusJumpPower < 1.5f)
                {
                    Jump();
                }
            }
        }
        

    }
}
