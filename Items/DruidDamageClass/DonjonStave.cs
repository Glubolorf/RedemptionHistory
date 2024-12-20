using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class DonjonStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Donjon Stave");
			base.Tooltip.SetDefault("Shoots a Water Bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 23;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 54, 30);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = 27;
			base.item.shootSpeed = 16f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			this.defaultShoot = 27;
			this.guardianBuffID = base.mod.BuffType("NatureGuardian10Buff");
			this.guardianProjectileID = base.mod.ProjectileType("NatureGuardian10");
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 48.2f;
			this.guardianName = "Skeletal Guardian";
			this.guardianType = "Guardian";
			this.guardianAbility = "Swift-Cast/Ironbone Aura";
			this.guardianEffects = "Staves cast a lot faster, Defence Enhancement";
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(4) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 15, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
