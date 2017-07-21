using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Views;
using Android.Graphics;

using ZXing;
using ZXing.Mobile;

namespace Scannercheck
{
    [Activity(Label = "Scanner check", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ImageButton buttonScan;

        MobileBarcodeScanner scanner;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
            MobileBarcodeScanner.Initialize(Application);
            SetContentView (Resource.Layout.Main);

            scanner = new MobileBarcodeScanner();
            buttonScan = this.FindViewById<ImageButton>(Resource.Id.scanbutton);
            buttonScan.Click += async delegate
            {
                scanner.UseCustomOverlay = false;

                scanner.TopText = "Giữ camera cách mã khoảng 6inch";
                scanner.BottomText = "Đợi một chút...";

                //Start scanning
                var result = await scanner.Scan();

                HandleScanResult(result);
            };

        }

        void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
                msg = "Found Barcode: " + result.Text;
            else
                msg = "Scanning Canceled!";

            this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }
    }
}

