using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrillDuckAttackSkill : Skill
{
    private DrillDuckController _controller;

    [field: SerializeField]
    private int _damage;

    [field: SerializeField]
    private float _range;

    protected override void Init()
    {
        SkillCoolDownTime = 0;
        _controller = GetComponent<DrillDuckController>();

        _damage = Root.GetComponent<MonsterController>().Stat.AttackDamage;
        _range = Root.GetComponent<MonsterController>().Stat.AttackRange;
    }

    protected override IEnumerator SkillCoroutine()
    {
        Root.GetComponent<Animator>().CrossFade("Attack", 0.3f, -1, 0);

        yield return new WaitForSeconds(0.2f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.DrillDuckAttackEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.transform.parent = _controller.transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _damage, _seq);

        skillObj.localScale = new Vector3(1.0f, 3.0f, _range / 2);    // 5.0f
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_range / 3));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 1.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        ps.transform.parent = skillObj.transform;
        ps.transform.position = skillObj.transform.position + skillObj.transform.right * 3.0f;
        ps.transform.position = skillObj.transform.position + skillObj.transform.forward * 1.0f;

        yield return new WaitForSeconds(0.8f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
