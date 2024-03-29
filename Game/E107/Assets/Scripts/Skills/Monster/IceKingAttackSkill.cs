using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKingAttackSkill : Skill
{
    private IceKingController _controller;

    [field: SerializeField]
    private int _damage;
    [field: SerializeField]
    private float _range;

    protected override void Init()
    {
        //SkillCoolDownTime = 3.0f;
        _controller = GetComponent<IceKingController>();
        _damage = Root.GetComponent<MonsterController>().Stat.AttackDamage;
        _range = Root.GetComponent<MonsterController>().Stat.AttackRange;
    }

    protected override IEnumerator SkillCoroutine()
    {
        Root.GetComponent<Animator>().CrossFade("Attack", 0.3f, -1, 0);

        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.IceKingCleaveEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.transform.parent = _controller.transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _damage, _seq);

        skillObj.localScale = new Vector3(1.0f, 3.0f, _range + 3.0f);    // 5.0f
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_range - 1.0f));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        ps.transform.parent = skillObj.transform;
        //ps.transform.position = new Vector3(skillObj.position.x - 5.0f, skillObj.position.y, skillObj.position.z - 0.9f);
        //ps.transform.position = skillObj.transform.position + skillObj.transform.right * 3.0f;
        ps.transform.position = skillObj.transform.position - skillObj.transform.forward * 3.0f;
        //ps.position = skillObj.position / 10.0f;

        yield return new WaitForSeconds(1.0f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
