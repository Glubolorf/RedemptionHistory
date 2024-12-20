using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class SoulOfBloom : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soul of Bloom");
			base.Tooltip.SetDefault("'The essence of nature'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[base.item.type] = true;
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.buyPrice(0, 0, 85, 0);
			base.item.rare = 5;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.HotPink.ToVector3() * 0.55f * Main.essScale);
		}
	}
}
