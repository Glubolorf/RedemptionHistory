using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class ServantOfTheHolyKnight : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Servant Of The Holy Knight");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 220;
			base.npc.height = 266;
			base.npc.friendly = false;
			base.npc.damage = 250;
			base.npc.defense = 90;
			base.npc.lifeMax = 50000;
			base.npc.HitSound = SoundID.NPCHit42;
			base.npc.DeathSound = SoundID.NPCDeath37;
			base.npc.value = (float)Item.buyPrice(0, 10, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 5;
			base.npc.noGravity = true;
			base.npc.alpha = 50;
			base.npc.noTileCollide = true;
			this.animationType = 82;
		}

		public override void AI()
		{
			base.npc.ai[0] += 1f;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] >= 90f)
			{
				float num = 10f;
				Vector2 vector;
				vector..ctor(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int num2 = 100;
				int num3 = base.mod.ProjectileType("HolyBeam");
				float num4 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 30000 && base.npc.ai[1] >= 60f)
			{
				float num5 = 14f;
				Vector2 vector2;
				vector2..ctor(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int num6 = 110;
				int num7 = base.mod.ProjectileType("HolyBeam2");
				float num8 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num8) * (double)num5 * -1.0), (float)(Math.Sin((double)num8) * (double)num5 * -1.0), num7, num6, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 20000 && base.npc.ai[1] >= 40f)
			{
				float num9 = 17f;
				Vector2 vector3;
				vector3..ctor(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int num10 = 120;
				int num11 = base.mod.ProjectileType("HolyBeam3");
				float num12 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num12) * (double)num9 * -1.0), (float)(Math.Sin((double)num12) * (double)num9 * -1.0), num11, num10, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 10000 && base.npc.ai[1] >= 20f)
			{
				float num13 = 26f;
				Vector2 vector4;
				vector4..ctor(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int num14 = 120;
				int num15 = base.mod.ProjectileType("HolyBeam3");
				float num16 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num16) * (double)num13 * -1.0), (float)(Math.Sin((double)num16) * (double)num13 * -1.0), num15, num14, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 7000 && base.npc.ai[1] >= 10f)
			{
				float num17 = 30f;
				Vector2 vector5;
				vector5..ctor(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int num18 = 130;
				int num19 = base.mod.ProjectileType("HolyBeam4");
				float num20 = (float)Math.Atan2((double)(vector5.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector5.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num20) * (double)num17 * -1.0), (float)(Math.Sin((double)num20) * (double)num17 * -1.0), num19, num18, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 5000 && base.npc.ai[1] >= 4f)
			{
				float num21 = 35f;
				Vector2 vector6;
				vector6..ctor(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int num22 = 160;
				int num23 = base.mod.ProjectileType("HolyBeam5");
				float num24 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num24) * (double)num21 * -1.0), (float)(Math.Sin((double)num24) * (double)num21 * -1.0), num23, num22, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 1000 && base.npc.ai[1] >= 2f)
			{
				float num25 = 45f;
				Vector2 vector7;
				vector7..ctor(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int num26 = 180;
				int num27 = base.mod.ProjectileType("HolyBeam5");
				float num28 = (float)Math.Atan2((double)(vector7.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector7.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num28) * (double)num25 * -1.0), (float)(Math.Sin((double)num28) * (double)num25 * -1.0), num27, num26, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life > 10000)
			{
				this.specialAttack1++;
				if (this.specialAttack1 == 400)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(0f, -5f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(0f, 5f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(-5f, 0f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(5f, 0f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					this.specialAttack1 = 0;
				}
			}
			if (base.npc.life > 10000 && base.npc.life < 35000 && this.specialAttack1 == 200)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(5f, 5f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(5f, -5f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(-5f, 5f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(-5f, -5f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
			}
			if (base.npc.life < 10000 && base.npc.life > 6000)
			{
				this.specialAttack2++;
				if (this.specialAttack2 == 250)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(0f, -8f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(0f, 8f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(-8f, 0f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(8f, 0f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(6f, 6f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(6f, -6f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(-6f, 6f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(-6f, -6f), base.mod.ProjectileType("HolyBeam5"), 150, 3f, 255, 0f, 0f);
					this.specialAttack2 = 0;
				}
			}
			if (base.npc.life < 6000)
			{
				this.specialAttack3++;
				if (this.specialAttack3 == 60)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(0f, -8f), base.mod.ProjectileType("HolyBeam5"), 180, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(0f, 8f), base.mod.ProjectileType("HolyBeam5"), 180, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(-8f, 0f), base.mod.ProjectileType("HolyBeam5"), 180, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(8f, 0f), base.mod.ProjectileType("HolyBeam5"), 180, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(6f, 6f), base.mod.ProjectileType("HolyBeam5"), 180, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(6f, -6f), base.mod.ProjectileType("HolyBeam5"), 180, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(-6f, 6f), base.mod.ProjectileType("HolyBeam5"), 180, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f), new Vector2(-6f, -6f), base.mod.ProjectileType("HolyBeam5"), 180, 3f, 255, 0f, 0f);
					this.specialAttack3 = 0;
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public int specialAttack1;

		public int specialAttack2;

		public int specialAttack3;
	}
}
