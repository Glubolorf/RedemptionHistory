using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class AncientBrassBoomerang : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Boomerang");
			base.Tooltip.SetDefault("'A bladed boomerang made completely of old brass.'\nPenetrates enemies and stops briefly before returning.");
		}

		public override void SetDefaults()
		{
			base.item.damage = 14;
			base.item.melee = true;
			base.item.width = 24;
			base.item.height = 40;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = 1400;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.shootSpeed = 10f;
			base.item.shoot = base.mod.ProjectileType("AncientBrassBoomerangProj");
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientBrassIngot", 15);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
