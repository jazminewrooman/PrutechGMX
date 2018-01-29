using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;
using GMX;

[assembly: ExportEffect(typeof(EntryCapitalizeKeyboard), nameof(GMX.EntryCapitalizeKeyboard))]
namespace GMX.iOS
{
    [Preserve(AllMembers = true)]
    public class EntryCapitalizeKeyboard : PlatformEffect
    {
        private UITextAutocapitalizationType _old;

        protected override void OnAttached()
        {
            var editText = Control as UITextField;
            if (editText == null)
                return;

            _old = editText.AutocapitalizationType;
            editText.AutocapitalizationType = UITextAutocapitalizationType.AllCharacters;
        }

        protected override void OnDetached()
        {
            var editText = Control as UITextField;
            if (editText == null)
                return;

            editText.AutocapitalizationType = _old;
        }
    }
}