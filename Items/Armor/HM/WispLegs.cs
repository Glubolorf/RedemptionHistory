using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class WispLegs : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wisp's Leggings");
			base.Tooltip.SetDefault("8% increased druidic damage\n3% damage reduction\nHold UP to decrease fall speed\nIncreased movement speed\n[c/bdffff:Spirit Level +1]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 14;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 5, 50, 0);
			base.item.rare = 8;
			base.item.defense = 13;
			this.spiritWeapon = false;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.08f;
			player.endurance += 0.03f;
			if (player.controlUp)
			{
				player.slowFall = true;
			}
			player.moveSpeed += 0.5f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).spiritLevel++;
		}

		public override bool DrawLegs()
		{
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SoulOfBloom", 15);
			modRecipe.AddIngredient(1508, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
