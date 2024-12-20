using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class Soul2Body : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wandering Soul's Chestplate");
			base.Tooltip.SetDefault("8% increased druidic damage\n4% increased druidic critical strike chance\n3% damage reduction\nSpirits shoot faster\n[c/bdffff:Spirit Level +1]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 32;
			base.item.height = 26;
			base.item.value = 750;
			base.item.rare = 4;
			base.item.defense = 9;
			this.spiritWeapon = false;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.08f;
			druidDamagePlayer.druidCrit += 4;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSpirits = true;
			player.endurance += 0.03f;
			redePlayer.spiritLevel++;
		}

		public override bool DrawBody()
		{
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LostSoul", 6);
			modRecipe.AddIngredient(501, 20);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
