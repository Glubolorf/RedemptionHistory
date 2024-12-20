using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Waters
{
	public class LabWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == base.mod.GetSurfaceBgStyleSlot("LabSurfaceBgStyle");
		}

		public override int ChooseWaterfallStyle()
		{
			return base.mod.GetWaterfallStyleSlot("LabWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return ModContent.DustType<LabWaterSplash>();
		}

		public override int GetDropletGore()
		{
			return base.mod.GetGoreSlot("Gores/Misc/LabDroplet");
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
