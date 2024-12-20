using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Critters;
using Redemption.Items.Placeable.Banners;
using Redemption.Items.Usable.Potions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Critters
{
	public class Chicken : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chicken");
			Main.npcFrameCount[base.npc.type] = 7;
			NPCID.Sets.TownCritter[base.npc.type] = true;
		}

		public override void SetDefaults()
		{
			base.npc.width = 28;
			base.npc.height = 24;
			base.npc.defense = 0;
			base.npc.lifeMax = 5;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.5f;
			base.npc.friendly = true;
			base.npc.npcSlots = 0f;
			base.npc.aiStyle = 7;
			this.aiType = 46;
			this.animationType = 46;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<ChickenBanner>();
			base.npc.catchItem = (short)ModContent.ItemType<ChickenItem>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/ChickenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/ChickenGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/ChickenGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/ChickenGore3"), 1f);
				if (base.npc.FindBuffIndex(24) != -1)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<FriedChicken>(), 1, false, 0, false, false);
				}
			}
		}

		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return new bool?(true);
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return new bool?(true);
		}

		public override void AI()
		{
			if (!this.change)
			{
				int changeChoice = Main.rand.Next(1000);
				if (changeChoice == 0)
				{
					base.npc.SetDefaults(ModContent.NPCType<LongChicken>(), -1f);
					this.change = true;
				}
				if (changeChoice >= 1 && changeChoice < 4)
				{
					base.npc.SetDefaults(ModContent.NPCType<ChickenGold>(), -1f);
					this.change = true;
				}
				if (changeChoice >= 4 && changeChoice < 8)
				{
					base.npc.SetDefaults(ModContent.NPCType<VlitchChicken>(), -1f);
					this.change = true;
				}
				if (changeChoice >= 8)
				{
					int num = Main.rand.Next(5);
					if (num == 0)
					{
						base.npc.SetDefaults(ModContent.NPCType<LeghornChicken>(), -1f);
						this.change = true;
					}
					if (num == 1)
					{
						base.npc.SetDefaults(ModContent.NPCType<RedChicken>(), -1f);
						this.change = true;
					}
					if (num >= 2)
					{
						this.change = true;
					}
				}
			}
			if (this.peckPeck)
			{
				this.peckCounter++;
				if (this.peckCounter > 5)
				{
					this.peckFrame++;
					this.peckCounter = 0;
				}
				if (this.peckFrame >= 6)
				{
					this.peckFrame = 0;
				}
			}
			if (Main.rand.Next(500) == 0 && !this.peckPeck)
			{
				this.peckPeck = true;
			}
			if (this.peckPeck)
			{
				base.npc.velocity.X = 0f;
				this.peckTimer++;
				if (this.peckTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.LightGoldenrodYellow, "Peck Peck", true, true);
				}
				if (this.peckTimer >= 30)
				{
					this.peckPeck = false;
					this.peckCounter = 0;
					this.peckFrame = 0;
					this.peckTimer = 0;
				}
			}
			if (Main.rand.Next(500) == 0 && !this.cluckCluck)
			{
				this.cluckCluck = true;
			}
			if (this.cluckCluck)
			{
				this.cluckTimer++;
				if (this.cluckTimer == 1)
				{
					int num2 = Main.rand.Next(3);
					if (num2 == 0 && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck1").WithVolume(0.5f).WithPitchVariance(0.1f), base.npc.position);
					}
					if (num2 == 1 && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck2").WithVolume(0.5f).WithPitchVariance(0.1f), base.npc.position);
					}
					if (num2 == 2 && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck3").WithVolume(0.5f).WithPitchVariance(0.1f), base.npc.position);
					}
				}
				if (this.cluckTimer >= 2)
				{
					this.cluckCluck = false;
					this.cluckTimer = 0;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (RedeWorld.downedKingChicken)
			{
				return SpawnCondition.OverworldDayGrassCritter.Chance * 1.8f;
			}
			return SpawnCondition.OverworldDayGrassCritter.Chance * 3.2f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D peckAni = base.mod.GetTexture("NPCs/Critters/ChickenPeck");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.peckPeck)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.peckPeck)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = peckAni.Height / 6;
				int y6 = num214 * this.peckFrame;
				Main.spriteBatch.Draw(peckAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, peckAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)peckAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		private bool peckPeck;

		private int peckFrame;

		private int peckCounter;

		private int peckTimer;

		private bool cluckCluck;

		private int cluckTimer;

		private bool change;
	}
}
