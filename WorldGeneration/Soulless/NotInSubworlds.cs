using System;
using SubworldLibrary;
using Terraria.ModLoader;

namespace Redemption.WorldGeneration.Soulless
{
	public class NotInSubworlds : ModWorld
	{
		public override void PostUpdate()
		{
			if (!SLWorld.subworld)
			{
				SubworldCache.UpdateCache();
			}
		}
	}
}
