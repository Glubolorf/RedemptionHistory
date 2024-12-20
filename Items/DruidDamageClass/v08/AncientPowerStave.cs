using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class AncientPowerStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Power Stave");
			base.Tooltip.SetDefault("");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 235;
			base.item.width = 76;
			base.item.height = 76;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 20, 0, 30);
			base.item.UseSound = SoundID.Item8;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.shoot = ModContent.ProjectileType<AncientTimeAura>();
			base.item.shootSpeed = 0f;
			this.defaultShoot = ModContent.ProjectileType<AncientTimeAura>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian27Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian27>();
			this.guardianTime = 600;
			this.singleShotStave = false;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 76.2f;
			this.guardianName = "Ancient Statue";
			this.guardianType = "Mystic";
			this.guardianAbility = "Swift-Cast/Ancient Embrace";
			this.guardianEffects = "Staves cast a lot faster, Defense Enhancement+++";
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			return true;
		}
	}
}
