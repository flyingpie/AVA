namespace AVA.Core.Providers
{
    public interface INowPlayingProvider
    {
        bool TryGetNowPlaying(out string result);
    }
}