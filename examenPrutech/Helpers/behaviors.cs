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
            ((Entry)sender).Text = e.NewTextValue.ToUpper();
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= bindable_TextChanged;
			base.OnDetachingFrom(bindable);
		}
	}

    public class MaxLengthValidator : Behavior<Entry>     {         static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(MaxLengthValidator), false);          public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;          public bool IsValid         {             get { return (bool)base.GetValue(IsValidProperty); }             private set { base.SetValue(IsValidPropertyKey, value); }         }          public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthValidator), 0);         public int MaxLength         {             get { return (int)GetValue(MaxLengthProperty); }             set { SetValue(MaxLengthProperty, value); }         }          protected override void OnAttachedTo(Entry bindable)         {             bindable.TextChanged += bindable_TextChanged;             base.OnAttachedTo(bindable);         }

		private void bindable_TextChanged(object sender, TextChangedEventArgs e)
		{

			if (((Entry)sender).Text != null)
			{
				if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > MaxLength)
					((Entry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
				//else
				//	((Entry)sender).Text = e.NewTextValue.ToUpper();
			}
		}          protected override void OnDetachingFrom(Entry bindable)         {             bindable.TextChanged -= bindable_TextChanged;             base.OnDetachingFrom(bindable);         }     } 
    public class EmailValidationBehavior : Behavior<Entry>     {         static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidationBehavior), false);          public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;          public bool IsValid         {             get { return (bool)base.GetValue(IsValidProperty); }             private set { base.SetValue(IsValidPropertyKey, value); }         }         protected override void OnAttachedTo(Entry entry)         {             entry.TextChanged += OnEntryTextChanged;             base.OnAttachedTo(entry);         }         protected override void OnDetachingFrom(Entry entry)         {             entry.TextChanged -= OnEntryTextChanged;             base.OnDetachingFrom(entry);         }         void OnEntryTextChanged(object sender, TextChangedEventArgs args)         {             const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";             IsValid = (Regex.IsMatch(args.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));             //((Entry)sender).BackgroundColor = isValid ? Color.Default : Color.Red;         }     }

	public class RFCValidationBehavior : Behavior<Entry>
	{
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidationBehavior), false);

		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public bool IsValid
		{
			get { return (bool)base.GetValue(IsValidProperty); }
			private set { base.SetValue(IsValidPropertyKey, value); }
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
		}
	}
}
