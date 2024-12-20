using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Materials.PostML;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Soulless
{
	public class SmallShadesoulNPC : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Small Shadesoul");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 18;
			base.npc.height = 18;
			base.npc.damage = 1;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit36;
			base.npc.DeathSound = SoundID.NPCDeath39;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 2;
			base.npc.alpha = 60;
			base.npc.noGravity = true;
			base.npc.catchItem = (short)ModContent.ItemType<SmallShadesoul>();
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 20;
				if (base.npc.frame.Y > 60)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			base.npc.TargetClosest(true);
			Vector2 direction = Main.player[base.npc.target].Center - base.npc.Center;
			base.npc.rotation = Utils.ToRotation(direction);
			this.deathTimer++;
			if (this.deathTimer >= 1200)
			{
				Main.PlaySound(SoundID.NPCDeath39.WithVolume(0.3f), (int)base.npc.position.X, (int)base.npc.position.Y);
				base.npc.active = false;
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), Main.rand.Next(3, 7), true);
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SmallShadesoul>(), 1, false, 0, false, false);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Soulless/SmallShadesoulNPC_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		private int deathTimer;
	}
}
