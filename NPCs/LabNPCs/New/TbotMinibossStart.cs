using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Armor.Costumes;
using Redemption.Items.LabThings;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class TbotMinibossStart : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/LabNPCs/New/TbotMiniboss";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Protector Volt");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 38;
			base.npc.height = 70;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 140000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = false;
			base.npc.noTileCollide = false;
			base.npc.dontTakeDamage = true;
			base.npc.aiStyle = -1;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 30; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.landed);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.landed = reader.ReadBool();
			}
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
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
				npc.frame.Y = npc.frame.Y + 72;
				if (base.npc.frame.Y > 216)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (base.npc.spriteDirection == 1)
			{
				this.gunRot = 0f;
			}
			else
			{
				this.gunRot = 3.1416f;
			}
			if (RedeWorld.downedVolt)
			{
				base.npc.Transform(ModContent.NPCType<ProtectorVoltNPC>());
			}
			else
			{
				if (base.npc.Distance(Main.player[base.npc.target].Center) < 300f && base.npc.ai[0] == 0f && player.Center.X > base.npc.Center.X)
				{
					if (((BasePlayer.HasChestplate(player, ModContent.ItemType<TBotVanityChestplate>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<TBotVanityLegs>(), true)) || (BasePlayer.HasChestplate(player, ModContent.ItemType<AndroidArmour>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<AndroidPants>(), true)) || (BasePlayer.HasChestplate(player, ModContent.ItemType<JanitorOutfit>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<JanitorPants>(), true))) && (BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityEyes>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityGoggles>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<AdamHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<OperatorHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<VoltHead>(), true)))
					{
						base.npc.ai[0] = 2f;
					}
					else
					{
						base.npc.ai[0] = 1f;
					}
				}
				if (base.npc.ai[0] == 1f)
				{
					if (RedeWorld.voltBegin)
					{
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] >= 60f)
						{
							Redemption.ShowTitle(base.npc, 19);
							base.npc.Transform(ModContent.NPCType<TbotMiniboss>());
						}
					}
					else
					{
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] == 40f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Leave.", true, false);
						}
						if (base.npc.ai[1] == 120f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Deeper sectors are life-threatening for your kind.", true, false);
						}
						if (base.npc.ai[1] == 260f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "If you wish to enter, prove your strength to me.", true, false);
						}
						if (base.npc.ai[1] == 320f)
						{
							base.npc.velocity.Y = -8f;
							this.landed = false;
						}
						if (this.landed)
						{
							Mod mod = Redemption.inst;
							if (!Main.dedServ)
							{
								Main.PlaySound(mod.GetLegacySoundSlot(50, "Sounds/Custom/EarthBoom").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
							}
							Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
							colorToTile[new Color(150, 150, 150)] = -2;
							colorToTile[Color.Black] = -1;
							TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/VoltDestroy"), colorToTile, null, null, null, null, null, null);
							Point origin = new Point((int)((float)Main.maxTilesX * 0.55f), (int)((float)Main.maxTilesY * 0.65f));
							texGenerator.Generate(origin.X, origin.Y, true, true);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							}
							base.npc.ai[0] = 3f;
							base.npc.ai[1] = 0f;
						}
					}
				}
				else if (base.npc.ai[0] == 2f)
				{
					base.npc.ai[1] += 1f;
					if (base.npc.ai[1] == 40f && !RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Hm? Are you supposed to be let through?", true, false);
					}
					if (base.npc.ai[1] == 220f && !RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "One second...", true, false);
					}
					if (base.npc.ai[1] == 340f && !RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "...", true, false);
					}
					if (base.npc.ai[1] == 500f && !RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Everything seems to be in order. Move along.", true, false);
					}
					if (base.npc.ai[1] > 560f)
					{
						if (!RedeWorld.labAccess5)
						{
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ZoneAccessPanel5A>(), 1, false, 0, false, false);
						}
						RedeWorld.downedVolt = true;
						base.npc.Transform(ModContent.NPCType<ProtectorVoltNPC>());
					}
				}
			}
			if (base.npc.ai[0] >= 3f)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] >= 60f)
				{
					Redemption.ShowTitle(base.npc, 19);
					RedeWorld.voltBegin = true;
					base.npc.Transform(ModContent.NPCType<TbotMiniboss>());
				}
			}
			if (base.npc.collideY && base.npc.velocity.Y > 0f && base.npc.ai[1] >= 320f && !this.landed)
			{
				for (int j = 0; j < 40; j++)
				{
					Dust.NewDust(base.npc.BottomLeft, Main.rand.Next(base.npc.width), 1, 31, 0f, 0f, 0, default(Color), 2f);
				}
				this.landed = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/LabNPCs/New/TbotMinibossHop");
			Texture2D gunAni = base.mod.GetTexture("NPCs/LabNPCs/New/TbotMinibossGun");
			int spriteDirection = base.npc.spriteDirection;
			if (base.npc.velocity.Y != 0f)
			{
				new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				new Vector2((float)hopAni.Width * 0.5f, (float)hopAni.Height * 0.5f);
				for (int i = this.oldPos.Length - 1; i >= 0; i--)
				{
					float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(hopAni, this.oldPos[i] - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor * (0.5f * alpha), this.oldrot[i], new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
			}
			if (base.npc.velocity.Y == 0f)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.velocity.Y != 0f)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = hopAni.Height / 1;
				int y7 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, hopAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			Vector2 drawCenterG = new Vector2(base.npc.Center.X, base.npc.Center.Y + 6f);
			int numG = gunAni.Height / 4;
			int yG = 0;
			spriteBatch.Draw(gunAni, drawCenterG - Main.screenPosition, new Rectangle?(new Rectangle(0, yG, gunAni.Width, numG)), drawColor, this.gunRot, new Vector2((float)gunAni.Width / 2f, (float)numG / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
			return false;
		}

		private void DespawnHandler()
		{
			Player player = Main.player[base.npc.target];
			if (!player.active || player.dead)
			{
				base.npc.TargetClosest(false);
				player = Main.player[base.npc.target];
				if (!player.active || player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = base.npc.lifeMax;
		}

		private Vector2[] oldPos = new Vector2[3];

		private float[] oldrot = new float[3];

		private bool landed;

		public Vector2 MoveVector2;

		private float gunRot;
	}
}
