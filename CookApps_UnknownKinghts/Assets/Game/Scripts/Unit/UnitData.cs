using System.Collections.Generic;

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

        public int             Level         = 1;
        public int             MaxLevel      = 1;
        public string          Name          = "";
         
        public EGradeType      GradeType     = EGradeType    .Unknown;
        public EUnitType       UnitType      = EUnitType     .Unknown;  
        public ESpecType       SpecType      = ESpecType     .Unknown;
        public EAttackPosType  AttackPosType = EAttackPosType.Unknown;

        public AbilityInfo     Ability       = null;
        public List<SkillInfo> SkillGroup    = new List<SkillInfo>();
    }
}