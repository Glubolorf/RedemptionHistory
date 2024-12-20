using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Waters
{
	public class SoullessWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == base.mod.GetSurfaceBgStyleSlot("SoullessSurfaceBgStyle");
		}

		public override int ChooseWaterfallStyle()
		{
			return base.mod.GetWaterfallStyleSlot("SoullessWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return base.mod.DustType("SoullessWaterSplash");
		}

		public override int GetDropletGore()
		{
			return base.mod.GetGoreSlot("Gores/SoullessDroplet");
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
