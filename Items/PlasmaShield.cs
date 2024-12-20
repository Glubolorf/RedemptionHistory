using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class PlasmaShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holoshield");
			base.Tooltip.SetDefault("6% damage reduction\n25% chance to reflect projectiles, but they still deal damage to you");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 7;
			base.item.accessory = true;
			base.item.defense = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			modPlayer.plasmaShield = true;
			player.endurance += 0.06f;
		}
	}
}
