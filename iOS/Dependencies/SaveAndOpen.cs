﻿using System;
using System.Threading.Tasks;
using System.IO;
using QuickLook;
using UIKit;
using Foundation;
using GMX.Dependencies.iOS;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SaveAndOpen))]
namespace GMX.Dependencies.iOS
{
    public class SaveAndOpen : ISaveAndOpen
    {
		public async Task OpenFile(string filename, byte[] bytes)
		{
			var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			var filePath = Path.Combine(documentsPath, filename);
			File.WriteAllBytes(filePath, bytes);
			OpenPDF(filePath);
		}

        public void OpenPDF(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            Device.BeginInvokeOnMainThread(() =>
            {
                QLPreviewController previewController = new QLPreviewController();
                previewController.DataSource = new PDFPreviewControllerDataSource(fi.FullName, fi.Name);

                UINavigationController controller = FindNavigationController();
                if (controller != null)
                    controller.PresentViewController(previewController, true, null);
            });
        }

		private UINavigationController FindNavigationController()
		{
			foreach (var window in UIApplication.SharedApplication.Windows)
			{
				if (window.RootViewController.NavigationController != null)
					return window.RootViewController.NavigationController;
				else
				{
					UINavigationController val = CheckSubs(window.RootViewController.ChildViewControllers);
					if (val != null)
						return val;
				}
			}

			return null;
		}

		private UINavigationController CheckSubs(UIViewController[] controllers)
		{
			foreach (var controller in controllers)
			{
				if (controller.NavigationController != null)
					return controller.NavigationController;
				else
				{
					UINavigationController val = CheckSubs(controller.ChildViewControllers);
					if (val != null)
						return val;
				}
			}
			return null;
		}

		public class PDFItem : QLPreviewItem
		{
			string title;
			string uri;

			public PDFItem(string title, string uri)
			{
				this.title = title;
				this.uri = uri;
			}

			public override string ItemTitle
			{
				get { return title; }
			}

			public override NSUrl ItemUrl
			{
				get { return NSUrl.FromFilename(uri); }
			}
		}

		public class PDFPreviewControllerDataSource : QLPreviewControllerDataSource
		{
			string url = "";
			string filename = "";

			public PDFPreviewControllerDataSource(string url, string filename)
			{
				this.url = url;
				this.filename = filename;
			}

			public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
			{
				return new PDFItem(filename, url);
			}

			public override nint PreviewItemCount(QLPreviewController controller)
			{
				return 1;
			}


		}
    }
}
