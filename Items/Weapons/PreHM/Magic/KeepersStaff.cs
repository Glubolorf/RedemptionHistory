using System;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Magic
{
	public class KeepersStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Staff of the Fallen");
			base.Tooltip.SetDefault("Fires homing Dark Souls");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 26;
			base.item.magic = true;
			base.item.mana = 5;
			base.item.width = 38;
			base.item.height = 44;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<DarkSoulPro2>();
			base.item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DarkShard", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
