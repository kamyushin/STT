using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharaController
{
    [SerializeField]
    private float walkSpeed = 0.5f;
    public float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }

    [SerializeField]
    private float changeForwardTime = 3.0f;
    public float ChangeForwardTime
    {
        get { return changeForwardTime; }
        set { changeForwardTime = value; }
    }
    

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (HP == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public override void wait() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Routine = CharaRoutine.CHARA_ROUTINE_MOVE;
            StartCoroutine("RandomWalk");
        }
        
    }
    public override void move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Routine = CharaRoutine.CHARA_ROUTINE_WAIT;
            StopCoroutine("RandomWalk");
        }
        transform.position += transform.forward * WalkSpeed * Time.deltaTime;
    }


    public override void shot()
    {
    }

    IEnumerator RandomWalk()
    {
        while (true)
        {
            transform.forward = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
            yield return new WaitForSeconds(ChangeForwardTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "enemy")
            damageHP(1);
    }
}
