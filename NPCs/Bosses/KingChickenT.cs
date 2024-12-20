using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class KingChickenT : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Mighty King Chicken");
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
			base.npc.aiStyle = 7;
			this.aiType = 46;
			this.animationType = 46;
			base.npc.boss = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/Nope");
			this.bossBag = base.mod.ItemType("KingChickenBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/KingChickenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/KingChickenGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore3"), 1f);
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			string text = "It's a freakin' chicken, what did you expect...";
			Color rarityGreen = Colors.RarityGreen;
			byte r = rarityGreen.R;
			Color rarityGreen2 = Colors.RarityGreen;
			byte g = rarityGreen2.G;
			Color rarityGreen3 = Colors.RarityGreen;
			Main.NewText(text, r, g, rarityGreen3.B, false);
			potionType = base.mod.ItemType("ChickenEgg");
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KingChickenTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KingChickenMask"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Grain"), 1, false, 0, false, false);
		}

		public override void AI()
		{
			this.timer++;
			if (this.timer == 40)
			{
				string text = "*cluck cluck* You fool... Wait... How did I get here? *cluck cluck*";
				Color rarityOrange = Colors.RarityOrange;
				byte r = rarityOrange.R;
				Color rarityOrange2 = Colors.RarityOrange;
				byte g = rarityOrange2.G;
				Color rarityOrange3 = Colors.RarityOrange;
				Main.NewText(text, r, g, rarityOrange3.B, false);
			}
			if (this.timer == 200)
			{
				string text2 = "*cluck cluck* Why am I here? *cluck cluck*";
				Color rarityOrange4 = Colors.RarityOrange;
				byte r2 = rarityOrange4.R;
				Color rarityOrange5 = Colors.RarityOrange;
				byte g2 = rarityOrange5.G;
				Color rarityOrange6 = Colors.RarityOrange;
				Main.NewText(text2, r2, g2, rarityOrange6.B, false);
			}
			if (this.timer == 600)
			{
				string text3 = "*cluck cluck* Well, whatever... *cluck cluck*";
				Color rarityOrange7 = Colors.RarityOrange;
				byte r3 = rarityOrange7.R;
				Color rarityOrange8 = Colors.RarityOrange;
				byte g3 = rarityOrange8.G;
				Color rarityOrange9 = Colors.RarityOrange;
				Main.NewText(text3, r3, g3, rarityOrange9.B, false);
			}
			if (this.timer == 900)
			{
				string text4 = "*cluck cluck* Now, I am the Mighty King Chicken! *cluck cluck*";
				Color rarityOrange10 = Colors.RarityOrange;
				byte r4 = rarityOrange10.R;
				Color rarityOrange11 = Colors.RarityOrange;
				byte g4 = rarityOrange11.G;
				Color rarityOrange12 = Colors.RarityOrange;
				Main.NewText(text4, r4, g4, rarityOrange12.B, false);
			}
			if (this.timer == 1400)
			{
				string text5 = "*cluck cluck* And... Uh... *cluck cluck*";
				Color rarityOrange13 = Colors.RarityOrange;
				byte r5 = rarityOrange13.R;
				Color rarityOrange14 = Colors.RarityOrange;
				byte g5 = rarityOrange14.G;
				Color rarityOrange15 = Colors.RarityOrange;
				Main.NewText(text5, r5, g5, rarityOrange15.B, false);
			}
			if (this.timer == 1500)
			{
				string text6 = "*clucks in confusion*";
				Color rarityOrange16 = Colors.RarityOrange;
				byte r6 = rarityOrange16.R;
				Color rarityOrange17 = Colors.RarityOrange;
				byte g6 = rarityOrange17.G;
				Color rarityOrange18 = Colors.RarityOrange;
				Main.NewText(text6, r6, g6, rarityOrange18.B, false);
			}
			if (this.timer == 4000)
			{
				string text7 = "*clucks impatiently*";
				Color rarityOrange19 = Colors.RarityOrange;
				byte r7 = rarityOrange19.R;
				Color rarityOrange20 = Colors.RarityOrange;
				byte g7 = rarityOrange20.G;
				Color rarityOrange21 = Colors.RarityOrange;
				Main.NewText(text7, r7, g7, rarityOrange21.B, false);
			}
			if (this.timer == 8000)
			{
				string text8 = "*cluck cluck* You bore me, mortal! I shall be gone, but I will return!   ...   Maybe.";
				Color rarityOrange22 = Colors.RarityOrange;
				byte r8 = rarityOrange22.R;
				Color rarityOrange23 = Colors.RarityOrange;
				byte g8 = rarityOrange23.G;
				Color rarityOrange24 = Colors.RarityOrange;
				Main.NewText(text8, r8, g8, rarityOrange24.B, false);
			}
			if (this.timer == 8100)
			{
				string text9 = "*clucks a goodbye*";
				Color rarityOrange25 = Colors.RarityOrange;
				byte r9 = rarityOrange25.R;
				Color rarityOrange26 = Colors.RarityOrange;
				byte g9 = rarityOrange26.G;
				Color rarityOrange27 = Colors.RarityOrange;
				Main.NewText(text9, r9, g9, rarityOrange27.B, false);
				base.npc.active = false;
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public int timer;
	}
}
