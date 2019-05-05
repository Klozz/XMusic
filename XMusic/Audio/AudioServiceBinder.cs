using System;
using Android.OS;

namespace XMusic.Audio
{
    public class AudioServiceBinder : Binder
    {
        private AudioService _audioService;

        public AudioServiceBinder(AudioService audioService)
        {
            _audioService = audioService;
        }

        public AudioService GetAudioService()
        {
            return _audioService;
        }
    }
}