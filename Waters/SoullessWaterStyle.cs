using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Waters
{
	public class SoullessWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == base.mod.GetSurfaceBgStyleSlot("SoullessSurfaceBgStyle") && !Main.LocalPlayer.GetModPlayer<RedePlayer>().anglerPot;
		}

		public override int ChooseWaterfallStyle()
		{
			return base.mod.GetWaterfallStyleSlot("SoullessWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return ModContent.DustType<SoullessWaterSplash>();
		}

		public override int GetDropletGore()
		{
			return base.mod.GetGoreSlot("Gores/Misc/SoullessDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 0f;
			g = 0f;
			b = 0f;
		}

		public override Color BiomeHairColor()
		{
			return Color.Black;
		}
	}
}
