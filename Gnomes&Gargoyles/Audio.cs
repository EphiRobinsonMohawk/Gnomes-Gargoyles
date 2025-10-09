using System;
using System.Collections.Generic;
using System.IO;
using System.Media;

namespace Gnomes_Gargoyles
{
    static class Audio
    {
        private static readonly Dictionary<string, SoundPlayer> players = new();

        public static void Load(string key, string relPath)
        {
            var full = Path.Combine(AppContext.BaseDirectory, relPath);
            if (!File.Exists(full)) throw new FileNotFoundException(full);

            var sp = new SoundPlayer(full);
            sp.Load(); // will throw if format unsupported
            players[key] = sp;
        }

        public static void Play(string key)
        {
            if (players.TryGetValue(key, out var sp)) sp.Play();
        }
    }
}

