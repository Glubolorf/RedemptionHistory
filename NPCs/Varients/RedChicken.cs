using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Varients
{
	public class RedChicken : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chicken");
			Main.npcFrameCount[base.npc.type] = 7;
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
			base.npc.npcSlots = 0f;
			base.npc.aiStyle = 7;
			this.aiType = 46;
			this.animationType = 46;
			base.npc.dontTakeDamageFromHostiles = false;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("ChickenBanner");
			base.npc.catchItem = (short)base.mod.ItemType("ChickenRedItem");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore8"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore9"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore3"), 1f);
				if (base.npc.FindBuffIndex(24) != -1)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FriedChicken"), 1, false, 0, false, false);
				}
			}
		}

		public override void AI()
		{
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
				if (this.peckTimer == 1 && !Config.NoCombatText)
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
					int num = Main.rand.Next(3);
					if (num == 0 && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck1").WithVolume(0.5f).WithPitchVariance(0.1f), base.npc.position);
					}
					if (num == 1 && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck2").WithVolume(0.5f).WithPitchVariance(0.1f), base.npc.position);
					}
					if (num == 2 && !Main.dedServ)
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

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Varients/RedChickenPeck");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.peckPeck)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.peckPeck)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 6;
				int num2 = num * this.peckFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
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
