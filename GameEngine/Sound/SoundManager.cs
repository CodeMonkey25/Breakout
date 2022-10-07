using System;
using System.Collections.Generic;
using System.Linq;
using GameMain.GameEngine.Events;
using Microsoft.Xna.Framework.Audio;

namespace GameMain.GameEngine.Sound;

public class SoundManager
{
    private int _soundtrackIndex = -1;
    private IList<SoundEffectInstance> _soundtracks = new List<SoundEffectInstance>();
    private IDictionary<Type, SoundBankItem> _soundBank = new Dictionary<Type, SoundBankItem>();
    
    public void OnNotify(BaseGameStateEvent gameEvent) 
    {
        if (_soundBank.TryGetValue(gameEvent.GetType(), out SoundBankItem sound))
        {
            sound.Play();
        }
    }
    
    public void PlaySoundtrack()
    {
        if (!_soundtracks.Any()) return;

        SoundEffectInstance currentTrack = _soundtracks[_soundtrackIndex];
        if (currentTrack.State != SoundState.Stopped) return;

        _soundtrackIndex++;
        if (_soundtrackIndex >= _soundtracks.Count) _soundtrackIndex = 0;
            
        currentTrack = _soundtracks[_soundtrackIndex];
        currentTrack.Play();
    }
    
    public void SetSoundtrack(IList<SoundEffectInstance> tracks)
    {
        _soundtracks = tracks;
        _soundtrackIndex = _soundtracks.Count - 1;
    }
    
    internal void RegisterSound(BaseGameStateEvent gameEvent, SoundEffect sound, float volume=1.0f, float pitch=0.0f, float pan=0.0f)
    {
        _soundBank.Add(gameEvent.GetType(), new SoundBankItem(sound, new SoundAttributes(volume, pitch, pan)));
    }
}