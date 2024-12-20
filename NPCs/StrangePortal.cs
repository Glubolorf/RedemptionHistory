using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	[AutoloadBossHead]
	public class StrangePortal : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Strange Portal");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 42;
			base.npc.height = 42;
			base.npc.friendly = false;
			base.npc.damage = 25;
			base.npc.defense = 0;
			base.npc.lifeMax = 750;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 2;
			this.aiType = 34;
			this.animationType = 34;
			base.npc.boss = true;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Strange Portal";
			potionType = 58;
			RedeWorld.downedStrangePortal = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 30; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 242, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 242, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			base.npc.rotation += 0.09f;
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 242, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			this.timer++;
			if (this.timer >= 400)
			{
				base.npc.aiStyle = 97;
			}
			if (this.timer <= 400)
			{
				base.npc.aiStyle = 2;
			}
			if (this.timer >= 800)
			{
				this.timer = 0;
			}
			if (this.timer == 420)
			{
				float num = 5f;
				Vector2 vector;
				vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num2 = 7;
				int num3 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num4 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
			}
			if (this.timer == 440)
			{
				float num5 = 5f;
				Vector2 vector2;
				vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num6 = 7;
				int num7 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num8 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num8) * (double)num5 * -1.0), (float)(Math.Sin((double)num8) * (double)num5 * -1.0), num7, num6, 0f, 0, 0f, 0f);
			}
			if (this.timer == 460)
			{
				float num9 = 5f;
				Vector2 vector3;
				vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num10 = 7;
				int num11 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num12 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num12) * (double)num9 * -1.0), (float)(Math.Sin((double)num12) * (double)num9 * -1.0), num11, num10, 0f, 0, 0f, 0f);
			}
			if (this.timer == 520)
			{
				float num13 = 5f;
				Vector2 vector4;
				vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num14 = 7;
				int num15 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num16 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num16) * (double)num13 * -1.0), (float)(Math.Sin((double)num16) * (double)num13 * -1.0), num15, num14, 0f, 0, 0f, 0f);
			}
			if (this.timer == 540)
			{
				float num17 = 5f;
				Vector2 vector5;
				vector5..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num18 = 7;
				int num19 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num20 = (float)Math.Atan2((double)(vector5.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector5.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num20) * (double)num17 * -1.0), (float)(Math.Sin((double)num20) * (double)num17 * -1.0), num19, num18, 0f, 0, 0f, 0f);
			}
			if (this.timer == 560)
			{
				float num21 = 5f;
				Vector2 vector6;
				vector6..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num22 = 7;
				int num23 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num24 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num24) * (double)num21 * -1.0), (float)(Math.Sin((double)num24) * (double)num21 * -1.0), num23, num22, 0f, 0, 0f, 0f);
			}
			if (this.timer == 620)
			{
				float num25 = 5f;
				Vector2 vector7;
				vector7..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num26 = 7;
				int num27 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num28 = (float)Math.Atan2((double)(vector7.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector7.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num28) * (double)num25 * -1.0), (float)(Math.Sin((double)num28) * (double)num25 * -1.0), num27, num26, 0f, 0, 0f, 0f);
			}
			if (this.timer == 630)
			{
				float num29 = 5f;
				Vector2 vector8;
				vector8..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num30 = 7;
				int num31 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num32 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num32) * (double)num29 * -1.0), (float)(Math.Sin((double)num32) * (double)num29 * -1.0), num31, num30, 0f, 0, 0f, 0f);
			}
			if (this.timer == 640)
			{
				float num33 = 5f;
				Vector2 vector9;
				vector9..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num34 = 7;
				int num35 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num36 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num36) * (double)num33 * -1.0), (float)(Math.Sin((double)num36) * (double)num33 * -1.0), num35, num34, 0f, 0, 0f, 0f);
			}
			if (this.timer == 650)
			{
				float num37 = 5f;
				Vector2 vector10;
				vector10..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num38 = 7;
				int num39 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num40 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num40) * (double)num37 * -1.0), (float)(Math.Sin((double)num40) * (double)num37 * -1.0), num39, num38, 0f, 0, 0f, 0f);
			}
			if (this.timer == 660)
			{
				float num41 = 5f;
				Vector2 vector11;
				vector11..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num42 = 7;
				int num43 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num44 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num44) * (double)num41 * -1.0), (float)(Math.Sin((double)num44) * (double)num41 * -1.0), num43, num42, 0f, 0, 0f, 0f);
			}
			if (this.timer == 670)
			{
				float num45 = 5f;
				Vector2 vector12;
				vector12..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num46 = 7;
				int num47 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num48 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num48) * (double)num45 * -1.0), (float)(Math.Sin((double)num48) * (double)num45 * -1.0), num47, num46, 0f, 0, 0f, 0f);
			}
			if (this.timer == 680)
			{
				float num49 = 5f;
				Vector2 vector13;
				vector13..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num50 = 7;
				int num51 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num52 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num52) * (double)num49 * -1.0), (float)(Math.Sin((double)num52) * (double)num49 * -1.0), num51, num50, 0f, 0, 0f, 0f);
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(1) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(164, 200, true);
			}
		}

		public int timer;
	}
}
