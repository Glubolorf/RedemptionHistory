using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.SeedOfInfection
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
			base.npc.noTileCollide = true;
			base.npc.boss = true;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Strange Portal";
			potionType = 58;
			if (!RedeWorld.downedStrangePortal)
			{
				for (int i = 0; i < 255; i++)
				{
					CombatText.NewText(Main.player[i].getRect(), Color.Gray, "+0", true, false);
				}
			}
			RedeWorld.downedStrangePortal = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<StrangePortal2>(), 0, 0f, Main.myPlayer, 0f, 0f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 30; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 15.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 46;
				if (base.npc.frame.Y > 138)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			base.npc.rotation += 0.09f;
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			if (!this.boom)
			{
				for (int i = 0; i < 10; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[dustIndex].velocity *= 2.9f;
				}
				this.boom = true;
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
				float Speed = 7f;
				Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage = 7;
				int type = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
				Main.projectile[num54].netUpdate = true;
			}
			if (this.timer == 440)
			{
				float Speed2 = 7f;
				Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage2 = 7;
				int type2 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
				int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
				Main.projectile[num55].netUpdate = true;
			}
			if (this.timer == 460)
			{
				float Speed3 = 7f;
				Vector2 vector10 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage3 = 7;
				int type3 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
				int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
				Main.projectile[num56].netUpdate = true;
			}
			if (this.timer == 520)
			{
				float Speed4 = 7f;
				Vector2 vector11 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage4 = 7;
				int type4 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation4 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
				int num57 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
				Main.projectile[num57].netUpdate = true;
			}
			if (this.timer == 540)
			{
				float Speed5 = 7f;
				Vector2 vector12 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage5 = 7;
				int type5 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation5 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
				int num58 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
				Main.projectile[num58].netUpdate = true;
			}
			if (this.timer == 560)
			{
				float Speed6 = 7f;
				Vector2 vector13 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage6 = 7;
				int type6 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation6 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
				int num59 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)rotation6) * (double)Speed6 * -1.0), (float)(Math.Sin((double)rotation6) * (double)Speed6 * -1.0), type6, damage6, 0f, 0, 0f, 0f);
				Main.projectile[num59].netUpdate = true;
			}
			if (this.timer == 620)
			{
				float Speed7 = 7f;
				Vector2 vector14 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage7 = 7;
				int type7 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation7 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
				int num60 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)rotation7) * (double)Speed7 * -1.0), (float)(Math.Sin((double)rotation7) * (double)Speed7 * -1.0), type7, damage7, 0f, 0, 0f, 0f);
				Main.projectile[num60].netUpdate = true;
			}
			if (this.timer == 630)
			{
				float Speed8 = 7f;
				Vector2 vector15 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage8 = 7;
				int type8 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation8 = (float)Math.Atan2((double)(vector15.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector15.X - (player.position.X + (float)player.width * 0.5f)));
				int num61 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)rotation8) * (double)Speed8 * -1.0), (float)(Math.Sin((double)rotation8) * (double)Speed8 * -1.0), type8, damage8, 0f, 0, 0f, 0f);
				Main.projectile[num61].netUpdate = true;
			}
			if (this.timer == 640)
			{
				float Speed9 = 7f;
				Vector2 vector16 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage9 = 7;
				int type9 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation9 = (float)Math.Atan2((double)(vector16.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector16.X - (player.position.X + (float)player.width * 0.5f)));
				int num62 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)rotation9) * (double)Speed9 * -1.0), (float)(Math.Sin((double)rotation9) * (double)Speed9 * -1.0), type9, damage9, 0f, 0, 0f, 0f);
				Main.projectile[num62].netUpdate = true;
			}
			if (this.timer == 650)
			{
				float Speed10 = 7f;
				Vector2 vector17 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage10 = 7;
				int type10 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation10 = (float)Math.Atan2((double)(vector17.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector17.X - (player.position.X + (float)player.width * 0.5f)));
				int num63 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)rotation10) * (double)Speed10 * -1.0), (float)(Math.Sin((double)rotation10) * (double)Speed10 * -1.0), type10, damage10, 0f, 0, 0f, 0f);
				Main.projectile[num63].netUpdate = true;
			}
			if (this.timer == 660)
			{
				float Speed11 = 7f;
				Vector2 vector18 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage11 = 7;
				int type11 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation11 = (float)Math.Atan2((double)(vector18.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector18.X - (player.position.X + (float)player.width * 0.5f)));
				int num64 = Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)rotation11) * (double)Speed11 * -1.0), (float)(Math.Sin((double)rotation11) * (double)Speed11 * -1.0), type11, damage11, 0f, 0, 0f, 0f);
				Main.projectile[num64].netUpdate = true;
			}
			if (this.timer == 670)
			{
				float Speed12 = 7f;
				Vector2 vector19 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage12 = 7;
				int type12 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation12 = (float)Math.Atan2((double)(vector19.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector19.X - (player.position.X + (float)player.width * 0.5f)));
				int num65 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)rotation12) * (double)Speed12 * -1.0), (float)(Math.Sin((double)rotation12) * (double)Speed12 * -1.0), type12, damage12, 0f, 0, 0f, 0f);
				Main.projectile[num65].netUpdate = true;
			}
			if (this.timer == 680)
			{
				float Speed13 = 7f;
				Vector2 vector20 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage13 = 7;
				int type13 = ModContent.ProjectileType<ShardShot1>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float rotation13 = (float)Math.Atan2((double)(vector20.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector20.X - (player.position.X + (float)player.width * 0.5f)));
				int num66 = Projectile.NewProjectile(vector20.X, vector20.Y, (float)(Math.Cos((double)rotation13) * (double)Speed13 * -1.0), (float)(Math.Sin((double)rotation13) * (double)Speed13 * -1.0), type13, damage13, 0f, 0, 0f, 0f);
				Main.projectile[num66].netUpdate = true;
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(164, 200, true);
		}

		public bool boom;

		public int timer;
	}
}
