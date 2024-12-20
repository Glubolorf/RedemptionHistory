using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class XeniumBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Breastplate");
			base.Tooltip.SetDefault("+50 max life and mana\n7% increased damage reduction under 50% max life\nGetting hit has a chance to cause an electric discharge, damaging nearby enemies\nThe discharge's damage is based on the owner's defence multiplied by 3");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.defense = 35;
		}

		public override void UpdateEquip(Player player)
		{
			player.statLifeMax2 += 50;
			player.statManaMax2 += 50;
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.5f)
			{
				player.endurance += 0.07f;
			}
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.xeniumDischarge = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 18);
			modRecipe.AddIngredient(null, "ArtificalMuscle", 4);
			modRecipe.AddIngredient(null, "Mk3Capacitator", 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 18);
			modRecipe.AddIngredient(null, "ArtificalMuscle", 4);
			modRecipe.AddIngredient(null, "Mk2Capacitator", 2);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 18);
			modRecipe.AddIngredient(null, "ArtificalMuscle", 4);
			modRecipe.AddIngredient(null, "Mk1Capacitator", 4);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
