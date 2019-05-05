using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XMusic.Modelos;

namespace XMusic.Interfaces
{
    public interface IPlaylistManager
    {
        void AddToPlaylist(Playlist playlist, Song song);

        Playlist CreatePlaylist(string name);

        IList<Playlist> GetPlaylists();

        Task<IList<Song>> GetPlaylistSongs(ulong playlistId);

        Task<IList<Song>> GetAllSongs();
    }
}