namespace GameMain.GameEngine.Sound;

public class SoundAttributes
{
    public float Volume { get; }
    public float Pitch { get; }
    public float Pan { get; }

    public SoundAttributes(float volume, float pitch, float pan)
    {
        Volume = volume;
        Pitch = pitch;
        Pan = pan;
    }
}