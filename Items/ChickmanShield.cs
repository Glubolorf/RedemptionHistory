using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class ChickmanShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chickman Shield");
			base.Tooltip.SetDefault("When below 25% health, you will ignore knockback");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 18;
			base.item.value = Item.buyPrice(0, 0, 20, 0);
			base.item.rare = 1;
			base.item.accessory = true;
			base.item.defense = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.25f)
			{
				player.noKnockback = true;
			}
		}
	}
}
