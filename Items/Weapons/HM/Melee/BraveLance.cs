using System;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class BraveLance : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Brave Lance");
			base.Tooltip.SetDefault("'Wielding this lance fills you with strength to fight'\nDeals damage rapidly");
		}

		public override void SetDefaults()
		{
			base.item.damage = 38;
			base.item.useStyle = 5;
			base.item.useAnimation = 21;
			base.item.useTime = 21;
			base.item.shootSpeed = 3.6f;
			base.item.knockBack = 6.4f;
			base.item.width = 68;
			base.item.height = 68;
			base.item.scale = 1f;
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = ModContent.ProjectileType<BraveLancePro>();
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.melee = true;
			base.item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(550, 1);
			modRecipe.AddIngredient(548, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}
	}
}
