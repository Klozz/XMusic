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
    public class Song
    {
        public ulong Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public string Genre { get; set; }

        public object Artwork { get; set; }

        public double Duration { get; set; }

        public string Uri { get; set; }

    }
}