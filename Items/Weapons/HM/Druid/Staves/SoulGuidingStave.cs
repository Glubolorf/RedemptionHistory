using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Druid.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Staves
{
	public class SoulGuidingStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soul-Guiding Stave");
			base.Tooltip.SetDefault("Shoots souls of light and night to brighten your path\nSlaying an enemy with this increases your Spirit Level by 1 for a minute");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 31;
			base.item.width = 32;
			base.item.height = 38;
			base.item.useTime = 23;
			base.item.useAnimation = 23;
			base.item.crit = 4;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 2, 35, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = ModContent.ProjectileType<LightSoulPro1>();
			base.item.shootSpeed = 17f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			this.defaultShoot = ModContent.ProjectileType<LightSoulPro1>();
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 32.2f;
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<NightSoulPro1>(), damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(520, 20);
			modRecipe.AddIngredient(521, 20);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddIngredient(null, "ForestCore", 6);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
