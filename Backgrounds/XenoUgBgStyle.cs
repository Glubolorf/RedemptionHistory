using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Backgrounds
{
	public class XenoUgBgStyle : ModUgBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneXeno || Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneEvilXeno || Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneEvilXeno2;
		}

		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = base.mod.GetBackgroundSlot("Backgrounds/XenoBiomeUG0");
			textureSlots[1] = base.mod.GetBackgroundSlot("Backgrounds/XenoBiomeUG1");
			textureSlots[2] = base.mod.GetBackgroundSlot("Backgrounds/XenoBiomeUG2");
			textureSlots[3] = base.mod.GetBackgroundSlot("Backgrounds/XenoBiomeUG3");
		}
	}
}
