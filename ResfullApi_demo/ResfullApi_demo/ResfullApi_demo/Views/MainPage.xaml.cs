using Newtonsoft.Json;
using ResfullApi_demo.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ResfullApi_demo.Views
{
	public partial class MainPage : ContentPage
	{

		private const string url = "https://jsonplaceholder.typicode.com/posts";
		private HttpClient _Client = new HttpClient( );
		private ObservableCollection<Post> _post;

		public MainPage ()
		{
			InitializeComponent ();
		}

		protected override async void OnAppearing()
		{
			var content = await _Client.GetStringAsync( url );
			var post = JsonConvert.DeserializeObject<List<Post>>( content );
			_post = new ObservableCollection<Post>( post );
			Post_List.ItemsSource = _post;
			base.OnAppearing( );
		}
		private async void OnAdd( object sender , System.EventArgs e )
		{
			var post = new Post( ) { title = "Title" + DateTime.Now.Ticks , body = "Body" };
			_post.Insert( 0 , post );
			var content = JsonConvert.SerializeObject( post );
			await _Client.PostAsync( url , new StringContent( content ) );
		}
		private async void OnDelete( object sender , System.EventArgs e )
		{
			var post = _post [0];
			_post.Remove( post );
			await _Client.DeleteAsync( url + "/" + post.id );
		}
	}
}