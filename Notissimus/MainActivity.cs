using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Collections.Generic;

namespace Notissimus
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : AppCompatActivity
    {
        ListView listView1;
        List<string> list;
        private HttpController _httpController;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _httpController = new HttpController("https://yastatic.net/market-export/_/partner/help/YML.xml");

            SetupListView();

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async void SetupListView()
        {
            list = await _httpController.GetOfferIds();

            listView1 = FindViewById<ListView>(Resource.Id.listView1);
            listView1.ItemClick += ItemClick;

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, list);
            listView1.Adapter = adapter;
        }

        private async void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string jsonOnfo = await _httpController.GetOfferJsonById(listView1.GetItemAtPosition(e.Position).ToString());
            var intent = new Android.Content.Intent(this, typeof(JsonInfoActivity));
            intent.PutExtra("Offer info", jsonOnfo);
            StartActivity(intent);
        }
    }
}