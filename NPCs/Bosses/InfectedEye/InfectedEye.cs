using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.InfectedEye
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
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
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
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("InfectousJavelin"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Xenomite"), Main.rand.Next(4, 6), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AntiXenomiteApplier"), Main.rand.Next(2, 6), false, 0, false, false);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			if (!RedeWorld.downedInfectedEye)
			{
				CombatText.NewText(this.player.getRect(), Color.Gray, "+0", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active)
					{
						for (int j = 0; j < player.inventory.Length; j++)
						{
							if (player.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> Oh... That's only made the Infection worse... Well, it doesn't matter, at least Cthulhu's other eye is no more!", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedInfectedEye = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.579f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			if (Config.classicRedeIE)
			{
				this.oldCounter++;
				if (this.oldCounter > 20)
				{
					this.oldFrame++;
					this.oldCounter = 0;
				}
				if (this.oldFrame >= 3)
				{
					this.oldFrame = 0;
				}
			}
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
			base.npc.ai[3] += 1f;
			if (base.npc.ai[3] == 100f)
			{
				float num = 12f;
				Vector2 vector;
				vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num2 = 36;
				int num3 = base.mod.ProjectileType("ToxicSludge2");
				float num4 = (float)Math.Atan2((double)(vector.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector.X - (this.player.position.X + (float)this.player.width * 0.5f)));
				int num5 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), num3, num2, 0f, 0, 0f, 0f);
				Main.projectile[num5].netUpdate = true;
				int num6 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), num3, num2, 0f, 0, 0f, 0f);
				Main.projectile[num6].netUpdate = true;
				int num7 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), num3, num2, 0f, 0, 0f, 0f);
				Main.projectile[num7].netUpdate = true;
				if (Main.expertMode)
				{
					int num8 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), num3, num2, 0f, 0, 0f, 0f);
					Main.projectile[num8].netUpdate = true;
					int num9 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num4) * (double)num * -1.0) + (float)Main.rand.Next(-2, 2), num3, num2, 0f, 0, 0f, 0f);
					Main.projectile[num9].netUpdate = true;
				}
			}
			if (base.npc.ai[3] == 160f || base.npc.ai[3] == 220f)
			{
				float num10 = 18f;
				Vector2 vector2;
				vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num11 = 34;
				int num12 = base.mod.ProjectileType("XenomiteShot2");
				float num13 = (float)Math.Atan2((double)(vector2.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector2.X - (this.player.position.X + (float)this.player.width * 0.5f)));
				int num14 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), num12, num11, 0f, 0, 0f, 0f);
				int num15 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), num12, num11, 0f, 0, 0f, 0f);
				int num16 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), num12, num11, 0f, 0, 0f, 0f);
				int num17 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), num12, num11, 0f, 0, 0f, 0f);
				int num18 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num13) * (double)num10 * -1.0) + (float)Main.rand.Next(-2, 2), num12, num11, 0f, 0, 0f, 0f);
				Main.projectile[num14].netUpdate = true;
				Main.projectile[num15].netUpdate = true;
				Main.projectile[num16].netUpdate = true;
				Main.projectile[num17].netUpdate = true;
				Main.projectile[num18].netUpdate = true;
			}
			if (Main.expertMode)
			{
				if (base.npc.ai[3] == 300f || base.npc.ai[3] == 340f || base.npc.ai[3] == 380f || base.npc.ai[3] == 420f || base.npc.ai[3] == 460f)
				{
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num19 = (float)Math.Atan2((double)(vector3.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector3.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num19) * 12.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num19) * 12.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					base.npc.ai[2] = -300f;
					Color color = default(Color);
					Rectangle rectangle;
					rectangle..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num20 = 30;
					for (int i = 1; i <= num20; i++)
					{
						int num21 = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 107, 0f, 0f, 100, color, 2.5f);
						Main.dust[num21].noGravity = false;
					}
					return;
				}
			}
			else if (base.npc.ai[3] == 300f || base.npc.ai[3] == 360f || base.npc.ai[3] == 440f)
			{
				Vector2 vector4;
				vector4..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float num22 = (float)Math.Atan2((double)(vector4.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector4.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
				base.npc.velocity.X = (float)(Math.Cos((double)num22) * 12.0) * -1f;
				base.npc.velocity.Y = (float)(Math.Sin((double)num22) * 12.0) * -1f;
				base.npc.ai[0] %= 6.2831855f;
				new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
				base.npc.ai[2] = -300f;
				Color color2 = default(Color);
				Rectangle rectangle2;
				rectangle2..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
				int num23 = 30;
				for (int j = 1; j <= num23; j++)
				{
					int num24 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 107, 0f, 0f, 100, color2, 2.5f);
					Main.dust[num24].noGravity = false;
				}
				return;
			}
			if (base.npc.ai[3] == 500f || base.npc.ai[3] == 550f || base.npc.ai[3] == 600f || base.npc.ai[3] == 650f || base.npc.ai[3] == 700f)
			{
				float num25 = 15f;
				Vector2 vector5;
				vector5..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num26 = 36;
				int num27 = base.mod.ProjectileType("InfectedSpray");
				float num28 = (float)Math.Atan2((double)(vector5.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector5.X - (this.player.position.X + (float)this.player.width * 0.5f)));
				int num29 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num28) * (double)num25 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)num28) * (double)num25 * -1.0) + (float)Main.rand.Next(-2, 2), num27, num26, 0f, 0, 0f, 0f);
				Main.projectile[num29].netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
			{
				if (Main.expertMode)
				{
					if (base.npc.ai[3] >= 760f && base.npc.ai[3] < 840f && Main.rand.Next(10) == 0)
					{
						int num30 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12), base.mod.ProjectileType("XenomiteShot2"), 35, 1f, 255, 0f, 0f);
						Main.projectile[num30].netUpdate = true;
					}
					if (base.npc.ai[3] >= 900f)
					{
						base.npc.ai[3] = 0f;
					}
					if (Main.rand.Next(30) == 0)
					{
						int num31 = Projectile.NewProjectile(base.npc.Center.X + (float)Main.rand.Next(-80, 80), base.npc.Center.Y + (float)Main.rand.Next(-60, 60), 0f, 0f, base.mod.ProjectileType("DribblingOoze"), 30, 1f, 255, 0f, 0f);
						Main.projectile[num31].netUpdate = true;
					}
				}
				else if (base.npc.ai[3] >= 700f)
				{
					base.npc.ai[3] = 0f;
				}
			}
			else if (base.npc.ai[3] >= 700f)
			{
				base.npc.ai[3] = 0f;
			}
			if (base.npc.ai[0] % 200f == 3f && NPC.CountNPCS(base.mod.NPCType("XenomiteEye")) <= 3)
			{
				int num32 = NPC.NewNPC((int)base.npc.position.X + 80, (int)base.npc.position.Y + 80, base.mod.NPCType("XenomiteEye"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num32].netUpdate = true;
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

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/InfectedEye/InfectedEye_OLD");
			int spriteDirection = base.npc.spriteDirection;
			if (Config.classicRedeIE)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 3;
				int num2 = num * this.oldFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			else
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
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

		private int oldFrame;

		private int oldCounter;
	}
}
