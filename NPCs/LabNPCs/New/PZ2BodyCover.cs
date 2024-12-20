using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class PZ2BodyCover : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kari");
		}

		public override void SetDefaults()
		{
			base.npc.width = 52;
			base.npc.height = 66;
			base.npc.friendly = false;
			base.npc.damage = 110;
			base.npc.defense = 0;
			base.npc.lifeMax = 200000;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.noTileCollide = false;
			base.npc.dontTakeDamage = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/LabBossMusic2");
			this.bossBag = base.mod.ItemType("PZBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
				Gore.NewGore(new Vector2(base.npc.Center.X + (float)Main.rand.Next(-100, 100), base.npc.Center.Y + (float)Main.rand.Next(-100, 0)), base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/PZGoreEye"), 1f);
				for (int j = 0; j < 3; j++)
				{
					Gore.NewGore(new Vector2(base.npc.Center.X + (float)Main.rand.Next(-100, 100), base.npc.Center.Y + (float)Main.rand.Next(-100, 0)), base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/PZGoreFlesh"), 1f);
					Gore.NewGore(new Vector2(base.npc.Center.X + (float)Main.rand.Next(-100, 100), base.npc.Center.Y + (float)Main.rand.Next(-100, 0)), base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/PZGoreFlesh1"), 1f);
					Gore.NewGore(new Vector2(base.npc.Center.X + (float)Main.rand.Next(-100, 100), base.npc.Center.Y + (float)Main.rand.Next(-100, 0)), base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/PZGoreFlesh2"), 1f);
				}
				for (int k = 0; k < 3; k++)
				{
					Gore.NewGore(new Vector2(base.npc.Center.X + (float)Main.rand.Next(-100, 100), base.npc.Center.Y + (float)Main.rand.Next(-100, 0)), base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/PZGoreGoo1"), 1f);
					Gore.NewGore(new Vector2(base.npc.Center.X + (float)Main.rand.Next(-100, 100), base.npc.Center.Y + (float)Main.rand.Next(-100, 0)), base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/PZGoreGooEye"), 1f);
					Gore.NewGore(new Vector2(base.npc.Center.X + (float)Main.rand.Next(-100, 100), base.npc.Center.Y + (float)Main.rand.Next(-100, 0)), base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/PZGoreGoop"), 1f);
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			potionType = 58;
			if (!RedeWorld.downedPatientZero)
			{
				RedeWorld.redemptionPoints += 3;
				CombatText.NewText(player.getRect(), Color.Gold, "+3", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> We did it! We stopped the Infection! High-five! ... Oh, right.", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedPatientZero = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (!RedeWorld.labAccess7)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel7A"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("PZTrophy"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FloppyDisk7"), 1, false, 0, false, false);
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("PZMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("PZGauntlet"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("SwarmerGun"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XeniumSaber"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("MedicKit1"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("BluePrints"), 1, false, 0, false, false);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.customAI[0]);
				writer.Write(this.customAI[1]);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.customAI[0] = reader.ReadFloat();
				this.customAI[1] = reader.ReadFloat();
			}
		}

		public override void AI()
		{
			int boss = (int)base.npc.ai[0];
			if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != base.mod.NPCType("PZ2Fight"))
			{
				base.npc.active = false;
			}
			base.npc.netUpdate = true;
			NPC npc2 = Main.npc[(int)base.npc.ai[0]];
			if (base.npc.ai[1] == 0f)
			{
				this.exposed = false;
				if (npc2.life <= (int)((float)npc2.lifeMax * 0.75f))
				{
					base.npc.ai[1] += 1f;
				}
			}
			if (base.npc.ai[1] == 1f)
			{
				this.exposed = true;
				if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.75f))
				{
					this.exposed = false;
					base.npc.ai[1] += 1f;
				}
			}
			if (base.npc.ai[1] == 2f)
			{
				this.exposed = false;
				if (npc2.life <= (int)((float)npc2.lifeMax * 0.5f))
				{
					base.npc.ai[1] += 1f;
				}
			}
			if (base.npc.ai[1] == 3f)
			{
				this.exposed = true;
				if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.5f))
				{
					this.exposed = false;
					base.npc.ai[1] += 1f;
				}
			}
			if (base.npc.ai[1] == 4f)
			{
				this.exposed = false;
				if (npc2.life <= (int)((float)npc2.lifeMax * 0.35f))
				{
					base.npc.ai[1] += 1f;
				}
			}
			if (base.npc.ai[1] == 5f)
			{
				this.exposed = true;
				if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.25f))
				{
					this.exposed = false;
					base.npc.ai[1] += 1f;
				}
			}
			if (base.npc.ai[1] == 6f)
			{
				this.exposed = false;
				if (npc2.life <= (int)((float)npc2.lifeMax * 0.1f))
				{
					base.npc.ai[1] += 1f;
				}
			}
			if (base.npc.ai[1] >= 7f)
			{
				this.exposed = true;
			}
			if (this.exposed)
			{
				Player player = Main.player[base.npc.target];
				npc2.ai[3] = 1f;
				base.npc.dontTakeDamage = false;
				switch ((int)base.npc.ai[2])
				{
				case 0:
					break;
				case 1:
					base.npc.ai[3] += 1f;
					if (base.npc.ai[1] == 1f)
					{
						if (base.npc.ai[3] % 120f == 0f)
						{
							Main.PlaySound(SoundID.Item72, base.npc.position);
							for (int i = 0; i < 6; i++)
							{
								int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-5 + Main.rand.Next(0, 11)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("PoisonBeat"), 30, 3f, 255, 0f, 0f);
								Main.projectile[p].netUpdate = true;
							}
						}
					}
					else if (base.npc.ai[1] == 3f)
					{
						if (base.npc.ai[3] % 120f == 0f)
						{
							Main.PlaySound(SoundID.Item72, base.npc.position);
							for (int j = 0; j < 6; j++)
							{
								int p2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-5 + Main.rand.Next(0, 11)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("PoisonBeat"), 30, 3f, 255, 0f, 0f);
								Main.projectile[p2].netUpdate = true;
							}
							for (int k = 0; k < 4; k++)
							{
								int p3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-5 + Main.rand.Next(0, 11)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("InfectiousBeat"), 30, 3f, 255, 0f, 0f);
								Main.projectile[p3].netUpdate = true;
							}
						}
					}
					else if (base.npc.ai[1] >= 5f && base.npc.ai[3] % 100f == 0f)
					{
						Main.PlaySound(SoundID.Item72, base.npc.position);
						for (int l = 0; l < 8; l++)
						{
							int p4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-5 + Main.rand.Next(0, 11)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("PoisonBeat"), 30, 3f, 255, 0f, 0f);
							Main.projectile[p4].netUpdate = true;
						}
						for (int m = 0; m < 4; m++)
						{
							int p5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-5 + Main.rand.Next(0, 11)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("InfectiousBeat"), 30, 3f, 255, 0f, 0f);
							Main.projectile[p5].netUpdate = true;
						}
					}
					if (base.npc.ai[3] >= 500f)
					{
						base.npc.ai[2] += 1f;
						base.npc.ai[3] = 0f;
						base.npc.netUpdate = true;
						goto IL_E9A;
					}
					goto IL_E9A;
				case 2:
					if (base.npc.ai[1] < 3f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[2] = 0f;
						goto IL_E9A;
					}
					base.npc.ai[3] += 1f;
					if (base.npc.ai[3] % 60f == 0f && base.npc.ai[3] >= 120f)
					{
						Main.PlaySound(SoundID.Item103, base.npc.position);
						float Speed = 10f;
						Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage = 36;
						int type = base.mod.ProjectileType("PatientLaser5");
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), type, damage, 0f, 0, 0f, 0f);
					}
					if (base.npc.ai[3] >= 340f)
					{
						base.npc.ai[2] = 0f;
						base.npc.ai[3] = 0f;
						base.npc.netUpdate = true;
						goto IL_E9A;
					}
					goto IL_E9A;
				default:
					base.npc.ai[3] = 0f;
					break;
				}
				base.npc.ai[3] += 1f;
				if (base.npc.ai[1] == 1f)
				{
					if (base.npc.ai[3] % 5f == 0f && base.npc.ai[3] >= 120f)
					{
						Main.PlaySound(SoundID.Item20, base.npc.position);
						float Speed2 = 12f;
						Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage2 = 30;
						int type2 = base.mod.ProjectileType("TearOfInfectionBall");
						float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
						Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), type2, damage2, 0f, 0, 0f, 0f);
					}
				}
				else if (base.npc.ai[1] == 3f)
				{
					if (base.npc.ai[3] % 5f == 0f && base.npc.ai[3] >= 120f)
					{
						Main.PlaySound(SoundID.Item20, base.npc.position);
						float Speed3 = 11f;
						Vector2 vector10 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage3 = 32;
						int type3 = base.mod.ProjectileType("CausticTearBall");
						float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
						Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-2, 2), type3, damage3, 0f, 0, 0f, 0f);
					}
				}
				else if (base.npc.ai[1] >= 5f && base.npc.ai[3] % 5f == 0f && base.npc.ai[3] >= 120f)
				{
					Main.PlaySound(SoundID.Item20, base.npc.position);
					float Speed4 = 10f;
					Vector2 vector11 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage4 = 34;
					int type4 = base.mod.ProjectileType("TearOfPainBall");
					float rotation4 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0) + (float)Main.rand.Next(-2, 2), type4, damage4, 0f, 0, 0f, 0f);
				}
				if (base.npc.ai[3] >= 138f)
				{
					base.npc.ai[2] += 1f;
					base.npc.ai[3] = 0f;
					base.npc.netUpdate = true;
				}
			}
			else
			{
				npc2.ai[3] = 0f;
				base.npc.dontTakeDamage = true;
			}
			IL_E9A:
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		private int bodyFrame;

		private bool eyeOpen;

		private int eyeFrame;

		public float[] customAI = new float[2];

		private bool exposed;
	}
}
