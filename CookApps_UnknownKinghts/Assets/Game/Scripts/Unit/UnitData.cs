using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit.ForData
{
    [System.Serializable]
    public class UnitData
    {
        [System.Serializable]
        public class AbilityInfo
        {
            public int   Power          = 0;
            public int   Health         = 0;
            public int   Defense        = 0;
            public int   PenetratePower = 0;
            public int   CriticalDamage = 0;
            public int   CriticalRate   = 0;
            public float AttackSpeed    = 0f;
        }

        [System.Serializable]
        public class SkillInfo
        {
            public string Name     = "";
            public float  CoolTime = 0f;
        }

        public Sprite          Profile       = null;

        public int             Level         = 1;
        public int             MaxLevel      = 1;
        public int             Star          = 1;
        public string          Name          = "";

        public EGradeType      GradeType     = EGradeType    .Unknown;
        public EUnitType       UnitType      = EUnitType     .UnknownHero;  
        public EJobType        JobType       = EJobType      .Unknown;
        public ESpecType       SpecType      = ESpecType     .Unknown;
        public EAttackPosType  AttackPosType = EAttackPosType.Unknown;

        public AbilityInfo     Ability       = null;
        public List<SkillInfo> SkillGroup    = new List<SkillInfo>();

        // ----- Test용 [필요시에 따라 테이블로 관리해야할 부분]
        public UnitData(Sprite profile,
                        int star, int level, int maxLevel, string name,
                        EJobType jobType, EGradeType gradeType, EUnitType unitType, ESpecType specType, EAttackPosType attackPosType,
                        int power, int hp, int defense, int penetratePower, int criticalDamage, int criticalRate, float attackSpeed,
                        string skillName, float coolTime) 
        {
            Profile       = profile;

            Star          = star;
            Level         = level;
            MaxLevel      = maxLevel;
            Name          = name;
            JobType       = jobType;
            GradeType     = gradeType;
            UnitType      = unitType;
            SpecType      = specType;
            AttackPosType = attackPosType;

            AbilityInfo ability = new AbilityInfo();
            ability.Power          = power;
            ability.Health         = hp;
            ability.Defense        = defense;
            ability.PenetratePower = penetratePower;
            ability.CriticalDamage = criticalDamage;
            ability.CriticalRate   = criticalRate;
            ability.AttackSpeed    = attackSpeed;  
            
            SkillInfo skillInfo = new SkillInfo();
            skillInfo.Name = skillName;
            skillInfo.CoolTime = coolTime;
            SkillGroup.Add(skillInfo);
        }

        public void AddToSkill(string skillName, float coolTime)
        {
            SkillInfo skillInfo = new SkillInfo();
            skillInfo.Name      = skillName;
            skillInfo.CoolTime  = coolTime; 
        }
    }
}