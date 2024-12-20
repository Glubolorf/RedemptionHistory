using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class MaskFish : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Jagbone Fish");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 38;
			base.item.value = Item.sellPrice(0, 1, 5, 0);
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(ModContent.ItemType<VesselFrag>(), Main.rand.Next(3, 8));
		}
	}
}
