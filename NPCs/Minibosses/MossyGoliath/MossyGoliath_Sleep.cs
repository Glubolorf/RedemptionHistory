using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Quest;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses.MossyGoliath
{
	public class MossyGoliath_Sleep : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mossy Goliath");
			Main.npcFrameCount[base.npc.type] = 7;
		}

		public override void SetDefaults()
		{
			base.npc.width = 116;
			base.npc.height = 84;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit37;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.dontTakeDamage = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 3, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2f);
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 20.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 88;
				if (base.npc.frame.Y > 528)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			base.npc.TargetClosest(true);
			float distance = base.npc.Distance(Main.player[base.npc.target].Center);
			if ((player.velocity.X > 5f || player.velocity.X < -5f) ? (distance < 400f) : (distance < 100f))
			{
				base.npc.SetDefaults(base.mod.NPCType("MossyGoliath"), -1f);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D textureZ = base.mod.GetTexture("NPCs/Minibosses/MossyGoliath/MossyGoliath_Sleep_Z");
			int spriteDirection = base.npc.spriteDirection;
			if (RedeQuests.zephosQuests == 4)
			{
				spriteBatch.Draw(textureZ, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].boss)
				{
					return 0f;
				}
			}
			return SpawnCondition.SurfaceJungle.Chance * ((!RedeQuests.ZswordQuest && !RedeWorld.downedMossyGoliath && (RedeQuests.zephosQuests == 4 || RedeQuests.daerelQuests == 4) && !NPC.AnyNPCs(base.mod.NPCType("MossyGoliath_Sleep")) && !NPC.AnyNPCs(base.mod.NPCType("MossyGoliath"))) ? 1.5f : 0f);
		}
	}
}
