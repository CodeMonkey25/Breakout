using Microsoft.Xna.Framework.Audio;

namespace GameMain.GameEngine.Sound;

public class SoundBankItem
{
    public SoundEffect Sound { get; }
    public SoundAttributes Attributes { get; }

    public SoundBankItem(SoundEffect sound, SoundAttributes attributes)
    {
        Sound = sound;
        Attributes = attributes;
    }

    public void Play()
    {
        Sound.Play(Attributes.Volume, Attributes.Pitch, Attributes.Pan);
    }
}