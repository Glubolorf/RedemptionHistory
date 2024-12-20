using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class EvilJelly : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Evil Jelly");
			base.Tooltip.SetDefault("Lures a dark gelatinous being\nSold by Zephos/Daerel after all Mech bosses are defeated");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item1;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.width = 24;
			base.item.height = 38;
			base.item.maxStack = 1;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 6;
			base.item.buffType = ModContent.BuffType<EvilJellyBuff>();
			base.item.buffTime = 7200;
		}

		public override bool UseItem(Player player)
		{
			RedeWorld.darkSlimeLure = true;
			return true;
		}
	}
}
