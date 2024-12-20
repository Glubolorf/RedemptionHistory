using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions.HoloMinions
{
	public class HoloMinion1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hologram");
			Main.projFrames[base.projectile.type] = 2;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 40;
			base.projectile.height = 40;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 1f;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 100;
			base.projectile.timeLeft = 18000;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.usesLocalNPCImmunity = true;
		}

		private void DustPuff()
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDustPerfect(base.projectile.Center, 226, new Vector2?(RedeHelper.PolarVector((float)Main.rand.Next(4), Utils.NextFloat(Main.rand) * 3.1415927f * 2f)), 0, default(Color), 1f);
			}
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.holoMinion = false;
			}
			if (modPlayer.holoMinion)
			{
				base.projectile.timeLeft = 2;
			}
			if (this.counter % 60 == 0)
			{
				this.flyOffset = RedeHelper.PolarVector((float)Main.rand.Next(100), Utils.NextFloat(Main.rand) * 3.1415927f * 2f);
			}
			this.counter++;
			this.attackCooldown--;
			base.projectile.frameCounter++;
			switch (this.mode)
			{
			case 0:
				this.flyTo = player.Center + this.flyOffset - 30f * Vector2.UnitY;
				if ((player.Center - base.projectile.Center).Length() < 140f && RedeHelper.ClosestNPC(ref this.target, 10000f, player.Center, false, player.MinionAttackTargetNPC, null))
				{
					this.mode = 1;
					this.DustPuff();
				}
				base.projectile.rotation = base.projectile.velocity.X * 3.1415927f / 40f;
				break;
			case 1:
				if (RedeHelper.ClosestNPC(ref this.target, 10000f, player.Center, false, player.MinionAttackTargetNPC, null))
				{
					Vector2 diff = this.target.Center - base.projectile.Center;
					if (diff.Length() < 350f)
					{
						this.mode = 2;
						this.DustPuff();
					}
					else
					{
						base.projectile.rotation = Utils.ToRotation(diff) + 3.1415927f;
						if (this.attackCooldown < 0)
						{
							Projectile.NewProjectile(base.projectile.Center, RedeHelper.PolarVector(25f, Utils.ToRotation(diff)), 440, base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
							this.attackCooldown = 60;
						}
						this.flyTo = player.Center + this.flyOffset + RedeHelper.PolarVector(-50f, Utils.ToRotation(diff));
					}
				}
				else
				{
					this.mode = 0;
				}
				break;
			case 2:
				if (this.attackCooldown < 0)
				{
					if (RedeHelper.ClosestNPC(ref this.target, 400f, base.projectile.Center, true, player.MinionAttackTargetNPC, null))
					{
						this.flyTo = this.target.Center + this.flyOffset * 0.5f;
						Vector2 diff2 = this.flyTo - base.projectile.Center;
						base.projectile.velocity = RedeHelper.PolarVector(20f, Utils.ToRotation(diff2));
						this.attackCooldown = 20;
					}
					else
					{
						this.mode = 0;
						this.DustPuff();
					}
				}
				if (this.attackCooldown < 10)
				{
					base.projectile.velocity *= 0.8f;
				}
				base.projectile.rotation += base.projectile.velocity.X * 3.1415927f * 2f / 30f;
				break;
			}
			if (this.mode != 2)
			{
				Vector2 diff3 = this.flyTo - base.projectile.Center;
				if (diff3.Length() < 10f)
				{
					base.projectile.velocity = diff3;
					return;
				}
				base.projectile.velocity = RedeHelper.PolarVector(10f, Utils.ToRotation(diff3));
			}
		}

		public override bool? CanHitNPC(NPC target)
		{
			return new bool?(this.mode == 2);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			base.projectile.localNPCImmunity[target.whoAmI] = 10;
			target.immune[base.projectile.owner] = 0;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = base.mod.GetTexture("Projectiles/Minions/HoloMinions/HoloMinion1");
			if (this.mode != 0)
			{
				if (this.mode == 1)
				{
					texture = base.mod.GetTexture("Projectiles/Minions/HoloMinions/HoloMinion4");
				}
				else
				{
					texture = base.mod.GetTexture("Projectiles/Minions/HoloMinions/HoloMinion3");
				}
			}
			int frameHeight = texture.Height / 2;
			int frame = (base.projectile.frameCounter % 20 < 10) ? 0 : 1;
			int c = 255 - base.projectile.alpha;
			spriteBatch.Draw(texture, base.projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, frameHeight * frame, texture.Width, frameHeight)), new Color(c, c, c, c), base.projectile.rotation, new Vector2((float)texture.Width, (float)frameHeight) * 0.5f, Vector2.One, (base.projectile.velocity.X > 0f && this.mode != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			return false;
		}

		private int mode;

		private const int chickenMode = 0;

		private const int eyeMode = 1;

		private const int swordMode = 2;

		private Vector2 flyOffset;

		private Vector2 flyTo;

		private int counter;

		private NPC target;

		private int attackCooldown;
	}
}
