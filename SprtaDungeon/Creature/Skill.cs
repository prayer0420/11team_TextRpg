namespace SprtaDungeon
{
    public class Skill
    {
        public string _SkillName { get; private set; }
        public string _SkillExplanation { get; private set; }
        public int _SkillMp { get; private set; }
        public float _SkillAtk { get; private set; }

        public Skill(string name, string explanation, int mp, float atk, int target)
        {
            _SkillName = name;
            _SkillExplanation = explanation;
            _SkillMp = mp;
            _SkillAtk = atk;
        }
    }
}

