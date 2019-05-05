using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XMusic.Modelos
{
    public class Playlist
    {
        public ulong Id { get; set; }

        public string Title { get; set; }

        public IList<Song> Songs { get; set; }

        public int Count { get; set; }

    }
}