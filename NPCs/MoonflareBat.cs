using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class MoonflareBat : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Moonflare Bat");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.width = 28;
			base.npc.height = 18;
			base.npc.defense = 0;
			base.npc.damage = 5;
			base.npc.lifeMax = 15;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath4;
			base.npc.knockBackResist = 0.5f;
			base.npc.aiStyle = 14;
			this.aiType = 49;
			this.animationType = 49;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<MoonflareBatBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 262, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 162, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/MoonflareBat_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode)
			{
				return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
			}
			return SpawnCondition.OverworldNightMonster.Chance * 0.06f;
		}
	}
}
