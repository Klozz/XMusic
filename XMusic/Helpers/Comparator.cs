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

using XMusic.Modelos;

namespace XMusic.Helpers
{
    class Comparator : IEqualityComparer<Song>
    {
        public bool Equals(Song x, Song y)
        {
            return x.Id == y.Id && (bool)x.Uri?.Equals(y.Uri);
        }

        public int GetHashCode(Song obj)
        {
            return (int)obj.Id;
        }
    }
}