using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class MACEProjectJawOffA : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/LabNPCs/New/MACEProjectJawA";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("MACE Project");
		}

		public override void SetDefaults()
		{
			base.npc.width = 68;
			base.npc.height = 80;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.noGravity = true;
			base.npc.noTileCollide = false;
			base.npc.dontTakeDamage = true;
			base.npc.npcSlots = 0f;
		}

		public override void AI()
		{
			if (RedeWorld.maceUS)
			{
				base.npc.SetDefaults(ModContent.NPCType<MACEProjectJawA>(), -1f);
				return;
			}
			base.npc.timeLeft = 10;
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/LabNPCs/New/MACEProjectJawA_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
	}
}
