namespace SprtaDungeon
{
    public class Skill
    {
        public string _SkillName { get; set; }
        public string _SkillExplanation { get; set; }
        public int _SkillMp { get; set; }
        public float _SkillAtk { get; set; }

        public Skill(string name, string explanation, int mp, float atk, int target)
        {
            _SkillName = name;
            _SkillExplanation = explanation;
            _SkillMp = mp;
            _SkillAtk = atk;
        }
    }
}

