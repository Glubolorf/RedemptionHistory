using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class JanitorBotCleaning : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Janitor");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.friendly = false;
			base.npc.dontTakeDamage = true;
			base.npc.noGravity = false;
			base.npc.width = 34;
			base.npc.height = 44;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.aiStyle = -1;
			base.npc.knockBackResist = 0f;
			base.npc.npcSlots = 0f;
			base.npc.netAlways = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.frameCounter += 1.0;
			if (!this.looking)
			{
				if (base.npc.frameCounter >= 20.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 46;
					if (base.npc.frame.Y > 138)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
			}
			else
			{
				base.npc.frame.Y = 184;
			}
			base.npc.wet = false;
			base.npc.lavaWet = false;
			base.npc.honeyWet = false;
			base.npc.velocity.X = 0f;
			base.npc.dontTakeDamage = true;
			base.npc.immune[255] = 30;
			if ((NPC.CountNPCS(base.mod.NPCType("JanitorBotCleaning")) >= 2 && Main.rand.Next(2) == 0) || RedeWorld.downedJanitor)
			{
				base.npc.active = false;
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (num <= 200f && player.Center.X < base.npc.Center.X)
			{
				base.npc.ai[0] = 1f;
			}
			if (base.npc.ai[0] == 1f)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] >= 30f && base.npc.ai[1] < 120f)
				{
					this.looking = true;
				}
				else if (base.npc.ai[1] >= 240f)
				{
					this.looking = true;
				}
				else
				{
					this.looking = false;
				}
				if (base.npc.ai[1] == 30f && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Oi! Don't go there, the floor's wet.", true, false);
				}
				if (base.npc.ai[1] == 180f && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "...", false, false);
				}
				if (base.npc.ai[1] == 280f && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Wait... You're a trespasser!", true, false);
				}
				if (base.npc.ai[1] >= 400f)
				{
					base.npc.SetDefaults(base.mod.NPCType("JanitorBot"), -1f);
				}
			}
		}

		public bool looking;
	}
}
