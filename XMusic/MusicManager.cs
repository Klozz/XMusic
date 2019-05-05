﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using XMusic.Audio;
using XMusic.Modelos;
using XMusic.Interfaces;
using System.Threading.Tasks;

namespace XMusic
{
    class MusicManager : IMusicManager
    {
        private AudioService _audioService;
        private bool _isConnected = false;

        public void Init(Action<bool> IsPlaying, Action<double> GetSongPos, Action<int> GetQueuePos, Action<IList<Song>> GetQueue)
        {
            if (MainActivity.Binder != null)
            {
                _audioService = MainActivity.Binder.GetAudioService();
                _audioService.Init(IsPlaying, GetSongPos, GetQueuePos, GetQueue);
                _isConnected = true;
            }
        }

        public async void Next()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Next();
                }
            });
        }

        public async void Pause()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Pause();
                }
            });
        }

        public async void Play()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Play();
                }
            });
        }

        public async void Prev()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Prev();
                }
            });
        }

        public async void Seek(double position)
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Seek(position);
                }
            });
        }

        public async void SetQueue(IList<Song> songs)
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.SetQueue(songs);
                    _audioService?.Prepare(0);
                }
            });
        }

        public async void StartQueue(IList<Song> songs, int pos)
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.SetQueue(songs);
                    _audioService?.Start(pos);
                }
            });
        }

        public async void Shuffle()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Shuffle();
                }
            });
        }

        public async void Start(int pos)
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Start(pos);
                }
            });
        }
    }
}