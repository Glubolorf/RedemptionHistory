using System;
using Microsoft.Xna.Framework;
using Redemption.ChickenArmy;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.ChickenInvasion
{
	public class ShieldedChickenMan : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shielded Chickman");
			Main.npcFrameCount[base.npc.type] = Main.npcFrameCount[385];
		}

		public override void SetDefaults()
		{
			base.npc.width = 20;
			base.npc.height = 28;
			if (RedeWorld.downedPatientZero)
			{
				base.npc.damage = 80;
				base.npc.defense = 20;
				base.npc.lifeMax = 6500;
			}
			else
			{
				base.npc.damage = 20;
				base.npc.defense = 10;
				base.npc.lifeMax = 65;
			}
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 40);
			base.npc.knockBackResist = 0.05f;
			base.npc.aiStyle = 3;
			this.aiType = 73;
			this.animationType = 385;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ChickmanShield"), 1, false, 0, false, false);
			}
			if (RedeWorld.downedPatientZero && Main.rand.Next(1200) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ChickLauncher"), 1, false, 0, false, false);
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore1"), 1f);
				for (int i = 0; i < 7; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.4f;
				}
				if (ChickWorld.chickArmy)
				{
					ChickWorld.ChickPoints2++;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}
	}
}
