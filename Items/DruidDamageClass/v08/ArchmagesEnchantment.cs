using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class ArchmagesEnchantment : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Archmage's Enchantment");
			base.Tooltip.SetDefault("'Magic chickens!'\n10% increased druidic damage\nGetting attacked unleashes Ethereal Chicken to attack foes\nIncreases movement speed after being struck");
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.width = 54;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.1f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).spiritChicken2 = true;
			player.panic = true;
		}
	}
}
