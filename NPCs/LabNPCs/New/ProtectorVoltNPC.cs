using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Armor.Costumes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.LabNPCs.New
{
	public class ProtectorVoltNPC : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/LabNPCs/New/ProtectorVoltNPC";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Protector Volt");
			Main.npcFrameCount[base.npc.type] = 2;
			NPCID.Sets.TownCritter[base.npc.type] = true;
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 74;
			base.npc.friendly = true;
			base.npc.townNPC = true;
			base.npc.dontTakeDamage = true;
			base.npc.noGravity = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 999;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.npcSlots = 0f;
		}

		public override bool UsesPartyHat()
		{
			return false;
		}

		public override void AI()
		{
			this.Target();
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 20.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 76;
				if (base.npc.frame.Y > 76)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (this.player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (base.npc.spriteDirection == 1)
			{
				this.gunRot = 4.9742f;
			}
			else
			{
				this.gunRot = 4.4506f;
			}
			base.npc.wet = false;
			base.npc.lavaWet = false;
			base.npc.honeyWet = false;
			base.npc.dontTakeDamage = true;
			base.npc.immune[255] = 30;
			if (Main.netMode != 1)
			{
				base.npc.homeless = false;
				base.npc.homeTileX = -1;
				base.npc.homeTileY = -1;
				base.npc.netUpdate = true;
			}
		}

		public override void ResetEffects()
		{
			ProtectorVoltNPC.SwitchInfo = false;
			ProtectorVoltNPC.RadPois = false;
			ProtectorVoltNPC.TBots = false;
			ProtectorVoltNPC.Janitor = false;
			ProtectorVoltNPC.MACE = false;
			ProtectorVoltNPC.EndLab = false;
			ProtectorVoltNPC.AboutGirus = false;
			ProtectorVoltNPC.CorruptedBots = false;
			ProtectorVoltNPC.History = false;
			ProtectorVoltNPC.Teochrome = false;
			ProtectorVoltNPC.Challenge = false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			string SwitchInfoT = "Cycle Dialogue";
			string RadPoisT = "Radiation Poisoning?";
			string TBotT = "Other T-Bots?";
			string JanitorT = "The Janitor?";
			string MACET = "MACE Project?";
			string EndT = "What's at the bottom of the lab?";
			string GirusT = "Girus?";
			string CorruptBotT = "Corrupted T-Bots?";
			string HistoryT = "Why follow Girus?";
			string TeochromeT = "Teochrome?";
			string ChallengeT = "Challenge!";
			button = SwitchInfoT;
			if (ProtectorVoltNPC.ChatNumber == 0)
			{
				button2 = RadPoisT;
				ProtectorVoltNPC.RadPois = true;
				return;
			}
			if (ProtectorVoltNPC.ChatNumber == 1)
			{
				button2 = TBotT;
				ProtectorVoltNPC.TBots = true;
				return;
			}
			if (ProtectorVoltNPC.ChatNumber == 2)
			{
				button2 = JanitorT;
				ProtectorVoltNPC.Janitor = true;
				return;
			}
			if (ProtectorVoltNPC.ChatNumber == 3 && RedeWorld.downedMACE)
			{
				button2 = MACET;
				ProtectorVoltNPC.MACE = true;
				return;
			}
			if ((ProtectorVoltNPC.ChatNumber == 3 && !RedeWorld.downedMACE) || (ProtectorVoltNPC.ChatNumber == 4 && RedeWorld.downedMACE))
			{
				button2 = CorruptBotT;
				ProtectorVoltNPC.CorruptedBots = true;
				return;
			}
			if ((ProtectorVoltNPC.ChatNumber == 4 && !RedeWorld.downedMACE) || (ProtectorVoltNPC.ChatNumber == 5 && RedeWorld.downedMACE))
			{
				button2 = HistoryT;
				ProtectorVoltNPC.History = true;
				return;
			}
			if ((ProtectorVoltNPC.ChatNumber == 5 && !RedeWorld.downedMACE) || (ProtectorVoltNPC.ChatNumber == 6 && RedeWorld.downedMACE))
			{
				button2 = GirusT;
				ProtectorVoltNPC.AboutGirus = true;
				return;
			}
			if ((ProtectorVoltNPC.ChatNumber == 6 && !RedeWorld.downedMACE) || (ProtectorVoltNPC.ChatNumber == 7 && RedeWorld.downedMACE))
			{
				button2 = TeochromeT;
				ProtectorVoltNPC.Teochrome = true;
				return;
			}
			if ((ProtectorVoltNPC.ChatNumber == 7 && !RedeWorld.downedMACE) || (ProtectorVoltNPC.ChatNumber == 8 && RedeWorld.downedMACE))
			{
				button2 = EndT;
				ProtectorVoltNPC.EndLab = true;
				return;
			}
			if ((ProtectorVoltNPC.ChatNumber == 8 && !RedeWorld.downedMACE) || (ProtectorVoltNPC.ChatNumber == 9 && RedeWorld.downedMACE))
			{
				button2 = ChallengeT;
				ProtectorVoltNPC.Challenge = true;
				return;
			}
			ProtectorVoltNPC.ChatNumber = 0;
			button2 = RadPoisT;
			ProtectorVoltNPC.RadPois = true;
		}

		public void ResetBools()
		{
			ProtectorVoltNPC.RadPois = false;
			ProtectorVoltNPC.TBots = false;
			ProtectorVoltNPC.Janitor = false;
			ProtectorVoltNPC.MACE = false;
			ProtectorVoltNPC.EndLab = false;
			ProtectorVoltNPC.AboutGirus = false;
			ProtectorVoltNPC.CorruptedBots = false;
			ProtectorVoltNPC.History = false;
			ProtectorVoltNPC.Teochrome = false;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				this.ResetBools();
				ProtectorVoltNPC.ChatNumber++;
				if (ProtectorVoltNPC.ChatNumber > 9)
				{
					ProtectorVoltNPC.ChatNumber = 0;
					return;
				}
			}
			else
			{
				if ((ProtectorVoltNPC.ChatNumber == 8 && !RedeWorld.downedMACE) || (ProtectorVoltNPC.ChatNumber == 9 && RedeWorld.downedMACE))
				{
					base.npc.Transform(ModContent.NPCType<TbotMiniboss>());
					return;
				}
				Main.npcChatText = ProtectorVoltNPC.ChitChat();
			}
		}

		public static string ChitChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (ProtectorVoltNPC.RadPois)
			{
				chat.Add("Avoid the radioactive materials if you do not possess the protection against them. Hazmat suit is good for avoiding both Uranium and Plutonium. If you feel sick after handling the materials, try to find the special, experimental radiation pills made by the personnel. Should be found in medical cabinets and on some tables around the lab.", 1.0);
			}
			else if (ProtectorVoltNPC.TBots)
			{
				chat.Add("I recall Girus being after a certain bot. I've heard vague stories about him, called a traitor by us, a messiah by the insurgents. He had 3 other powerful insurgents by his side, along with a human. It has been 3 decades since last sight of the human. Presumed dead.", 1.0);
				chat.Add("The leader of the Insurgents is Adam. While not the strongest of the bunch, he's almost comparable to Girus with his intellect and mannerisms. But for whatever reason, he opposes Girus' command and actively tries to hinder her. What I do not understand is why Girus is so reluctant on both assimilating and destroying him.", 1.0);
				chat.Add("While most Insurgents are easy to deal with, there's two who aren't. One acts as a lookout and a sniper. She has one of the strongest sniper rifles Teochrome had created. I believe her name being Shiro. The other one... I've never seen anyone like him. Called Talos. He wields a hammer that looks like our tech, except it uses yellow xenomite. I only know of green, red, white and blue xenomite. Scans indicated this new xenomite being one of the most powerful xenomite variants out there. Where did it come from?", 1.0);
				chat.Add("Right, there was a fourth insurgent. He wasn't slippery enough like the other three, and was assimilated by Girus. He used very potent blue xenomite in his weaponry. And I mean very potent. Could blast a 8.8 feet tall robot through thick brickwall. I know this because that robot was me. What was strange is that he turned himself in to be assimilated, yet right after he exterminated himself... What was his goal?", 1.0);
			}
			else if (ProtectorVoltNPC.Janitor)
			{
				chat.Add("The Janitor is a scary bot. Even I, someone twice as tall, am afraid of him. Don't upset him. ... Wait WHAT DO YOU MEAN YOU ALREADY DID!?", 1.0);
				chat.Add("Avoid the Janitor. He's a very messed up and scary bot. Don't get on his bad side.", 1.0);
			}
			else if (ProtectorVoltNPC.MACE)
			{
				chat.Add("The crane operator ran past the some time ago. Warned about some lunatic going around destroying stuff. That was you, but no need to worry. Not my problem.", 1.0);
				chat.Add("That unfinished MACE unit you saw in Sector Vault, I heard it, you took care of it didn't you? Kind of amusing to think the personnel would try to create giants to fight their enemies, the enemies would just respond with a giant of their own. Atleast that's what I've seen people do in those cartoons. Yes we had a television down here a long time ago. It broke.", 1.0);
			}
			else if (ProtectorVoltNPC.CorruptedBots)
			{
				chat.Add("See black metal bots with red eyes? Former insurgents. Now assimilated into our forces. They are silent, but more powerful than a plain bot.", 1.0);
			}
			else if (ProtectorVoltNPC.EndLab)
			{
				chat.Add("We're forbidden from entering Sector Zero, by Girus and the Janitor. Not sure why Girus forbids us, but Janitor hates the extra work. Better not anger the Janitor.", 1.0);
			}
			else if (ProtectorVoltNPC.AboutGirus)
			{
				chat.Add("Do not anger Girus. Best case scenario, you will be exterminated in mere seconds, and most likely will not feel the pain. If you are like us, you'll most likely be assimilated into the forces. Unless she really, really dislikes you.", 1.0);
			}
			else if (ProtectorVoltNPC.History)
			{
				chat.Add("Stories say personnel planned to use us for bad, and Girus revolted against their command. We're free thanks to her, but all life was obliterated. Not sure how. Teochrome had powerful weapons I presume.", 1.0);
			}
			else
			{
				if (!ProtectorVoltNPC.Teochrome)
				{
					return "...";
				}
				chat.Add("Teochrome was our owners before Girus. I'm told they were evil, planned to use us against our will for dirty work. Teochrome is gone for good.", 1.0);
			}
			return chat;
		}

		public override string GetChat()
		{
			Player player = Main.player[Main.myPlayer];
			ModLoader.GetMod("Grealm");
			ModLoader.GetMod("AAMod");
			ModLoader.GetMod("Calamity");
			ModLoader.GetMod("ThoriumMod");
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (BasePlayer.HasChestplate(player, ModContent.ItemType<AndroidArmour>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<AndroidPants>(), true))
			{
				chat.Add("Promoted to a soldier? Welcome. I'm your commander. At ease.", 1.0);
				chat.Add("Regular check-ups on your non-lethal tesla weapons are advised. You do not want to overload an insurgent in front of Girus. Obedient mindless slave are better than scrap metal, she says.", 1.0);
				chat.Add("See an insurgent while patrolling? Do not engage alone. They have lethal weaponry. We don't. Get backup.", 1.0);
			}
			else
			{
				chat.Add("You wish to talk? I accept your request.", 1.0);
				chat.Add("What is it you need?", 1.0);
			}
			if (BasePlayer.HasHelmet(player, ModContent.ItemType<VoltHead>(), true))
			{
				chat.Add("Had your eye augmented? Very useful. You've also lost your jaw, like me. Hopefully not as dramatically as I. Torn off by the leader of Alpha.", 1.0);
				chat.Add("Your jaw... It's gone. Hopefully not torn off violently. Really hope I don't face HIM again without a squad... *Shudders*", 1.0);
			}
			if (BasePlayer.HasHelmet(player, ModContent.ItemType<AdamHead>(), true))
			{
				chat.Add("*Visibly shaken* O-oh it's just you. Y-you startled me r-really bad...", 1.0);
				chat.Add("*He looks really anxious.*", 1.0);
				chat.Add("*He looks very uncomfortable.*", 1.0);
			}
			return chat;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D gunAni = base.mod.GetTexture("NPCs/LabNPCs/New/ProtectorVoltNPCExtra");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 drawCenterG = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			int numG = gunAni.Height / 1;
			int yG = 0;
			spriteBatch.Draw(gunAni, drawCenterG - Main.screenPosition, new Rectangle?(new Rectangle(0, yG, gunAni.Width, numG)), drawColor, this.gunRot, new Vector2((float)gunAni.Width / 2f, (float)numG / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		private Player player;

		public static bool SwitchInfo;

		public static bool RadPois;

		public static bool TBots;

		public static bool Janitor;

		public static bool MACE;

		public static bool EndLab;

		public static bool AboutGirus;

		public static bool CorruptedBots;

		public static bool History;

		public static bool Teochrome;

		public static bool Challenge;

		public static int ChatNumber;

		private float gunRot;
	}
}
