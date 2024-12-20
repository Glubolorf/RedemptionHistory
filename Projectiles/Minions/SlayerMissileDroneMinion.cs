using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.KSIII.Clone;
using Redemption.NPCs.HM;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class SlayerMissileDroneMinion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Missile Drone Mk.I");
			Main.projFrames[base.projectile.type] = 4;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 36;
			base.projectile.penetrate = -1;
			base.projectile.minion = true;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.netImportant = true;
			base.projectile.timeLeft = 900;
			base.projectile.minionSlots = 0f;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
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
			Player player = Main.player[base.projectile.owner];
			player.GetModPlayer<RedePlayer>();
			float[] localAI = base.projectile.localAI;
			int num2 = 0;
			float num3 = localAI[num2] + 1f;
			localAI[num2] = num3;
			if (num3 % 80f == 0f)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 100.0);
				this.vector.Y = (float)(Math.Cos(angle) * 100.0);
				base.projectile.localAI[0] = 1f;
			}
			base.projectile.localAI[1] += 1f;
			if (base.projectile.localAI[1] > 120f && base.projectile.localAI[1] < 500f && this.shotCount < 4 && RedeHelper.ClosestNPC(ref this.target, 1000f, base.projectile.Center, true, player.MinionAttackTargetNPC, null) && this.target.type != ModContent.NPCType<Android>() && this.target.type != ModContent.NPCType<Apidroid>() && this.target.type != ModContent.NPCType<PrototypeSilver>() && this.target.type != ModContent.NPCType<SpaceKeeper>() && this.target.type != ModContent.NPCType<SpacePaladin>() && this.target.type != ModContent.NPCType<KS3_Body>() && this.target.type != ModContent.NPCType<KS3_Body_Clone>())
			{
				this.targetted = true;
				if (base.projectile.localAI[1] % 30f == 0f)
				{
					Main.PlaySound(SoundID.Item74, (int)base.projectile.position.X, (int)base.projectile.position.Y);
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center) + Utils.NextFloat(Main.rand, -20f, 20f)), ModContent.ProjectileType<SlayerMissilePro>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
					this.shotCount++;
				}
			}
			if (base.projectile.localAI[1] >= 500f || this.shotCount >= 4)
			{
				if (!this.targetted)
				{
					CombatText.NewText(base.projectile.getRect(), Colors.RarityCyan, "NO TARGETS DETECTED...", true, true);
					this.targetted = true;
				}
				Projectile projectile3 = base.projectile;
				projectile3.velocity.Y = projectile3.velocity.Y - 0.5f;
				if (Vector2.Distance(base.projectile.Center, player.Center) > 1500f)
				{
					base.projectile.Kill();
					return;
				}
			}
			else
			{
				this.Move(new Vector2(this.vector.X, this.vector.Y));
			}
		}

		public void Move(Vector2 offset)
		{
			Entity entity = Main.player[base.projectile.owner];
			this.speed = 11f;
			Vector2 move = entity.Center + offset - base.projectile.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 15f;
			move = (base.projectile.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.projectile.velocity = move;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D glow = base.mod.GetTexture("Projectiles/Minions/SlayerMissileDroneMinion_Glow");
			int spriteDirection = base.projectile.spriteDirection;
			Vector2 drawCenter2 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y);
			int num215 = texture.Height / 4;
			int y7 = num215 * base.projectile.frame;
			Main.spriteBatch.Draw(texture, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), drawColor, base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Main.spriteBatch.Draw(glow, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), base.projectile.GetAlpha(Color.White), base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public override bool MinionContactDamage()
		{
			return false;
		}

		public float speed;

		private Vector2 vector;

		private NPC target;

		public bool targetted;

		public int shotCount;
	}
}
