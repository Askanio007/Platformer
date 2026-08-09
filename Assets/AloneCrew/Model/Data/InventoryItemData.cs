using System;

namespace AloneCrew.Model.Data
{
    [Serializable]
    public class InventoryItemData
    {
        public string Id;
        public int Value;

        public InventoryItemData(string id, int value)
        {
            this.Id = id;
            this.Value = value;
        }
    }
}