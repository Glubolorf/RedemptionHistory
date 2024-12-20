using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class SoulLeggings : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lost Soul's Leggings");
			base.Tooltip.SetDefault("5% increased druidic damage\n2% damage reduction\nDecreased falling speed");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = 50;
			base.item.rare = 1;
			base.item.defense = 2;
			this.spiritWeapon = false;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.05f;
			player.endurance += 0.02f;
			player.slowFall = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SmallLostSoul", 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
