using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Druid;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid
{
	public class AngelicFan : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Angelic Fan");
			base.Tooltip.SetDefault("'Born from ashes of the undead'\nThrows a cluster of Druid Daggers\nDruid Daggers tossed from the Angelic Fan unleash life-stealing clouds");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 20f;
			base.item.crit = 4;
			base.item.damage = 37;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 14;
			base.item.useTime = 14;
			base.item.width = 32;
			base.item.height = 32;
			base.item.maxStack = 1;
			base.item.rare = 8;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.shoot = ModContent.ProjectileType<DruidDagger2Pro>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(45f));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1569, 1);
			modRecipe.AddIngredient(null, "DruidDagger", 100);
			modRecipe.AddIngredient(null, "SoulOfBloom", 80);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
