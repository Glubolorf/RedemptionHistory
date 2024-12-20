using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class Soul2Leggings : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wandering Soul's Leggings");
			base.Tooltip.SetDefault("2% increased druidic damage\n3% damage reduction\nHold UP to decrease fall speed\nIncreased movement speed\n[c/bdffff:Spirit Level +1]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = 500;
			base.item.rare = 4;
			base.item.defense = 7;
			this.spiritWeapon = false;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.02f;
			player.endurance += 0.03f;
			if (player.controlUp)
			{
				player.slowFall = true;
			}
			player.moveSpeed += 0.5f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).spiritLevel++;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LostSoul", 5);
			modRecipe.AddIngredient(501, 15);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
