﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;

using XMusic.Interfaces;
using XMusic.Modelos;

namespace XMusic.View
{
    class MusicModelView : BaseView
    {
        private static MusicModelView _instance;
        public static MusicModelView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MusicModelView();
                }
                return _instance;
            }
        }

        public Command SetQueueCommand { get; private set; }
        //public Command InitCommand { get; private set; }
        public Command PlayCommand { get; private set; }
        public Command PauseCommand { get; private set; }
        public Command NextCommand { get; private set; }
        public Command PrevCommand { get; private set; }
        public Command ShuffleCommand { get; private set; }
        public Command ArtworkCommand { get; private set; }

        private MusicModelView()
        {
  
            SetQueueCommand = new Command(async () => {
                DependencyService.Get<IMusicManager>().Init(
                    isPlaying =>
                    {
                        IsPlaying = isPlaying;
                    },
                    getPosition =>
                    {
                        ActualPosition = getPosition;
                    },
                    getQueuePos =>
                    {
                        QueuePos = getQueuePos;
                    },
                    getQueue =>
                    {
                        Queue = getQueue;
                    });
                DependencyService.Get<IMusicManager>().SetQueue(
                    await DependencyService.Get<IPlaylistManager>().GetAllSongs());
            });
            PlayCommand = new Command(() => DependencyService.Get<IMusicManager>().Play());
            PauseCommand = new Command(() => DependencyService.Get<IMusicManager>().Pause());
            NextCommand = new Command(() => DependencyService.Get<IMusicManager>().Next());
            PrevCommand = new Command(() => DependencyService.Get<IMusicManager>().Prev());
            ShuffleCommand = new Command(() => DependencyService.Get<IMusicManager>().Shuffle());
        }

        public string IsPlayingText
        {
            get
            {
                return (_isPlaying ? "Playing..." : "Paused...");
            }
        }

        private bool _isPlaying;

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;
                    OnPropertyChanged(nameof(IsPlaying));
                    OnPropertyChanged(nameof(IsPlayingText));
                }
            }
        }

        private IList<Song> _queue;

        public IList<Song> Queue
        {
            get { return _queue ?? new ObservableCollection<Song>(); }
            set
            {
                if (_queue != value)
                {
                    _queue = value;
                }
                OnPropertyChanged(nameof(Queue));
                OnPropertyChanged(nameof(Progress));
            }
        }

        private int _queuePos;

        public int QueuePos
        {
            get { return _queuePos; }
            set
            {
                _queuePos = value;
                OnPropertyChanged(nameof(QueuePos));
                OnPropertyChanged(nameof(Progress));
                OnPropertyChanged(nameof(SelectedSong));
            }
        }

        private Song _selectedSong;

        public Song SelectedSong
        {
            get
            {
                if (Queue.Count > _queuePos)
                {
                    _selectedSong = Queue[_queuePos];
                }
                return _selectedSong;
            }
            set
            {
                if (Queue.Contains(value))
                {
                    QueuePos = Queue.IndexOf(value);
                    DependencyService.Get<IMusicManager>().Start(_queuePos);
                }
            }
        }



        private double _actualPosition;

        public double ActualPosition
        {
            get { return _actualPosition; }
            set
            {
                _actualPosition = value;
                Position = _actualPosition;
            }
        }


        private double _position;

        public double Position
        {
            get { return _position; }
            set
            {
                if (_position != value && _position < _queue[_queuePos].Duration)
                {
                    double temp = _position;
                    _position = value;
                    OnPropertyChanged(nameof(Position));
                    OnPropertyChanged(nameof(Progress));
                    if (Math.Abs(_position - _actualPosition) > 1)
                    {
                        DependencyService.Get<IMusicManager>().Seek(value);
                    }
                }
            }
        }

        public double Progress
        {
            get
            {
                if (_queue == null || _queue.Count == 0)
                    return 0;
                double ret = _position / _queue[_queuePos].Duration;
                return ret;
            }
        }

    }
}