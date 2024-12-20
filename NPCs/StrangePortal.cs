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
			Player player = Main.player[base.npc.target];
			name = "A Strange Portal";
			potionType = 58;
			if (!RedeWorld.downedStrangePortal)
			{
				CombatText.NewText(player.getRect(), Color.Gray, "+0", true, false);
			}
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
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] >= 400f)
			{
				base.npc.aiStyle = 97;
			}
			if (base.npc.ai[1] <= 400f)
			{
				base.npc.aiStyle = 2;
			}
			if (base.npc.ai[1] >= 800f)
			{
				base.npc.ai[1] = 0f;
			}
			if (base.npc.ai[1] == 420f)
			{
				float num = 5f;
				Vector2 vector;
				vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num2 = 7;
				int num3 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num4 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
				int num5 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
				Main.projectile[num5].netUpdate = true;
			}
			if (base.npc.ai[1] == 440f)
			{
				float num6 = 5f;
				Vector2 vector2;
				vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num7 = 7;
				int num8 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num9 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
				int num10 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num9) * (double)num6 * -1.0), (float)(Math.Sin((double)num9) * (double)num6 * -1.0), num8, num7, 0f, 0, 0f, 0f);
				Main.projectile[num10].netUpdate = true;
			}
			if (base.npc.ai[1] == 460f)
			{
				float num11 = 5f;
				Vector2 vector3;
				vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num12 = 7;
				int num13 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num14 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
				int num15 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num14) * (double)num11 * -1.0), (float)(Math.Sin((double)num14) * (double)num11 * -1.0), num13, num12, 0f, 0, 0f, 0f);
				Main.projectile[num15].netUpdate = true;
			}
			if (base.npc.ai[1] == 520f)
			{
				float num16 = 5f;
				Vector2 vector4;
				vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num17 = 7;
				int num18 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num19 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
				int num20 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num19) * (double)num16 * -1.0), (float)(Math.Sin((double)num19) * (double)num16 * -1.0), num18, num17, 0f, 0, 0f, 0f);
				Main.projectile[num20].netUpdate = true;
			}
			if (base.npc.ai[1] == 540f)
			{
				float num21 = 5f;
				Vector2 vector5;
				vector5..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num22 = 7;
				int num23 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num24 = (float)Math.Atan2((double)(vector5.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector5.X - (player.position.X + (float)player.width * 0.5f)));
				int num25 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num24) * (double)num21 * -1.0), (float)(Math.Sin((double)num24) * (double)num21 * -1.0), num23, num22, 0f, 0, 0f, 0f);
				Main.projectile[num25].netUpdate = true;
			}
			if (base.npc.ai[1] == 560f)
			{
				float num26 = 5f;
				Vector2 vector6;
				vector6..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num27 = 7;
				int num28 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num29 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
				int num30 = Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0), (float)(Math.Sin((double)num29) * (double)num26 * -1.0), num28, num27, 0f, 0, 0f, 0f);
				Main.projectile[num30].netUpdate = true;
			}
			if (base.npc.ai[1] == 620f)
			{
				float num31 = 5f;
				Vector2 vector7;
				vector7..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num32 = 7;
				int num33 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num34 = (float)Math.Atan2((double)(vector7.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector7.X - (player.position.X + (float)player.width * 0.5f)));
				int num35 = Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num34) * (double)num31 * -1.0), (float)(Math.Sin((double)num34) * (double)num31 * -1.0), num33, num32, 0f, 0, 0f, 0f);
				Main.projectile[num35].netUpdate = true;
			}
			if (base.npc.ai[1] == 630f)
			{
				float num36 = 5f;
				Vector2 vector8;
				vector8..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num37 = 7;
				int num38 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num39 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
				int num40 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num39) * (double)num36 * -1.0), (float)(Math.Sin((double)num39) * (double)num36 * -1.0), num38, num37, 0f, 0, 0f, 0f);
				Main.projectile[num40].netUpdate = true;
			}
			if (base.npc.ai[1] == 640f)
			{
				float num41 = 5f;
				Vector2 vector9;
				vector9..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num42 = 7;
				int num43 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num44 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
				int num45 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num44) * (double)num41 * -1.0), (float)(Math.Sin((double)num44) * (double)num41 * -1.0), num43, num42, 0f, 0, 0f, 0f);
				Main.projectile[num45].netUpdate = true;
			}
			if (base.npc.ai[1] == 650f)
			{
				float num46 = 5f;
				Vector2 vector10;
				vector10..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num47 = 7;
				int num48 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num49 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
				int num50 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num49) * (double)num46 * -1.0), (float)(Math.Sin((double)num49) * (double)num46 * -1.0), num48, num47, 0f, 0, 0f, 0f);
				Main.projectile[num50].netUpdate = true;
			}
			if (base.npc.ai[1] == 660f)
			{
				float num51 = 5f;
				Vector2 vector11;
				vector11..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num52 = 7;
				int num53 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num54 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
				int num55 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num54) * (double)num51 * -1.0), (float)(Math.Sin((double)num54) * (double)num51 * -1.0), num53, num52, 0f, 0, 0f, 0f);
				Main.projectile[num55].netUpdate = true;
			}
			if (base.npc.ai[1] == 670f)
			{
				float num56 = 5f;
				Vector2 vector12;
				vector12..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num57 = 7;
				int num58 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num59 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
				int num60 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num59) * (double)num56 * -1.0), (float)(Math.Sin((double)num59) * (double)num56 * -1.0), num58, num57, 0f, 0, 0f, 0f);
				Main.projectile[num60].netUpdate = true;
			}
			if (base.npc.ai[1] == 680f)
			{
				float num61 = 5f;
				Vector2 vector13;
				vector13..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num62 = 7;
				int num63 = 573;
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num64 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
				int num65 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num64) * (double)num61 * -1.0), (float)(Math.Sin((double)num64) * (double)num61 * -1.0), num63, num62, 0f, 0, 0f, 0f);
				Main.projectile[num65].netUpdate = true;
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(1) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(164, 200, true);
			}
		}
	}
}
