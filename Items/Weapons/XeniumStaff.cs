using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XeniumStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Staff");
			base.Tooltip.SetDefault("Casts waving Xenium Bolts");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 200;
			base.item.magic = true;
			base.item.mana = 15;
			base.item.width = 58;
			base.item.height = 58;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item117;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("XeniumStaffPro1");
			base.item.shootSpeed = 14f;
		}

		public override bool Shoot(Player player, ref Vector2 shootPos, ref float speedX, ref float speedY, ref int projType, ref int damage, ref float knockback)
		{
			Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX, speedY, projType, damage, knockback, player.whoAmI, 0f, 0f);
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX * 1f, speedY * 1f, (i == 0) ? base.mod.ProjectileType("XeniumStaffPro2") : base.mod.ProjectileType("XeniumStaffPro3"), damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 19);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
