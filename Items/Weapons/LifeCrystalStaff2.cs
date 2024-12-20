using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LifeCrystalStaff2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Heart Scepter");
			base.Tooltip.SetDefault("'Fills the target with love'\nShoots tiny hearts\nWhile holding this, life regen is increased");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 17;
			base.item.magic = true;
			base.item.mana = 4;
			base.item.width = 72;
			base.item.height = 72;
			base.item.useTime = 23;
			base.item.useAnimation = 23;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 0, 75, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<TinyHeartPro>();
			base.item.shootSpeed = 15f;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(2, 4, true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(29, 1);
			modRecipe.AddIngredient(706, 12);
			modRecipe.AddIngredient(751, 5);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
