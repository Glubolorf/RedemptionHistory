using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Staves
{
	public class HallowedStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hallowed Stave");
			base.Tooltip.SetDefault("Shoots a golden bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 59;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 4, 60, 30);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = 597;
			base.item.shootSpeed = 16f;
			this.defaultShoot = 597;
			this.guardianBuffID = ModContent.BuffType<NatureGuardian16Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian16>();
			this.guardianTime = 900;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 48.2f;
			this.guardianName = "Holy Statuette";
			this.guardianType = "Mystic";
			this.guardianAbility = "Swift-Cast/Stream-Shot/Holy Aura";
			this.guardianEffects = "Staves cast a lot faster, Staves have a chance to shoot 2 extra projectiles\nDefence Enhancement+/Improved Sight/Mobility Enhancement at day";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1225, 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 57, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
