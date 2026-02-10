#if ANDROID
using Android.Media;

public class AudioService : IAudioService
{
    private MediaPlayer? _player;

    public async Task PlayAsync(string audioPath)
    {
        Stop();

        var assets = Android.App.Application.Context?.Assets;
        if (assets == null)
            throw new InvalidOperationException("Assets manager is not available.");

        var fd = assets.OpenFd(audioPath);
        _player = new MediaPlayer();
        _player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
        _player.Prepare();
        _player.Start();
    }

    public void Stop()
    {
        _player?.Stop();
        _player?.Release();
        _player = null;
    }
}
#endif
