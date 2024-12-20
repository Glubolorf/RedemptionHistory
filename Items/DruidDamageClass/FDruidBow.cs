using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class FDruidBow : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bow of the Blind Seamstress");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots an arrow dealing druidic damage\nRight-clicking shoots an arrow dealing ranged damage\nWhen shooting, you have a chance to summon a damaging Forest Soul around you");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 42;
			base.item.noMelee = true;
			base.item.ranged = false;
			base.item.width = 16;
			base.item.height = 36;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 5;
			base.item.shoot = 1;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.crit = 4;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 2, 35, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item5;
			base.item.shoot = 206;
			base.item.shootSpeed = 14f;
			base.item.autoReuse = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.ranged = true;
			}
			else
			{
				base.item.ranged = false;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.rand.Next(10) == 0 && player.ownedProjectileCounts[base.mod.ProjectileType("ForestSoul")] <= 5)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, base.mod.ProjectileType("ForestSoul"), 45, 0f, player.whoAmI, 0f, 0f);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ForestCore", 8);
			modRecipe.AddIngredient(null, "LostSoul", 2);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
