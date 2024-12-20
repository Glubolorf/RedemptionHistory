using System;
using Redemption.Items.Materials.PreHM;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class DragonfireYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragonfire");
		}

		public override void SetDefaults()
		{
			base.item.damage = 40;
			base.item.melee = true;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.channel = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 1, 5, 0);
			base.item.rare = 3;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<DragonfireYoyoPro>();
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<DragonLeadBar>(), 6);
			modRecipe.AddIngredient(175, 6);
			modRecipe.AddTile(77);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
