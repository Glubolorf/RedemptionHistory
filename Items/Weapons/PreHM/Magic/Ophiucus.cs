using System;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Magic
{
	public class Ophiucus : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ophiucus");
			base.Tooltip.SetDefault("Sends out a stellar snake that spews poisonous vapor clouds from its maw");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 10;
			base.item.magic = true;
			base.item.mana = 14;
			base.item.width = 38;
			base.item.height = 40;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = 5500;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<NightshadeSnek>();
			base.item.shootSpeed = 7f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(64, 1);
			modRecipe.AddIngredient(null, "Nightshade", 10);
			modRecipe.AddIngredient(109, 2);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(1256, 1);
			modRecipe2.AddIngredient(null, "Nightshade", 10);
			modRecipe2.AddIngredient(109, 2);
			modRecipe2.AddTile(16);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
