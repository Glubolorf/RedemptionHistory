using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LostSoul : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lost Soul");
			base.Tooltip.SetDefault("'The soul of a creature'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[base.item.type] = true;
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			Item item = new Item();
			item.SetDefaults(549, false);
			base.item.width = item.width;
			base.item.height = item.height;
			base.item.maxStack = 999;
			base.item.value = 50;
			base.item.rare = 0;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
		}
	}
}
