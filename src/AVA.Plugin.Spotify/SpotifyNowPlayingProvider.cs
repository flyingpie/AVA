using AVA.Core.Providers;
using MUI.DI;
using System;
using System.Diagnostics;
using System.Linq;

namespace AVA.Plugin.Spotify
{
    [Service]
    public class SpotifyNowPlayingProvider : INowPlayingProvider
    {
        private static readonly string SpotifyWindowTitle = "Spotify";

        private TimeSpan _pollInterval = TimeSpan.FromSeconds(2);
        private DateTime _nextPoll = DateTime.MinValue;
        private string _lastNowPlaying;

        public bool TryGetNowPlaying(out string nowPlaying)
        {
            nowPlaying = GetNowPlaying();
            return nowPlaying != null;
        }

        private string GetNowPlaying()
        {
            var now = DateTime.Now;

            if (_nextPoll < now)
            {
                _nextPoll = now.Add(_pollInterval);

                var proc = Process.GetProcessesByName(SpotifyWindowTitle).FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

                _lastNowPlaying = proc?.MainWindowTitle;
            }

            return _lastNowPlaying;
        }
    }
}