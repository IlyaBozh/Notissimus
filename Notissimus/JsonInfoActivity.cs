using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notissimus
{
    [Activity(Label = "JsonInfoActivity", ScreenOrientation = ScreenOrientation.Landscape)]
    public class JsonInfoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_json);

            string selectedInfo = Intent.GetStringExtra("Offer info");

            TextView textView = FindViewById<TextView>(Resource.Id.textView1);
            textView.Text = selectedInfo;
        }
    }
}