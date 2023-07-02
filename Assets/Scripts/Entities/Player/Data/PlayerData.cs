public class PlayerData
{
    private const float LvlUpMultiplier = 1.5f;
    private const int BaseExperienceToLvlUp = 100;
    private int _experience;
    private int _skillPoints;

    public int Coins;

    public LvlData MaxHpLvl { get; private set; } = new(10, 1.5f);
    public LvlData MoveSpeedLvl { get; private set; } = new(4, 1.15f);
    public LvlData StrengthLvl { get; private set; } = new(10, 1.5f);
    public LvlData StaminaLvl { get; private set; } = new(10, 1.5f);
    
    public int ExperienceToLvlUp => (int)(BaseExperienceToLvlUp * LvlUpMultiplier * _skillPoints);

    public void TakeExperience(int value)
    {
        if (value < 0) return;
        if (_experience + value >= ExperienceToLvlUp)
        {
            value = _experience + value - ExperienceToLvlUp;
            _experience += ExperienceToLvlUp - _experience;
            AddSkillPoint();
        }

        _experience += value;
    }


    public void PropertyLvlUp(ref LvlData property)
    {
        if (_skillPoints <= 0) return;
        _skillPoints--;
        property.Lvl++;
    }

    private void AddSkillPoint()
    {
        if (_experience < ExperienceToLvlUp) return;
        _skillPoints++;
        _experience = 0;
    }

    public struct LvlData
    {
        public int Lvl;
        private readonly float _multiplier;
        private readonly int _maxLvl;

        public LvlData(int maxLvl, float multiplier)
        {
            Lvl = 0;
            _maxLvl = maxLvl;
            _multiplier = multiplier;
        }

        public float LvlIncrease => Lvl * _multiplier;
        public float MaxLvlIncrease => _maxLvl * _multiplier;
    }
}