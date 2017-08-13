using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GMX.Helpers
{
    public class behaviors
    {
        public behaviors()
        {
        }
    }

	public class UpperValidator : Behavior<Entry>
	{
		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += bindable_TextChanged;
			base.OnAttachedTo(bindable);
		}

		private void bindable_TextChanged(object sender, TextChangedEventArgs e)
		{
            ((Entry)sender).TextChanged -= bindable_TextChanged;
            ((Entry)sender).Text = e.NewTextValue.ToUpper();
            ((Entry)sender).TextChanged += bindable_TextChanged;
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= bindable_TextChanged;
			base.OnDetachingFrom(bindable);
		}
	}

    public class MaxLengthValidator : Behavior<Entry>     {         public int MaxLength { get; set; }
        public bool Upper { get; set; }          protected override void OnAttachedTo(Entry bindable)         {
			base.OnAttachedTo(bindable);
			bindable.TextChanged += OnEntryTextChanged;         }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (entry.Text.Length > this.MaxLength)
            {
                string entryText = entry.Text;
                entry.TextChanged -= OnEntryTextChanged;
                if (Upper)
                    entry.Text = e.OldTextValue.ToUpper();
                else
                    entry.Text = e.OldTextValue;
                entry.TextChanged += OnEntryTextChanged;
            }
            else
            {
                //entry.TextChanged -= OnEntryTextChanged;
                if (Upper)
                    entry.Text = e.NewTextValue.ToUpper();
                //entry.TextChanged += OnEntryTextChanged;
            }
        }
         protected override void OnDetachingFrom(Entry bindable)         {
			base.OnDetachingFrom(bindable);
			bindable.TextChanged -= OnEntryTextChanged;         }     } 
    public class EmailValidationBehavior : Behavior<Entry>     {         /*static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidationBehavior), false);          public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;*/          public bool IsValid { get; set; }         /*{             get { return (bool)base.GetValue(IsValidProperty); }             private set { base.SetValue(IsValidPropertyKey, value); }         }*/         protected override void OnAttachedTo(Entry entry)         {             entry.TextChanged += OnEntryTextChanged;             base.OnAttachedTo(entry);         }         protected override void OnDetachingFrom(Entry entry)         {             entry.TextChanged -= OnEntryTextChanged;             base.OnDetachingFrom(entry);         }         void OnEntryTextChanged(object sender, TextChangedEventArgs args)         {             const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";             IsValid = (Regex.IsMatch(args.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));             //((Entry)sender).BackgroundColor = isValid ? Color.Default : Color.Red;         }     }

	public class RFCValidationBehavior : Behavior<Entry>
	{
        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
			bindable.BindingContextChanged += (sender, _) => this.BindingContext = ((BindableObject)sender).BindingContext;
        }

		public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(RFCValidationBehavior), false);

		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public bool IsValid
		{
			get { return (bool)base.GetValue(IsValidProperty); }
			set { base.SetValue(IsValidPropertyKey, value); }
		}
		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}
		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(entry);
		}
		void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			string pattern = @"[A-Z,Ñ,&]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[A-Z|\d]{3}";
			IsValid = (Regex.IsMatch(args.NewTextValue, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
		}
	}
}
