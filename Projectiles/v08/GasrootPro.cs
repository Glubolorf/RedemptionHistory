using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class GasrootPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gasroot");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 28;
			base.projectile.height = 40;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			base.projectile.spriteDirection = base.projectile.direction;
			base.projectile.rotation = base.projectile.velocity.X * 0.05f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 15f && base.projectile.localAI[0] < 28f)
			{
				if (base.projectile.velocity.X < 0f)
				{
					Projectile projectile = base.projectile;
					projectile.velocity.X = projectile.velocity.X + 1f;
				}
				if (base.projectile.velocity.X > 0f)
				{
					Projectile projectile2 = base.projectile;
					projectile2.velocity.X = projectile2.velocity.X - 1f;
				}
				if (base.projectile.velocity.Y < 0f)
				{
					Projectile projectile3 = base.projectile;
					projectile3.velocity.Y = projectile3.velocity.Y + 1f;
				}
				if (base.projectile.velocity.Y > 0f)
				{
					Projectile projectile4 = base.projectile;
					projectile4.velocity.Y = projectile4.velocity.Y - 1f;
				}
			}
			if (base.projectile.localAI[0] >= 28f && base.projectile.localAI[0] < 30f)
			{
				base.projectile.velocity.X = 0f;
				base.projectile.velocity.Y = 0f;
			}
			if (base.projectile.localAI[0] == 30f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
			if (base.projectile.localAI[0] >= 30f)
			{
				Projectile projectile5 = base.projectile;
				int num = projectile5.frameCounter + 1;
				projectile5.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile6 = base.projectile;
					num = projectile6.frame + 1;
					projectile6.frame = num;
					if (num >= 5)
					{
						base.projectile.frame = 3;
					}
				}
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5)
				{
					Vector2 newMove = Main.npc[i].Center - base.projectile.Center;
					float distanceTo = (float)Math.Sqrt((double)(newMove.X * newMove.X + newMove.Y * newMove.Y));
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				this.AdjustMagnitude(ref move);
				base.projectile.velocity = (10f * base.projectile.velocity + move) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 7f)
			{
				vector *= 8f / magnitude;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int critChance = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref critChance);
			if (critChance >= 100 || Main.rand.Next(1, 101) <= critChance)
			{
				crit = true;
			}
			target.AddBuff(39, 200, false);
		}

		public override void Kill(int timeLeft)
		{
			if (base.projectile.frame > 2)
			{
				Main.PlaySound(SoundID.Item54, base.projectile.position);
				int pieCut = 16;
				for (int i = 0; i < pieCut; i++)
				{
					int projID = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("GasrootPro2"), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)i / (float)pieCut * 6.28f);
				}
			}
		}
	}
}
