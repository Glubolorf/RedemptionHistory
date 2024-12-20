using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class InfectedEye : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Eye");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = 5;
			base.npc.lifeMax = 20000;
			base.npc.damage = 60;
			base.npc.defense = 20;
			base.npc.knockBackResist = 0f;
			base.npc.width = 110;
			base.npc.height = 166;
			base.npc.friendly = false;
			this.animationType = 2;
			base.npc.value = (float)Item.buyPrice(0, 6, 50, 0);
			base.npc.npcSlots = 1f;
			base.npc.boss = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.buffImmune[24] = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossXeno2");
			base.npc.buffImmune[20] = true;
			base.npc.netAlways = true;
			this.bossBag = base.mod.ItemType("InfectedEyeBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore5"), 1f);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("InfectedEyeTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteStaff"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("TheInfectedEye"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Xenomite"), Main.rand.Next(4, 6), false, 0, false, false);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			RedeWorld.downedInfectedEye = true;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.579f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			if (Main.dayTime)
			{
				NPC npc = base.npc;
				npc.position.Y = npc.position.Y - 300f;
			}
			this.Target();
			this.DespawnHandler();
			base.npc.ai[0] += 1f;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			if (base.npc.life <= 9000)
			{
				this.specialAttack++;
				if (this.specialAttack >= 600 && this.specialAttack <= 639)
				{
					base.npc.velocity.X = 0f;
					base.npc.velocity.Y = 0f;
					base.npc.rotation += 0.11f;
					base.npc.aiStyle = 2;
				}
				if (this.specialAttack == 600)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 82f, base.npc.position.Y + 64f), new Vector2(-8f, 0f), base.mod.ProjectileType("InfectedEyePro"), 40, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack == 610)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 82f, base.npc.position.Y + 64f), new Vector2(-8f, -8f), base.mod.ProjectileType("InfectedEyePro"), 40, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack == 620)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 82f, base.npc.position.Y + 64f), new Vector2(0f, -8f), base.mod.ProjectileType("InfectedEyePro"), 40, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack == 630)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 82f, base.npc.position.Y + 64f), new Vector2(8f, -8f), base.mod.ProjectileType("InfectedEyePro"), 40, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack == 640)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 82f, base.npc.position.Y + 64f), new Vector2(8f, 0f), base.mod.ProjectileType("InfectedEyePro"), 40, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack == 630)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 82f, base.npc.position.Y + 64f), new Vector2(8f, 8f), base.mod.ProjectileType("InfectedEyePro"), 40, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack == 630)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 82f, base.npc.position.Y + 64f), new Vector2(0f, 8f), base.mod.ProjectileType("InfectedEyePro"), 40, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack >= 640)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 82f, base.npc.position.Y + 64f), new Vector2(-8f, 8f), base.mod.ProjectileType("InfectedEyePro"), 40, 3f, 255, 0f, 0f);
					this.specialAttack = 0;
					base.npc.aiStyle = 5;
				}
			}
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] >= 80f)
			{
				float num = 12f;
				Vector2 vector;
				vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num2 = 60;
				int num3 = base.mod.ProjectileType("InfectedEyePro");
				float num4 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
				base.npc.ai[1] = 0f;
			}
			if (base.npc.ai[0] % 200f == 3f && NPC.CountNPCS(base.mod.NPCType("XenomiteEye")) <= 4)
			{
				NPC.NewNPC((int)base.npc.position.X + 80, (int)base.npc.position.Y + 80, base.mod.NPCType("XenomiteEye"), 0, 0f, 0f, 0f, 0f, 255);
			}
			base.npc.ai[1] += 0f;
			if (base.npc.life <= 10000)
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 20f)
			{
				NPC npc2 = base.npc;
				npc2.velocity.X = npc2.velocity.X * 0.58f;
				NPC npc3 = base.npc;
				npc3.velocity.Y = npc3.velocity.Y * 0.58f;
				Vector2 vector2;
				vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float num5 = (float)Math.Atan2((double)(vector2.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector2.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
				base.npc.velocity.X = (float)(Math.Cos((double)num5) * 12.0) * -1f;
				base.npc.velocity.Y = (float)(Math.Sin((double)num5) * 12.0) * -1f;
				base.npc.ai[0] %= 6.2831855f;
				new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
				base.npc.ai[2] = -300f;
				Color color = default(Color);
				Rectangle rectangle;
				rectangle..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
				int num6 = 30;
				for (int i = 1; i <= num6; i++)
				{
					int num7 = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 107, 0f, 0f, 100, color, 2.5f);
					Main.dust[num7].noGravity = false;
				}
				return;
			}
			base.npc.ai[1] += 0f;
			if (base.npc.life <= 5000)
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 20f)
			{
				NPC npc4 = base.npc;
				npc4.velocity.X = npc4.velocity.X * 0.98f;
				NPC npc5 = base.npc;
				npc5.velocity.Y = npc5.velocity.Y * 0.98f;
				Vector2 vector3;
				vector3..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float num8 = (float)Math.Atan2((double)(vector3.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector3.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
				base.npc.velocity.X = (float)(Math.Cos((double)num8) * 12.0) * -1f;
				base.npc.velocity.Y = (float)(Math.Sin((double)num8) * 12.0) * -1f;
				base.npc.ai[0] %= 6.2831855f;
				new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
				base.npc.ai[2] = -300f;
				Color color2 = default(Color);
				Rectangle rectangle2;
				rectangle2..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
				int num9 = 30;
				for (int j = 1; j <= num9; j++)
				{
					int num10 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 107, 0f, 0f, 100, color2, 2.5f);
					Main.dust[num10].noGravity = false;
				}
				return;
			}
			base.npc.ai[1] += 0f;
			if (base.npc.life <= 3000)
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 20f)
			{
				NPC npc6 = base.npc;
				npc6.velocity.X = npc6.velocity.X * 5.98f;
				NPC npc7 = base.npc;
				npc7.velocity.Y = npc7.velocity.Y * 5.98f;
				Vector2 vector4;
				vector4..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float num11 = (float)Math.Atan2((double)(vector4.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector4.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
				base.npc.velocity.X = (float)(Math.Cos((double)num11) * 12.0) * -1f;
				base.npc.velocity.Y = (float)(Math.Sin((double)num11) * 12.0) * -1f;
				base.npc.ai[0] %= 6.2831855f;
				new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
				base.npc.ai[2] = -300f;
				Color color3 = default(Color);
				Rectangle rectangle3;
				rectangle3..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
				int num12 = 30;
				for (int k = 1; k <= num12; k++)
				{
					int num13 = Dust.NewDust(base.npc.position, rectangle3.Width, rectangle3.Height, 107, 0f, 0f, 100, color3, 2.5f);
					Main.dust[num13].noGravity = false;
				}
				return;
			}
			base.npc.ai[1] += 0f;
			if (base.npc.life <= 2000)
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 20f)
			{
				NPC npc8 = base.npc;
				npc8.velocity.X = npc8.velocity.X * 10.98f;
				NPC npc9 = base.npc;
				npc9.velocity.Y = npc9.velocity.Y * 10.98f;
				Vector2 vector5;
				vector5..ctor(base.npc.position.X + (float)base.npc.width * 1.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float num14 = (float)Math.Atan2((double)(vector5.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector5.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
				base.npc.velocity.X = (float)(Math.Cos((double)num14) * 12.0) * -1f;
				base.npc.velocity.Y = (float)(Math.Sin((double)num14) * 12.0) * -1f;
				base.npc.ai[0] %= 6.2831855f;
				new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
				base.npc.ai[2] = -300f;
				Color color4 = default(Color);
				Rectangle rectangle4;
				rectangle4..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
				int num15 = 30;
				for (int l = 1; l <= num15; l++)
				{
					int num16 = Dust.NewDust(base.npc.position, rectangle4.Width, rectangle4.Height, 107, 0f, 0f, 100, color4, 2.5f);
					Main.dust[num16].noGravity = false;
				}
				return;
			}
			base.npc.ai[1] += 0f;
			if (base.npc.life <= 800)
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 20f)
			{
				NPC npc10 = base.npc;
				npc10.velocity.X = npc10.velocity.X * 30.98f;
				NPC npc11 = base.npc;
				npc11.velocity.Y = npc11.velocity.Y * 30.98f;
				Vector2 vector6;
				vector6..ctor(base.npc.position.X + (float)base.npc.width * 4.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float num17 = (float)Math.Atan2((double)(vector6.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector6.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 1.5f)));
				base.npc.velocity.X = (float)(Math.Cos((double)num17) * 12.0) * -1f;
				base.npc.velocity.Y = (float)(Math.Sin((double)num17) * 12.0) * -1f;
				base.npc.ai[0] %= 6.2831855f;
				new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
				base.npc.ai[2] = -300f;
				Color color5 = default(Color);
				Rectangle rectangle5;
				rectangle5..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
				int num18 = 50;
				for (int m = 1; m <= num18; m++)
				{
					int num19 = Dust.NewDust(base.npc.position, rectangle5.Width, rectangle5.Height, 107, 0f, 0f, 100, color5, 2.5f);
					Main.dust[num19].noGravity = false;
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff2"), Main.rand.Next(250, 500), true);
			}
		}

		private Player player;

		public int specialAttack;
	}
}
