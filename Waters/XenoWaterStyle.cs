using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Waters
{
	public class XenoWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == base.mod.GetSurfaceBgStyleSlot("XenoSurfaceBgStyle");
		}

		public override int ChooseWaterfallStyle()
		{
			return base.mod.GetWaterfallStyleSlot("XenoWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return ModContent.DustType<XenoWaterSplash>();
		}

		public override int GetDropletGore()
		{
			return base.mod.GetGoreSlot("Gores/XenoDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 0f;
			g = 1f;
			b = 0f;
		}

		public override Color BiomeHairColor()
		{
			return Color.ForestGreen;
		}
	}
}
