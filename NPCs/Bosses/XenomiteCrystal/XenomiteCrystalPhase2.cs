using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.XenomiteCrystal
{
	[AutoloadBossHead]
	public class XenomiteCrystalPhase2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Crystal");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 54;
			base.npc.height = 50;
			base.npc.friendly = false;
			base.npc.damage = 32;
			base.npc.defense = 0;
			base.npc.lifeMax = 2000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 2, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 85;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.boss = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossXeno1");
			base.npc.alpha = 50;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			this.animationType = 34;
			this.bossBag = base.mod.ItemType("XenomiteCrystalBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				for (int i = 0; i < 35; i++)
				{
					int num = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[num].velocity *= 5f;
					Main.dust[num].noGravity = true;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 188;
			if (!RedeWorld.downedXenomiteCrystal)
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
								Main.NewText("<Chalice of Alignment> You've awoken the infection now. But don't worry, I'm sure we can handle it!", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedXenomiteCrystal = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteCrystalTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteGlaive"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("InfectedMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteYoyo"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenoCanister"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(12, 22), false, 0, false, false);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			this.Move(new Vector2(0f, 0f));
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("PuriumFlame"), 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(400) == 0)
			{
				int num = NPC.NewNPC((int)base.npc.position.X + 50, (int)base.npc.position.Y + 50, base.mod.NPCType("XenomitePiece"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num].netUpdate = true;
			}
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] == 100f || base.npc.ai[1] == 150f || base.npc.ai[1] == 200f)
			{
				float num2 = 12f;
				Vector2 vector;
				vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num3 = 20;
				int num4 = base.mod.ProjectileType("ToxicSludge1");
				float num5 = (float)Math.Atan2((double)(vector.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector.X - (this.player.position.X + (float)this.player.width * 0.5f)));
				int num6 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num5) * (double)num2 * -1.0) + (float)Main.rand.Next(-1, 1), num4, num3, 0f, 0, 0f, 0f);
				int num7 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num5) * (double)num2 * -1.0) + (float)Main.rand.Next(-1, 1), num4, num3, 0f, 0, 0f, 0f);
				Main.projectile[num6].netUpdate = true;
				Main.projectile[num7].netUpdate = true;
				if (Main.rand.Next(2) == 0)
				{
					int num8 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num5) * (double)num2 * -1.0) + (float)Main.rand.Next(-1, 1), num4, num3, 0f, 0, 0f, 0f);
					Main.projectile[num8].netUpdate = true;
				}
			}
			if (Main.expertMode && base.npc.ai[1] == 250f)
			{
				float num9 = 10f;
				Vector2 vector2;
				vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num10 = 20;
				int num11 = base.mod.ProjectileType("ToxicSludge1");
				float num12 = (float)Math.Atan2((double)(vector2.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector2.X - (this.player.position.X + (float)this.player.width * 0.5f)));
				int num13 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num12) * (double)num9 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num12) * (double)num9 * -1.0) + (float)Main.rand.Next(-1, 1), num11, num10, 0f, 0, 0f, 0f);
				int num14 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num12) * (double)num9 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num12) * (double)num9 * -1.0) + (float)Main.rand.Next(-1, 1), num11, num10, 0f, 0, 0f, 0f);
				Main.projectile[num13].netUpdate = true;
				Main.projectile[num14].netUpdate = true;
				if (Main.rand.Next(2) == 0)
				{
					int num15 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num12) * (double)num9 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num12) * (double)num9 * -1.0) + (float)Main.rand.Next(-1, 1), num11, num10, 0f, 0, 0f, 0f);
					Main.projectile[num15].netUpdate = true;
				}
			}
			if ((base.npc.ai[1] >= 350f || base.npc.ai[1] <= 450f) && Main.rand.Next(10) == 0)
			{
				int num16 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-6 + Main.rand.Next(0, 11)), (float)(-6 + Main.rand.Next(0, 11)), base.mod.ProjectileType("XenomiteShot1"), 18, 3f, 255, 0f, 0f);
				Main.projectile[num16].netUpdate = true;
			}
			if (base.npc.ai[1] >= 500f)
			{
				base.npc.ai[1] = 0f;
			}
			base.npc.ai[2] += 1f;
			if (base.npc.ai[2] == 600f)
			{
				int num17 = 8;
				for (int i = 0; i < num17; i++)
				{
					int num18 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
					Main.projectile[num18].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)num17 * 6.28f);
					Main.projectile[num18].netUpdate = true;
				}
				base.npc.ai[2] = 0f;
			}
			if (base.npc.life < 800 && base.npc.ai[2] == 300f)
			{
				int num19 = 8;
				for (int j = 0; j < num19; j++)
				{
					int num20 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
					Main.projectile[num20].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)j / (float)num19 * 6.28f);
					Main.projectile[num20].netUpdate = true;
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f))
			{
				this.speed = 9f;
			}
			else
			{
				this.speed = 6f;
			}
			Vector2 vector = this.player.Center + offset;
			Vector2 vector2 = vector - base.npc.Center;
			float num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			float num2 = 17f;
			vector2 = (base.npc.velocity * num2 + vector2) / (num2 + 1f);
			num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			base.npc.velocity = vector2;
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -20f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		private Player player;

		private float speed;
	}
}
