using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    public AudioClip RailgunShotSound;
    public AudioClip FlamethrowerSound;
    public AudioClip PistolShotSound;
    public AudioClip ShotgunSound;
}
