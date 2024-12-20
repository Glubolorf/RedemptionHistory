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
			Player P = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] >= 90f)
			{
				float Speed = 10f;
				Vector2 vector8 = new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int damage = 100;
				int type = base.mod.ProjectileType("HolyBeam");
				float rotation = (float)Math.Atan2((double)(vector8.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector8.X - (P.position.X + (float)P.width * 0.5f)));
				Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 30000 && base.npc.ai[1] >= 60f)
			{
				float Speed2 = 14f;
				Vector2 vector9 = new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int damage2 = 110;
				int type2 = base.mod.ProjectileType("HolyBeam2");
				float rotation2 = (float)Math.Atan2((double)(vector9.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector9.X - (P.position.X + (float)P.width * 0.5f)));
				Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 20000 && base.npc.ai[1] >= 40f)
			{
				float Speed3 = 17f;
				Vector2 vector10 = new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int damage3 = 120;
				int type3 = base.mod.ProjectileType("HolyBeam3");
				float rotation3 = (float)Math.Atan2((double)(vector10.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector10.X - (P.position.X + (float)P.width * 0.5f)));
				Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 10000 && base.npc.ai[1] >= 20f)
			{
				float Speed4 = 26f;
				Vector2 vector11 = new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int damage4 = 120;
				int type4 = base.mod.ProjectileType("HolyBeam3");
				float rotation4 = (float)Math.Atan2((double)(vector11.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector11.X - (P.position.X + (float)P.width * 0.5f)));
				Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 7000 && base.npc.ai[1] >= 10f)
			{
				float Speed5 = 30f;
				Vector2 vector12 = new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int damage5 = 130;
				int type5 = base.mod.ProjectileType("HolyBeam4");
				float rotation5 = (float)Math.Atan2((double)(vector12.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector12.X - (P.position.X + (float)P.width * 0.5f)));
				Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 5000 && base.npc.ai[1] >= 4f)
			{
				float Speed6 = 35f;
				Vector2 vector13 = new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int damage6 = 160;
				int type6 = base.mod.ProjectileType("HolyBeam5");
				float rotation6 = (float)Math.Atan2((double)(vector13.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector13.X - (P.position.X + (float)P.width * 0.5f)));
				Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)rotation6) * (double)Speed6 * -1.0), (float)(Math.Sin((double)rotation6) * (double)Speed6 * -1.0), type6, damage6, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life <= 1000 && base.npc.ai[1] >= 2f)
			{
				float Speed7 = 45f;
				Vector2 vector14 = new Vector2(base.npc.position.X + 110f, base.npc.position.Y + 50f);
				int damage7 = 180;
				int type7 = base.mod.ProjectileType("HolyBeam5");
				float rotation7 = (float)Math.Atan2((double)(vector14.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector14.X - (P.position.X + (float)P.width * 0.5f)));
				Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)rotation7) * (double)Speed7 * -1.0), (float)(Math.Sin((double)rotation7) * (double)Speed7 * -1.0), type7, damage7, 0f, 0, 0f, 0f);
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
