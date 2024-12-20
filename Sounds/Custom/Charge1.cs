﻿using System;
using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace Redemption.Sounds.Custom
{
	public class Charge1 : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = base.sound.CreateInstance();
			soundInstance.Volume = volume * 1f;
			soundInstance.Pan = pan;
			soundInstance.Pitch = 0f;
			return soundInstance;
		}
	}
}