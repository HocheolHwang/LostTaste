using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackSkill : Skill
{
    [SerializeField]
    private int Damage = 50;

    [field: SerializeField]
    private Vector3 Scale = new Vector3(3.0f, 2.0f, 3.0f);

    protected override void Init()
    {
        SkillCoolDownTime = 0;
        RequiredMp = 0;
        

    }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;

        //Debug.Log("Normal Attack");
        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);
        yield return new WaitForSeconds(0.3f);
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.NormalAttackEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq);



        skillObj.localScale = Scale;
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (Scale.z / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
        


    }
}
