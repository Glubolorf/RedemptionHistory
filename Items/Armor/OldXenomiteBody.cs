using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class OldXenomiteBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Old Xenomite Breastplate");
			base.Tooltip.SetDefault("'Comes from a simpler time...'\n+50 max mana\n10% increased magic damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 7;
			base.item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += 50;
			player.magicDamage *= 1.1f;
		}
	}
}
