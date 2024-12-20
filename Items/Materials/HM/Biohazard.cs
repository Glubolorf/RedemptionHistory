using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class Biohazard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Biohazard Particle");
			base.Tooltip.SetDefault("'The essence of the Wasteland'");
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 20, 0);
			base.item.rare = 7;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.Green.ToVector3() * 0.55f * Main.essScale);
		}
	}
}
