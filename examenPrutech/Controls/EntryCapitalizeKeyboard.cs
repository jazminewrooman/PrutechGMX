using Xamarin.Forms;

namespace GMX
{
    public class EntryCapitalizeKeyboard : RoutingEffect
    {
        public EntryCapitalizeKeyboard() : base(EffectIds.EntryCapitalizeKeyboard)
        {
        }
    }

    public class EffectIds
    {
        public static string EntryCapitalizeKeyboard => typeof(EntryCapitalizeKeyboard).FullName;
    }
}