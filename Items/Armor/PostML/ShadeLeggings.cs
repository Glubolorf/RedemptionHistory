using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class ShadeLeggings : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Greaves");
			base.Tooltip.SetDefault("40% increased movement speed\n10% increased druidic damage\n15% increased druidic critical strike chance\nHold UP to decrease fall speed\nIncreases spirits summoned by 1\n[c/bdffff:Spirit Level +2]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.defense = 26;
			this.spiritWeapon = false;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.4f;
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 15;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritExtras++;
			if (player.controlUp)
			{
				player.slowFall = true;
			}
			redePlayer.spiritLevel += 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Shadesoul", 3);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
