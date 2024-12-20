using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class ShadeBody : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadeplate");
			base.Tooltip.SetDefault("+50 max life and mana\n10% increased druidic damage\nSpirits shoot faster\n8% increased damage reduction\n[c/bdffff:Spirit Level +2]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 38;
			base.item.height = 32;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 26;
			this.spiritWeapon = false;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.statLifeMax2 += 50;
			player.statManaMax2 += 50;
			player.endurance += 0.08f;
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.1f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSpirits = true;
			redePlayer.spiritLevel += 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Shadesoul", 4);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
