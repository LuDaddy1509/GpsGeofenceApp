using System;
    public interface IAudioService
    {
        Task PlayAsync(string audioPath);
        void Stop();
    }

