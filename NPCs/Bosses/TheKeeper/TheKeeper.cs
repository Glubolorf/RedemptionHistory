using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items;
using Redemption.Items.Armor;
using Redemption.Items.Placeable;
using Redemption.Items.Weapons;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	[AutoloadBossHead]
	public class TheKeeper : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 3500;
			base.npc.damage = 30;
			base.npc.defense = 10;
			base.npc.knockBackResist = 0f;
			base.npc.width = 106;
			base.npc.height = 140;
			base.npc.value = (float)Item.buyPrice(0, 1, 50, 0);
			base.npc.npcSlots = 1f;
			base.npc.boss = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.alpha = 255;
			base.npc.dontTakeDamage = true;
			base.npc.netAlways = true;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			this.bossBag = ModContent.ItemType<TheKeeperBag>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 100; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[dustIndex].velocity *= 2.6f;
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 188;
			if (!RedeWorld.downedTheKeeper)
			{
				RedeWorld.redemptionPoints++;
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == ModContent.ItemType<RedemptionTeller>())
							{
								Main.NewText("<Chalice of Alignment> An undead... disgusting. Good thing you killed it.", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gold, "+1", true, false);
					}
				}
			}
			RedeWorld.downedTheKeeper = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<TheKeeperTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<OldGathicWaraxe>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<TheKeeperMask>(), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(5);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersBow>(), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersStaff>(), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersClaw>(), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersKnife>(), 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersSummon>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<DarkShard>(), Main.rand.Next(2, 3), false, 0, false, false);
		}

		public override void AI()
		{
			if (!this.title)
			{
				Redemption.ShowTitle(base.npc, 13);
				this.title = true;
			}
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 142;
				if (base.npc.frame.Y > 710)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.ai[3] == 1f)
			{
				this.frameCounters++;
				if (this.frameCounters > 5)
				{
					this.shriekFrame++;
					this.frameCounters = 0;
				}
				if (this.shriekFrame >= 3)
				{
					this.shriekFrame = 1;
				}
			}
			if (base.npc.ai[3] == 2f)
			{
				this.frameCounters++;
				if (this.frameCounters > 5)
				{
					this.slashFrame++;
					this.frameCounters = 0;
				}
				if (this.slashFrame >= 8)
				{
					this.slashFrame = 0;
					base.npc.ai[3] = 0f;
				}
			}
			Vector2 DefaultPos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 240f) : (player.Center.X + 240f), player.Center.Y - 50f);
			Vector2 LowPos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 240f) : (player.Center.X + 240f), player.Center.Y + 50f);
			Vector2 CenterPos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 140f) : (player.Center.X + 140f), player.Center.Y);
			Vector2 FarPos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 320f) : (player.Center.X + 320f), player.Center.Y - 25f);
			this.DespawnHandler();
			for (int i = 0; i < 255; i++)
			{
				Player player2 = Main.player[i];
				if (player2.active)
				{
					for (int j = 0; j < player2.inventory.Length; j++)
					{
						if (player2.inventory[j].type == ModContent.ItemType<AbandonedTeddy>())
						{
							this.teddy = true;
						}
					}
				}
			}
			if (base.npc.ai[0] != 0f)
			{
				base.npc.dontTakeDamage = false;
			}
			if (this.unveiled || base.npc.life > (int)((float)base.npc.lifeMax * 0.5f))
			{
				if (base.npc.ai[0] == 0f)
				{
					base.npc.alpha -= 3;
					if (base.npc.alpha <= 0)
					{
						if (this.teddy)
						{
							int p = Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 24f, 0f, 0f, ModContent.ProjectileType<VeilFX>(), 0, 0f, 255, 0f, 0f);
							Main.projectile[p].netUpdate = true;
							this.frameCounters = 0;
							base.npc.ai[0] = 5f;
						}
						else
						{
							base.npc.ai[0] = 1f;
						}
						base.npc.dontTakeDamage = false;
						base.npc.netUpdate = true;
					}
				}
				else if (base.npc.ai[0] == 1f)
				{
					this.frameCounters = 0;
					base.npc.ai[3] = 0f;
					base.npc.ai[0] = 2f;
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = (float)Main.rand.Next(6);
					base.npc.netUpdate = true;
				}
				else if (base.npc.ai[0] == 2f)
				{
					switch ((int)base.npc.ai[1])
					{
					case 0:
						this.MoveToVector2(DefaultPos);
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] % 50f == 0f)
						{
							float Speed = 6f;
							Vector2 vector8 = new Vector2(base.npc.position.X + (float)Main.rand.Next(0, base.npc.width), base.npc.position.Y + (float)Main.rand.Next(0, base.npc.height));
							float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
							int p2 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), ModContent.ProjectileType<ShadowBolt>(), 10, 3f, Main.myPlayer, 0f, 0f);
							Main.projectile[p2].netUpdate = true;
						}
						if (base.npc.ai[2] >= 200f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 1f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
						break;
					case 1:
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 70f)
						{
							this.MoveToVector2(CenterPos);
						}
						if (base.npc.ai[2] == 70f)
						{
							base.npc.velocity *= 0f;
							base.npc.ai[3] = 2f;
						}
						if (base.npc.ai[2] == 90f)
						{
							Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
							if (player.Center.X > base.npc.Center.X)
							{
								int p3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 25f, 0f, ModContent.ProjectileType<ReaperSlashPro>(), 11, 3f, 255, 0f, 0f);
								Main.projectile[p3].netUpdate = true;
								NPC npc2 = base.npc;
								npc2.velocity.X = npc2.velocity.X + 24f;
							}
							else
							{
								int p4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, -25f, 0f, ModContent.ProjectileType<ReaperSlashPro>(), 11, 3f, 255, 0f, 0f);
								Main.projectile[p4].netUpdate = true;
								NPC npc3 = base.npc;
								npc3.velocity.X = npc3.velocity.X - 24f;
							}
						}
						if (base.npc.ai[2] == 100f)
						{
							base.npc.velocity *= 0f;
						}
						if (base.npc.ai[2] > 120f)
						{
							this.MoveToVector2(DefaultPos);
						}
						if (base.npc.ai[2] >= 140f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 1f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
						break;
					case 2:
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 60f)
						{
							this.MoveToVector2(FarPos);
						}
						else
						{
							base.npc.velocity *= 0f;
						}
						if (base.npc.ai[2] == 80f)
						{
							player.ApplyDamageToNPC(base.npc, 60, 0f, 0, false);
							Main.PlaySound(SoundID.NPCDeath19, (int)base.npc.position.X, (int)base.npc.position.Y);
							for (int k = 0; k < 6; k++)
							{
								float Speed2 = Utils.NextFloat(Main.rand, 6f, 11f);
								Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
								float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
								int p5 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + Utils.NextFloat(Main.rand, -3f, 3f), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + Utils.NextFloat(Main.rand, -3f, 3f), ModContent.ProjectileType<BloodwavePro>(), 10, 3f, Main.myPlayer, 0f, 0f);
								Main.projectile[p5].netUpdate = true;
							}
							for (int l = 0; l < 30; l++)
							{
								int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 2.5f);
								Main.dust[dustIndex].velocity *= 1f;
							}
						}
						if (base.npc.ai[2] >= 110f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 1f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
						break;
					case 3:
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 80f)
						{
							this.MoveToVector2(CenterPos);
							for (int m = 0; m < 2; m++)
							{
								Dust dust2 = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 20, 0f, 0f, 100, default(Color), 1f);
								dust2.velocity = -base.npc.DirectionTo(dust2.position);
							}
						}
						if (base.npc.ai[2] == 80f)
						{
							Main.PlaySound(SoundID.NPCDeath52, (int)base.npc.position.X, (int)base.npc.position.Y);
							int p6 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)((player.Center.X > base.npc.Center.X) ? 14 : -14), 0f, ModContent.ProjectileType<SoulChargePro>(), 13, 3f, 255, 0f, 0f);
							Main.projectile[p6].netUpdate = true;
							base.npc.velocity *= 0f;
						}
						if (base.npc.ai[2] >= 120f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 1f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
						break;
					case 4:
						if (this.unveiled)
						{
							this.MoveToVector2(LowPos);
							base.npc.ai[2] += 1f;
							if (base.npc.ai[2] % 40f == 0f)
							{
								float Speed3 = 7f;
								Vector2 vector10 = new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 26f);
								float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
								int p7 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), ModContent.ProjectileType<DreadCoil>(), 11, 3f, Main.myPlayer, 0f, 0f);
								Main.projectile[p7].netUpdate = true;
							}
							if (base.npc.ai[2] >= 130f)
							{
								base.npc.ai[3] = 0f;
								base.npc.ai[0] = 1f;
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
							}
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(6);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
						break;
					case 5:
						if (this.unveiled)
						{
							base.npc.ai[2] += 1f;
							if (base.npc.ai[2] > 80f)
							{
								base.npc.ai[3] = 1f;
								base.npc.velocity *= 0f;
							}
							else
							{
								this.MoveToVector2(DefaultPos);
							}
							if (base.npc.ai[2] == 90f)
							{
								for (int n = 0; n < 30; n++)
								{
									int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 2.5f);
									Main.dust[dustIndex2].velocity *= 1f;
								}
								player.ApplyDamageToNPC(base.npc, 120, 0f, 0, false);
								for (int i2 = 0; i2 < 15; i2++)
								{
									Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), ModContent.ProjectileType<Blood>(), 11, 3f, 255, 0f, 0f);
								}
							}
							if (base.npc.ai[2] >= 120f)
							{
								base.npc.ai[3] = 0f;
								base.npc.ai[0] = 1f;
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
							}
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(6);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
						break;
					}
				}
			}
			else if (base.npc.ai[0] != 3f)
			{
				base.npc.velocity *= 0f;
				base.npc.ai[0] = 3f;
				base.npc.ai[3] = 1f;
				base.npc.netUpdate = true;
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Shriek").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
				}
				int p8 = Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 24f, 0f, 0f, ModContent.ProjectileType<VeilFX>(), 0, 0f, 255, 0f, 0f);
				Main.projectile[p8].netUpdate = true;
			}
			else
			{
				base.npc.dontTakeDamage = true;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] % 30f == 0f)
				{
					for (int i3 = -16; i3 <= 16; i3++)
					{
						Projectile.NewProjectile(base.npc.Center, 14f * Utils.RotatedBy(Vector2.UnitX, 0.19634954084936207 * (double)i3, default(Vector2)), ModContent.ProjectileType<ShriekWave>(), 0, 0f, 0, (float)base.npc.whoAmI, 0f);
					}
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] > 220f)
				{
					base.npc.dontTakeDamage = false;
					base.npc.netUpdate = true;
					this.unveiled = true;
					base.npc.ai[3] = 0f;
					base.npc.ai[0] = 1f;
					base.npc.ai[2] = 0f;
				}
			}
			if (base.npc.ai[0] == 5f)
			{
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
				this.unveiled = true;
				base.npc.velocity *= 0f;
				base.npc.dontTakeDamage = true;
				base.npc.netUpdate = true;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] < 120f)
				{
					base.npc.ai[3] = 3f;
					this.frameCounters++;
					if (this.frameCounters > 5)
					{
						this.closureFrame++;
						this.frameCounters = 0;
					}
					if (this.closureFrame >= 3)
					{
						this.closureFrame = 0;
					}
				}
				if (base.npc.ai[2] == 60f)
				{
					string text = "The Keeper noticed the abandoned teddy you're holding...";
					Color rarityPurple = Colors.RarityPurple;
					byte r = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text, r, g, rarityPurple.B, false);
				}
				if (base.npc.ai[2] >= 120f && base.npc.ai[2] < 400f)
				{
					base.npc.ai[3] = 3f;
					this.frameCounters++;
					if (this.frameCounters > 5)
					{
						this.closureFrame++;
						this.frameCounters = 0;
					}
					if (this.closureFrame >= 9)
					{
						this.closureFrame = 6;
					}
				}
				if (base.npc.ai[2] == 320f)
				{
					string text2 = "She starts to remember something...";
					Color rarityPurple = Colors.RarityPurple;
					byte r2 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g2 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text2, r2, g2, rarityPurple.B, false);
				}
				if (base.npc.ai[2] == 400f)
				{
					this.frameCounters = 0;
					this.closureFrame = 0;
				}
				if (base.npc.ai[2] >= 400f && base.npc.ai[2] < 800f)
				{
					base.npc.ai[3] = 4f;
					this.frameCounters++;
					if (this.frameCounters > 5)
					{
						this.closureFrame++;
						this.frameCounters = 0;
					}
					if (this.closureFrame >= 6)
					{
						this.closureFrame = 3;
					}
				}
				if (base.npc.ai[2] == 540f)
				{
					base.npc.netUpdate = true;
					string text3 = "Pain... Anger... Sadness... All those feelings were washed away...";
					Color rarityPurple = Colors.RarityPurple;
					byte r3 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g3 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text3, r3, g3, rarityPurple.B, false);
				}
				if (base.npc.ai[2] == 750f)
				{
					string text4 = "She only feels... at peace...";
					Color rarityPurple = Colors.RarityPurple;
					byte r4 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g4 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text4, r4, g4, rarityPurple.B, false);
				}
				if (base.npc.ai[2] >= 800f)
				{
					base.npc.ai[3] = 4f;
					this.frameCounters++;
					if (this.frameCounters > 5)
					{
						this.closureFrame++;
						this.frameCounters = 0;
					}
					if (this.closureFrame >= 16)
					{
						this.closureFrame = 14;
					}
				}
				if (base.npc.ai[2] >= 800f)
				{
					base.npc.alpha++;
					if (Main.rand.Next(5) == 0)
					{
						Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 20, 0f, 0f, 0, default(Color), 1f);
					}
				}
				if (base.npc.ai[2] == 840f)
				{
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Thank...", true, false);
				}
				if (base.npc.ai[2] == 900f)
				{
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "You...", true, false);
				}
				if (base.npc.ai[2] >= 900f)
				{
					for (int k2 = 0; k2 < 1; k2++)
					{
						double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
						this.vector.X = (float)(Math.Sin(angle) * 100.0);
						this.vector.Y = (float)(Math.Cos(angle) * 100.0);
						Dust dust3 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 3f)];
						dust3.noGravity = true;
						dust3.velocity = -base.npc.DirectionTo(dust3.position) * 10f;
					}
				}
				if (base.npc.alpha >= 255)
				{
					for (int i4 = 0; i4 < 50; i4++)
					{
						int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 20, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[dustIndex3].velocity *= 2.6f;
					}
					for (int i5 = 0; i5 < 50; i5++)
					{
						int dustIndex4 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 3f);
						Main.dust[dustIndex4].velocity *= 2.6f;
					}
					string text5 = "The Keeper's Spirit fades away... ?";
					Color rarityPurple = Colors.RarityPurple;
					byte r5 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g5 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text5, r5, g5, rarityPurple.B, false);
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeeperAcc>(), 1, false, 0, false, false);
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<TheKeeperTrophy>(), 1, false, 0, false, false);
					int p9 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<KeeperSoul>(), 0, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[p9].netUpdate = true;
					if (!RedeWorld.keeperSaved)
					{
						RedeWorld.redemptionPoints += 3;
						CombatText.NewText(player.getRect(), Color.Gold, "+3", true, false);
						for (int k3 = 0; k3 < 255; k3++)
						{
							Player player3 = Main.player[k3];
							if (player3.active)
							{
								for (int j2 = 0; j2 < player3.inventory.Length; j2++)
								{
									if (player3.inventory[j2].type == ModContent.ItemType<RedemptionTeller>())
									{
										Main.NewText("<Chalice of Alignment> You've redeemed yourself, Octavia may rest in undisturbed peace... Hm, Strange, something feels off... I'm sure it's nothing.", Color.DarkGoldenrod, false);
									}
								}
							}
						}
					}
					base.npc.netUpdate = true;
					RedeWorld.keeperSaved = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					base.npc.active = false;
				}
			}
			Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 0, default(Color), 1f);
		}

		private void DespawnHandler()
		{
			Player player = Main.player[base.npc.target];
			if (!player.active || player.dead)
			{
				base.npc.alpha -= 5;
				if (base.npc.timeLeft > 10)
				{
					base.npc.timeLeft = 10;
				}
				return;
			}
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = (base.npc.ai[1] == 3f || base.npc.ai[1] == 5f) ? 2f : (this.unveiled ? 12f : 6f);
			float velMultiplier = 1f;
			Vector2 dist = p - base.npc.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			base.npc.velocity *= moveSpeed;
			base.npc.velocity *= velMultiplier;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/TheKeeper/TheKeeper_Glow");
			Texture2D veilAni = base.mod.GetTexture("NPCs/Bosses/TheKeeper/TheKeeper_Veil");
			Texture2D shriekAni = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_Scream");
			Texture2D shriekGlow = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_Scream_Glow");
			Texture2D slashAni = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_ReaperSlash");
			Texture2D slashGlow = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_ReaperSlash_Glow");
			Texture2D closure1Ani = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_Closure1");
			Texture2D closure1Glow = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_Closure1_Glow");
			Texture2D closure2Ani = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_Closure2");
			Texture2D closure2Glow = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_Closure2_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			if (base.npc.ai[3] == 0f)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			if (base.npc.ai[3] == 1f)
			{
				int num214 = shriekAni.Height / 3;
				int y6 = num214 * this.shriekFrame;
				Main.spriteBatch.Draw(shriekAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, shriekAni.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)shriekAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(shriekGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, shriekGlow.Width, num214)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)shriekGlow.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.ai[3] == 2f)
			{
				int num215 = slashAni.Height / 8;
				int y7 = num215 * this.slashFrame;
				Main.spriteBatch.Draw(slashAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, slashAni.Width, num215)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)slashAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(slashGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, slashGlow.Width, num215)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)slashGlow.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.ai[3] == 3f)
			{
				int num216 = closure1Ani.Height / 9;
				int y8 = num216 * this.closureFrame;
				Main.spriteBatch.Draw(closure1Ani, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, closure1Ani.Width, num216)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)closure1Ani.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(closure1Glow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, closure1Ani.Width, num216)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)closure1Ani.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.ai[3] == 4f)
			{
				int num217 = closure2Ani.Height / 16;
				int y9 = num217 * this.closureFrame;
				Main.spriteBatch.Draw(closure2Ani, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, closure2Ani.Width, num217)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)closure2Ani.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(closure2Glow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, closure2Ani.Width, num217)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)closure2Ani.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (!this.unveiled && base.npc.life > (int)((float)base.npc.lifeMax * 0.5f))
			{
				Main.spriteBatch.Draw(veilAni, drawCenter - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public Player player;

		public Vector2 MoveVector2;

		private Vector2 vector;

		public int shriekFrame;

		public bool teddy;

		public bool unveiled;

		public int frameCounters;

		public int slashFrame;

		public int closureFrame;

		private bool title;
	}
}
