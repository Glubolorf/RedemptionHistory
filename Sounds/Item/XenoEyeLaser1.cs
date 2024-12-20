using System;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Sounds.Item
{
	public class XenoEyeLaser1 : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = base.sound.CreateInstance();
			soundInstance.Volume = volume * 1f;
			soundInstance.Pan = pan;
			soundInstance.Pitch = (float)Main.rand.Next(-5, 6) * 0.02f;
			return soundInstance;
		}
	}
}
