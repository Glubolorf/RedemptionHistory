using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Backgrounds
{
	public class LabUgBgStyle : ModUgBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).ZoneLab;
		}

		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = base.mod.GetBackgroundSlot("Backgrounds/LabBiomeUG0");
			textureSlots[1] = base.mod.GetBackgroundSlot("Backgrounds/LabBiomeUG1");
			textureSlots[2] = base.mod.GetBackgroundSlot("Backgrounds/LabBiomeUG2");
			textureSlots[3] = base.mod.GetBackgroundSlot("Backgrounds/LabBiomeUG3");
		}
	}
}
