using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Backgrounds
{
	public class SoullessUgBgStyle : ModUgBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneSoulless;
		}

		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = base.mod.GetBackgroundSlot("Backgrounds/ShadowCaveUG0");
			textureSlots[1] = base.mod.GetBackgroundSlot("Backgrounds/ShadowCaveUG1");
			textureSlots[2] = base.mod.GetBackgroundSlot("Backgrounds/ShadowCaveUG2");
			textureSlots[3] = base.mod.GetBackgroundSlot("Backgrounds/ShadowCaveUG3");
		}
	}
}
