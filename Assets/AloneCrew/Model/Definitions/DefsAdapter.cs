using UnityEditorInternal.Profiling.Memory.Experimental;

namespace AloneCrew.Model.Definitions
{
    public class DefsAdapter
    {
        public static bool TryGetPotionItem(string id, out PotionDef potion)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (!itemDef.HasTag(ItemTag.Usable))
            {
                potion = default;
                return false;
            }

            var item = DefsFacade.I.PotionItems.Get(id);
            if (item.Id != null)
            {
                potion = item;
                return true;
            }
            
            potion = default;
            return false;
        }
        
        public static bool TryGetThrowableItem(string id, out ThrowableDef throwable)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (!itemDef.HasTag(ItemTag.Throwable))
            {
                throwable = default;
                return false;
            }

            var item = DefsFacade.I.ThrowableItems.Get(id);
            if (item.Id != null)
            {
                throwable = item;
                return true;
            }
            
            throwable = default;
            return false;
        }
    }
}