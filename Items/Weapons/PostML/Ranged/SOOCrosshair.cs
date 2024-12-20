using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class SOOCrosshair : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/OmegaOblit/OOCrosshair";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crosshair");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 300;
			base.projectile.alpha = 255;
			this.drawOffsetX = -18;
			this.drawOriginOffsetY = -18;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f);
			Player player = Main.player[base.projectile.owner];
			if (!player.active || player.dead || base.projectile.localAI[0] >= 5f)
			{
				base.projectile.Kill();
			}
			if (base.projectile.ai[0] == 0f)
			{
				float obj = base.projectile.ai[1];
				if (!0f.Equals(obj))
				{
					if (!1f.Equals(obj))
					{
						return;
					}
					if (!this.locked.active)
					{
						base.projectile.Kill();
					}
					base.projectile.Center = this.locked.Center;
					base.projectile.alpha = 0;
					base.projectile.localAI[1] += 1f;
					if (base.projectile.localAI[1] >= 30f && base.projectile.localAI[1] % 5f == 0f && base.projectile.localAI[1] < 60f)
					{
						if (!Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MissileFire1").WithVolume(0.8f).WithPitchVariance(0.1f), (int)player.position.X, (int)player.position.Y);
						}
						Projectile.NewProjectile(new Vector2((player.direction == 1) ? (player.Center.X + 12f) : (player.Center.X - 12f), player.Center.Y - 10f), new Vector2((float)((player.direction == 1) ? 10 : -10), 0f), ModContent.ProjectileType<SOmegaMissile>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, (float)base.projectile.whoAmI, 1f);
					}
					if (base.projectile.localAI[1] >= 60f && !RedeHelper.AnyProjectiles(ModContent.ProjectileType<SOmegaMissile>()))
					{
						base.projectile.Kill();
						return;
					}
				}
				else
				{
					if (RedeHelper.ClosestNPC(ref this.target, 300f, base.projectile.Center, true, player.MinionAttackTargetNPC, null))
					{
						this.locked = Main.npc[this.target.whoAmI];
						base.projectile.ai[1] = 1f;
						return;
					}
					CombatText.NewText(player.getRect(), Color.Red, "No targets found!", false, false);
					base.projectile.Kill();
					return;
				}
			}
			else
			{
				base.projectile.alpha = 0;
				base.projectile.localAI[1] += 1f;
				if (base.projectile.localAI[1] == 30f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MissileFire1").WithVolume(0.8f).WithPitchVariance(0.1f), (int)player.position.X, (int)player.position.Y);
					}
					Projectile.NewProjectile(new Vector2((player.direction == 1) ? (player.Center.X + 12f) : (player.Center.X - 12f), player.Center.Y - 10f), new Vector2((float)((player.direction == 1) ? 10 : -10), 0f), ModContent.ProjectileType<SOmegaMissile>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, (float)base.projectile.whoAmI, 0f);
				}
			}
		}

		private NPC target;

		private NPC locked;
	}
}
