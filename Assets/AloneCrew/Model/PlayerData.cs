using System;

namespace AloneCrew.Model
{
    [Serializable]
    public class PlayerData
    {
        public int Coins;
        public int Hp;
        public bool IsArmed;
        public int SwordCount;
        
        public PlayerData Clone()
        {
            return new PlayerData
            {
                Coins = Coins,
                Hp = Hp,
                IsArmed = IsArmed,
                SwordCount = SwordCount
            };
        }
    }
}