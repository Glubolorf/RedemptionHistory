using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Loreholder : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Loreholder, Cursed Blade");
			base.Tooltip.SetDefault("'Your vision is blocked by walls of text'\nTells you the lore of enemies slain by this weapon\n[c/ffea9b:A sentient blade, cursed with infinite knowledge]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 40;
			base.item.melee = true;
			base.item.width = 72;
			base.item.height = 72;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = 22000;
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedGem", 1);
			modRecipe.AddIngredient(null, "ForgottenSword", 1);
			modRecipe.AddIngredient(520, 5);
			modRecipe.AddIngredient(521, 5);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0)
			{
				if (target.type == base.mod.NPCType("AAAA"))
				{
					Main.NewText("<Loreholder> The remnants of the Keeper's soul, why it was in the Fallen I do not know...", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("AJollyMadman"))
				{
					Main.NewText("<Loreholder> Once a noble knight of Gathuram. They were lost in a demon's destruction and had their body revived under mysterious means. Now dead and stuck under the earth in the dark labyrinthine caves, they have lost their mind and gone completely insane. The cruelty of nature is unyielding. They will kill any being they spot moving, human or not.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Android"))
				{
					Main.NewText("<Loreholder> The weakest unit in King Slayer III's army. They are constructed with heavy plating, giving them high defence.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Apidroid"))
				{
					Main.NewText("<Loreholder> A special type of Android, constructed with strange pink plating. These have insane defence.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Blobble"))
				{
					Main.NewText("<Loreholder> An exceptionally rare slime native to Ithon. It may look harmless, but the acid it is composed of can dissolve iron in less than a minute", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("BloodWormHead") || target.type == base.mod.NPCType("BloodWormBody") || target.type == base.mod.NPCType("BloodWormTail") || target.type == base.mod.NPCType("BloodDiggerHead") || target.type == base.mod.NPCType("BloodDiggerBody") || target.type == base.mod.NPCType("BloodDiggerTail"))
				{
					Main.NewText("<Loreholder> Annoying worms that leech on creatures, consuming their blood.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("BoneLeviathanHead") || target.type == base.mod.NPCType("BoneLeviathanBody") || target.type == base.mod.NPCType("BoneLeviathanTail"))
				{
					Main.NewText("<Loreholder> Extremely rare bone serpents. Most hide under the ashes of the underworld, only awakening once a year.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("BoneWorm"))
				{
					Main.NewText("<Loreholder> Serpent amalgamations of bones, constructed by the Keeper in an attempt to kill you.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Chicken") || target.type == base.mod.NPCType("LeghornChicken") || target.type == base.mod.NPCType("RedChicken"))
				{
					Main.NewText("<Loreholder> Once famous livestock of sentient races, these majestic creatures are now free to roam the world, free from their shackles and their slaughter. Not like they would care though, since they’re chickens.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("VlitchChicken"))
				{
					Main.NewText("<Loreholder> WAIT THAT'S NOT A CHICKEN! THAT'S A ROBOT!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("ChickenCultist"))
				{
					Main.NewText("<Loreholder> A follower of the being known as “The Mighty King Chicken.” They are devoted followers of the King who seek vengeance for the unjust killing of him. They will only appear after he is dead. It is believed they were created using Forbidden Necromancy by an insane sorcerer.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("ChickenGold"))
				{
					Main.NewText("<Loreholder> Chickens born with golden feathers, and the ability to lay solid golden eggs. Unfortunately, due to the fact the egg is made of gold, most chicks cannot hatch without assistance from metal-cracking tools, and end up starving to death.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CoastScarab"))
				{
					Main.NewText("<Loreholder> A species of scarab commonly found scuttering around the beach. They’re rather cute and harmless creatures, but can be used to create dyes if you’re a greedy monster.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CorpseWalkerPriest"))
				{
					Main.NewText("<Loreholder> Undead that take the role of a cleric, they shoot golden sparks that can heal other undead... But damage humans.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CorruptBat"))
				{
					Main.NewText("<Loreholder> A bat consumed by the Corruption.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CorruptedBlade"))
				{
					Main.NewText("<Loreholder> Mechanical floating blades that will swing at any living thing, created by Girus.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CorruptedPaladin"))
				{
					Main.NewText("<Loreholder> These are mechanical robots with insane endurance and defence, created by Girus.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CorruptedProbe"))
				{
					Main.NewText("<Loreholder> Corrupted Probes that shoot lasers at any living thing, the more damaged they are, the more rapid they shoot. Created by Girus.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CorruptedTBot"))
				{
					Main.NewText("<Loreholder> T-bots that were once normal, but got captured by Girus or his minions and corrupted.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CorruptedWormHead") || target.type == base.mod.NPCType("CorruptedWormBody") || target.type == base.mod.NPCType("CorruptedWormTail"))
				{
					Main.NewText("<Loreholder> Mechanical worms that are quickly constructed within Vlitch Gigipede's armoured tail.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CrusherHead") || target.type == base.mod.NPCType("CrusherBody") || target.type == base.mod.NPCType("CrusherTail"))
				{
					Main.NewText("<Loreholder> A dangerous worm that consumes all in its path.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("DarkSlime"))
				{
					Main.NewText("<Loreholder> A gelatinous mess of dark magic.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("DarkSlimeling"))
				{
					Main.NewText("<Loreholder> A sentient part of the Dark Slime.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("DarkSoul") || target.type == base.mod.NPCType("DarkSoul2") || target.type == base.mod.NPCType("DarkSoul3") || target.type == base.mod.NPCType("DarkSoul4"))
				{
					Main.NewText("<Loreholder> A void black soul, devoid of life. It will disperse after a short time.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("DeathGardener"))
				{
					Main.NewText("<Loreholder> They were once a widow in their past life, now they roam the dungeons.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("FatPirate"))
				{
					Main.NewText("<Loreholder> A pirate that cooks food for the crew. He seems to like it more than the others...", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("ForestGolem") || target.type == base.mod.NPCType("ForestGolemBlooming") || target.type == base.mod.NPCType("ForestGolemWounded"))
				{
					Main.NewText("<Loreholder> A golem of living wood and leaves. Occassionally, they find a shallow pond, and hibernate in the centre. The water from the pond feeds the golem while hibernating.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("ForestNymph") || target.type == base.mod.NPCType("EbonNymph") || target.type == base.mod.NPCType("ShadeNymph") || target.type == base.mod.NPCType("HallowNymph"))
				{
					Main.NewText("<Loreholder> They usually live deep within forests, in hollowed-out trees. They can learn basic magic, like growing flora, controlling the roots of trees, and healing.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("ForestSpider"))
				{
					Main.NewText("<Loreholder> These spiders like to live in caves in deciduous forests, only coming outside at night. They were more vibrantly coloured millions of years ago, but has changed due to it being very noticeable", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("GrandLarva"))
				{
					Main.NewText("<Loreholder> Gross insects holding many flies within. Can be used as good bait.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("GraniteCluster"))
				{
					Main.NewText("<Loreholder> A mixture of granite and a soul. These are unstable beings, shooting energy in all directions.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("HazmatSkeleton"))
				{
					Main.NewText("<Loreholder> A skeleton in the wasteland, they wear Hazmat Suits. Not like they actually need them.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("HazmatZombie"))
				{
					Main.NewText("<Loreholder> A zombie in the wasteland, they wear Hazmat Suits. Not like they actually need them.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("InfectedCaveBat"))
				{
					Main.NewText("<Loreholder> A bat that got infected after the Xenomite Crystal shattered.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("InfectedDemonEye"))
				{
					Main.NewText("<Loreholder> A demon eye that got infected after the Xenomite Crystal shattered.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("InfectedDiggerHead") || target.type == base.mod.NPCType("InfectedDiggerBody") || target.type == base.mod.NPCType("InfectedDiggerTail"))
				{
					Main.NewText("<Loreholder> A digger that got infected after the Xenomite Crystal shattered.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("InfectedGiantBat"))
				{
					Main.NewText("<Loreholder> A giant bat that got infected after the Xenomite Crystal shattered.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("InfectedGiantWormHead") || target.type == base.mod.NPCType("InfectedGiantWormBody") || target.type == base.mod.NPCType("InfectedGiantWormTail"))
				{
					Main.NewText("<Loreholder> A giant worm that got infected after the Xenomite Crystal shattered.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("InfectedZombie"))
				{
					Main.NewText("<Loreholder> A zombie that got infected after the Xenomite Crystal shattered.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("LickyLickyCactus"))
				{
					Main.NewText("<Loreholder> A creepy plant that grows in deserts. Produces painful clouds of pollen.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("LivingBlackGloop"))
				{
					Main.NewText("<Loreholder> Black Gloop that became sentient.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("LivingBloom"))
				{
					Main.NewText("<Loreholder> A common creature native to Anglon, living in lush forests. They are made out of plant fibre and roots.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("LostSoul1") || target.type == base.mod.NPCType("LostSoul2") || target.type == base.mod.NPCType("LostSoul3"))
				{
					Main.NewText("<Loreholder> Lost Souls search around the world to look for corpses to infuse with. They roam catacombs and graveyards to find the right vessel.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("MarbleChessHorse"))
				{
					Main.NewText("<Loreholder> A strange mixture of marble and a soul. If broken, it will spew sparks of fire.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("MartianScamArtist"))
				{
					Main.NewText("<Loreholder> They scam. Yep.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("MoltenGolem"))
				{
					Main.NewText("<Loreholder> Deadly golems of obsidian and magma. They erupt flames from their body. When in danger, they will slam to the ground and absorb the earth's magnetic energy to shield itself.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("MoonflareBat"))
				{
					Main.NewText("<Loreholder> Common bats that come out at night, they glow like the moon, which confuses some bugs.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("OmegaMK2Droid1") || target.type == base.mod.NPCType("OmegaMK2Droid2"))
				{
					Main.NewText("<Loreholder> A Mk-2 Omega Android... How did you even manage to kill it with this weapon?", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("PrototypeSilver"))
				{
					Main.NewText("<Loreholder> Tough mechanical brutes that could shield themselves. The 2nd weakest unit in King Slayer's army", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("RadiumDiggerHead") || target.type == base.mod.NPCType("RadiumDiggerBody") || target.type == base.mod.NPCType("RadiumDiggerTail"))
				{
					Main.NewText("<Loreholder> Extremely dangerous diggers, they eat the xenomite that lies in wasteland rocks.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("RaggedZombie"))
				{
					Main.NewText("<Loreholder> A zombie that was a druid in their past life.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("RainbowChicken"))
				{
					Main.NewText("<Loreholder> Beautiful.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("RogueTBot"))
				{
					Main.NewText("<Loreholder> A T-Bot that went rogue, since they are simply exoskeletons, they have no defence.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SandskinSpider"))
				{
					Main.NewText("<Loreholder> A spider that commonly burrowing in the desert sands. They don't normally come out, due to the intense heat of the sun.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("ServantOfTheHolyKnight"))
				{
					Main.NewText("<Loreholder> f xjwafsy tk ymj ijrnlti tk qnlmy. nk ymjd jajw htrj ktw dtz, ymjwj nx st mtuj fsdrtwj, dtz fwj ufxy wjijruynts. wzs.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SkeleDruid"))
				{
					Main.NewText("<Loreholder> A skeleton wearing druid clothing and carrying a Seedbag. They will use the Seedbag to shoot homing spore clouds at you.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Skelemies"))
				{
					Main.NewText("<Loreholder> A spooky skeledude that wears an old tophat.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Skelemies2"))
				{
					Main.NewText("<Loreholder> A dapper skeledude that wears a ye olde hat.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SkeletonAssassin") || target.type == base.mod.NPCType("SkeletonAssassin2"))
				{
					Main.NewText("<Loreholder> A short skeleton with a bandana and a worn dagger. When they die, their soul will either disperse or fly out of their body.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SkeletonDueller"))
				{
					Main.NewText("<Loreholder> A skeleton with a worn red cape and an old rapier. When they die, their soul will either disperse or fly out of their body.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Vepdor"))
				{
					Main.NewText("<Loreholder> A skeleton with a worn purple tuxedo and a shining silver rapier. A chunk of metal conceals his mouth. Once he lived in Ithon, and was the founder of the Edgeblade party, but was slain by one of his members; Elgas.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SkeletonMinion"))
				{
					Main.NewText("<Loreholder> A simple skeleton. Nothing more to say.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SkeletonNoble") || target.type == base.mod.NPCType("SkeletonNobleArmoured") || target.type == base.mod.NPCType("SkeletonNobleArmoured2") || target.type == base.mod.NPCType("SkeletonNobleArmoured3"))
				{
					Main.NewText("<Loreholder> A skeleton that once was a noble knight. They will slash you with their rusted noble's sword.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SkeletonWanderer") || target.type == base.mod.NPCType("SkeletonWanderer2"))
				{
					Main.NewText("<Loreholder> A skeleton wielding a rusted longspear. When they die, their soul will either disperse or fly out of their body.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SkeletonWarden"))
				{
					Main.NewText("<Loreholder> A skeleton that was once an archer. They have moderately bad aim.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SkullDigger"))
				{
					Main.NewText("<Loreholder> The first and only successful reanimation from the Keeper. Skulldigger was a ancient warrior buried in the tombs of Gatharum. During the Keeper’s time trapped there, she managed to successfully resurrect Skulldigger’s skeleton, bringing back it’s flesh and soul. Through this, Skulldigger and the Keeper became close friends, as they had no one else to talk to trapped underground. His bond with her was the only thing he had left, as all his past friends and family had long passed away, and any relatives he had on the surface had been executed or enslaved. When the Keeper’s body sucame to the forbidden necromancy she had become addicted to, he was the only being that could pity and mourn her. He was the last being that the Keeper could think of before she went insane, and thus she destroyed the walls of the Gathic catacombs so he could be free with her. She has long forgotten about him, now only driven by the hunger for souls and immense pain. He has not forgotten her however, and if anything were to happen to her, he would be the first and only being to avenge her. After the Keeper is killed, Skulldigger may appear to avenge whoever slaid her within the caverns. He carries an abandoned teddy bear.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SludgyBoi") || target.type == base.mod.NPCType("SludgyBoi2"))
				{
					Main.NewText("<Loreholder> Sentient sludge.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SnowyBoi"))
				{
					Main.NewText("<Loreholder> A sorcerer of unknown origins. Doesn't originate from my realm or the other...", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SpacePaladin"))
				{
					Main.NewText("<Loreholder> The strongest unit in King Slayer’s army, they are colossal robots. Their hand is a minigun and the other hand usually carry a giant weapon. But I guess they just don't feel like it.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SpiderSwarmer") || target.type == base.mod.NPCType("SpiderSwarmerQueen"))
				{
					Main.NewText("<Loreholder> They live anywhere in the world, normally in dark areas like basements, dungeons, caves etc. But not in this world, they just live in spider caves.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("StoneGolemAncient1") || target.type == base.mod.NPCType("StoneGolemAncient2"))
				{
					Main.NewText("<Loreholder> Stone golems infused with redemptive energy.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("StrangePortal"))
				{
					Main.NewText("<Loreholder> A portal that came from another world. With it carries something that could alter the world.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SunkenCaptain"))
				{
					Main.NewText("<Loreholder> Once a great captain, now sails the seas for eternity. Only when the moon is full can he safely go back on land.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SunkenDeckhand"))
				{
					Main.NewText("<Loreholder> A crewmember from the Sunken Captian's old crew.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SunkenParrot"))
				{
					Main.NewText("<Loreholder> A pet from the Sunken Captian's old crew.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("TheSoulless"))
				{
					Main.NewText("<Loreholder> aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("TreeBug"))
				{
					Main.NewText("<Loreholder> A beetle commonly found inhabiting trees. It feeds on their leaves and uses it’s leaf-like shell for camouflage from predators. It’s shell makes a good source of green dye.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("UndeadExecutioner1") || target.type == base.mod.NPCType("UndeadExecutioner2") || target.type == base.mod.NPCType("UndeadExecutioner3"))
				{
					Main.NewText("<Loreholder> These were the Keeper's loyal minions. After the Keeper's death, they sought revenge for the one responsible.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("UndeadViolinist"))
				{
					Main.NewText("<Loreholder> A husk of a sad violinist. Doesn't originate from my realm or the other...", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("UnstablePortal"))
				{
					Main.NewText("<Loreholder> An unstable portal. Doesn't originate from my realm or the other...", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("WanderingSoul"))
				{
					Main.NewText("<Loreholder> Before the Keeper's death, Wandering Souls were trapped within the Catacombs of Gathuram. However, after the Keeper's defeat, the spell was broken and now they roam free.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("XenoChomper") || target.type == base.mod.NPCType("XenoChomper2"))
				{
					Main.NewText("<Loreholder> Yowch! it bit me!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("XenomiteEye"))
				{
					Main.NewText("<Loreholder> Infected minions of the Infected Eye.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("XenomiteFragment"))
				{
					Main.NewText("<Loreholder> A fragment of the Xenomite Crystal.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("XenomiteGargantuan"))
				{
					Main.NewText("<Loreholder> A bigass golem, infected by the wasteland.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("XenomiteGolem"))
				{
					Main.NewText("<Loreholder> A golem, infected by the wasteland.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("XenomitePiece"))
				{
					Main.NewText("<Loreholder> A piece of the Xenomite Crystal.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("XenonRoller"))
				{
					Main.NewText("<Loreholder> Rollin' rollin' rollin'... Oh, I'm suppose to tell you lore... They are sentient chunks of dead rock.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Blisterface"))
				{
					Main.NewText("<Loreholder> That's a bigass fish.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Blisterling") || target.type == base.mod.NPCType("Blisterling2"))
				{
					Main.NewText("<Loreholder> Ow! It bit me!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("InfectionHive"))
				{
					Main.NewText("<Loreholder> Gross. Why must you kill it using me!? I don't wanna touch that!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("IrradiatedBehemoth"))
				{
					Main.NewText("<Loreholder> Gross. Why must you kill it using me!? I don't wanna touch that giant lump of sludge!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SludgyBlob"))
				{
					Main.NewText("<Loreholder> Ech.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Stage2Scientist") || target.type == base.mod.NPCType("Stage2ScientistBoss"))
				{
					Main.NewText("<Loreholder> A scientist with Stage 2 Infection. Very dangerous fellows.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Stage3Scientist"))
				{
					Main.NewText("<Loreholder> A scientist with Stage 3 Infection. Basically a crystal by now.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("WalterInfected"))
				{
					Main.NewText("<Loreholder> A scientist of the lab, infected with Xenomite.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("InfectedEye"))
				{
					Main.NewText("<Loreholder> Tis' a shame for Cthulhu's other eye to be caught up with the infection. Now he's got no eyes! Well... Assuming he has just 2 eyes...", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("KingChicken") || target.type == base.mod.NPCType("KingChickenT"))
				{
					Main.NewText("<Loreholder> One time an insane sorcerer woke up one day and thought: “Y’KNOW WHAT WOULD BE A GOOD IDEA!? INTELLIGENT CHICKENS!” and so he gave pieces of his soul to many chickens. He also thought it would be funny to put a mini crown on one, but that crown held a curse, and it caused that chicken to become extremely cocky. Hehe.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("TheKeeper"))
				{
					Main.NewText("<Loreholder> Deep within the inner Catacombs of Gathuram, far below the surface, a Lost Soul found a host. As the soul fused with the corpse, it awoken as a Fallen. The Fallen only saw darkness, although there was occasional blue lights. The ground was littered with bones and puddles of water. Calling out, the Fallen hoped to hear another voice, but the catacombs were silent; the only sound she could hear was dripping water falling from the ceiling. Hours passed as the Fallen roamed the hushed corridors and rooms. It was all the same, not a soul in sight. Days had passed, the Fallen was overwhelmed with loneliness and desperation. She found a black cloak which she then wore, she also discovered a wooden rod with an ancient skull on the top. Misery was taking over her, she wished for a second death, but she couldn’t kill herself. A week has passed and she was still alone. Desperate for company, she tried to learn Necromancy to create her own friends. It took months before complete success, and she had finally created a skeleton - she finally created a friend. The first skeleton - the first friend - only responded to commands, it had neither emotions nor senses. Despite this, she tried to talk to it like another person, but to no avail. Over months of constructing, she had created a dozen skeletons, but they were all the same. To her surprise, the first skeleton seemed to be weakened greatly. She attempted to heal it, but nothing happened. The passage of time was incomprehensible to the Fallen, she didn’t expect her first friend to revert so soon. Once again, she became only more desperate - she didn’t want to lose her first friend. The skull baton she found had a small amount of mana still inside it, however, the constant Necromancy had diluted it. When she strived to heal her friend again, she felt a numb pain course through her rotten body. The skeleton’s hollow eye sockets lit up with a tiny dot of white light, and a faded light emerged in its ribcage. The skeleton looked at her, the Fallen felt the skeleton’s presence for the first time. For the first time in years, a smile appeared on her face. She asked what the skeleton’s name is, it replied with a quiet voice: “Sk… ull… Dig… ger...” In only a day, she had become addicted to the Forbidden Necromancy. Red eyes started forming on her black cloak, she became frail, many thin arms grew from her body.But her mind was foggy, she felt nothing, the Forbidden Necromancy had taken control. In this time, her first creation - Skull Digger - had become stronger, he was no longer a skeleton, he had flesh, and a mask of bone had formed on his face. More and more, she become addicted to Forbidden Necromancy. Then one day, a sudden wave of extreme pain and agony flowed through her, she shrieked. Her skin burned, pieces of rusted metal emerged on her hands, her body distorted and grew into a monstrosity.When it ended, all she felt was a mixture of pain and powe- OH GOD, PLEASE, THIS IS A CURSE FOR ME TOO YA KNOW!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("VlitchCleaver"))
				{
					Main.NewText("<Loreholder> Girus's 1st overlord. A giant mechanical cleaver. Sorry, I'm getting too tired to tell you more.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("VlitchCore1") || target.type == base.mod.NPCType("VlitchCore2") || target.type == base.mod.NPCType("VlitchCore3"))
				{
					Main.NewText("<Loreholder> A... Core...", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("VlitchWormHead") || target.type == base.mod.NPCType("VlitchWormBody") || target.type == base.mod.NPCType("VlitchWormTail"))
				{
					Main.NewText("<Loreholder> Ouch, hey, that worm's armour is tough!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("XenomiteCrystalPhase2"))
				{
					Main.NewText("<Loreholder> A rather large, sentient Xenomite crystal. Seems to be a part of something larger...", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("KSEntrance"))
				{
					Main.NewText("<Loreholder> OUCH, MY MIND! ... Sorry, I can't tell you his lore, it's just too much. This curse of knowledge is painful.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("KSEntranceClone"))
				{
					Main.NewText("<Loreholder> (Oh thank God, it isn't the actual King Slayer...) A robot designed to be a clone of King Slayer III.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("OmegaOblitDamaged"))
				{
					Main.NewText("<Loreholder> A prototype designed by Girus. Originally he planned it to be a giant Vlitch Skull, but decided against it for some reason.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("OmegaAndroid"))
				{
					Main.NewText("<Loreholder> Hm, looks like King Slayer's androids had a redesign. Wonder why that is.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("OmegaPrototype"))
				{
					Main.NewText("<Loreholder> Hm, looks like King Slayer's prototypes had a redesign. Wonder why that is.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("OmegaSpacePaladin"))
				{
					Main.NewText("<Loreholder> Hm, looks like King Slayer's space paladudes had a redesign. Wonder why that is.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("SpaceKeeper"))
				{
					Main.NewText("<Loreholder> A unit in Slayer's army, inbetween Prototype Silvers and Space Paladins. They'd shoot nanobots to heal damaged robots, but can also charge up their core to unleash a powerful blast.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("MACEProjectJaw"))
				{
					Main.NewText("<Loreholder> Haha! We won! ... OH WAIT NO IT'S STILL ALIVE!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("MACEProjectHead"))
				{
					Main.NewText("<Loreholder> A work of genius, perfect at wiping out a large group of enemies in an instant.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("HiveGrowth") || target.type == base.mod.NPCType("HiveGrowth2"))
				{
					Main.NewText("<Loreholder> That's just... Ech.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("MACEProjectFist1") || target.type == base.mod.NPCType("MACEProjectFist3") || target.type == base.mod.NPCType("MACEProjectFist4"))
				{
					Main.NewText("<Loreholder> A fist of the MACE Project. Smashing.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("BloodBoiledSkeleton"))
				{
					Main.NewText("<Loreholder> A bigass skeleton covered in boiling blood and flesh. When killed, they will explode... But I guess you already figured that out...", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("BobTheBlob"))
				{
					Main.NewText("<Loreholder> Bob the Blob is a blob that bobbed and blobbed.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CleaverDagger"))
				{
					Main.NewText("<Loreholder> One of Vlitch Cleaver's daggers that circle around it. They can reflect projectiles.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("CorruptedCopter1") || target.type == base.mod.NPCType("CorruptedDrone1"))
				{
					Main.NewText("<Loreholder> Nyoooooom!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("DecayedGhoul") || target.type == base.mod.NPCType("GreenPigron") || target.type == base.mod.NPCType("InfectedGrub") || target.type == base.mod.NPCType("InfectedSnowFlinx") || target.type == base.mod.NPCType("InfectedSwarmer") || target.type == base.mod.NPCType("RadioactiveSlime") || target.type == base.mod.NPCType("RadiumDigger2Head") || target.type == base.mod.NPCType("RadiumDigger2Body") || target.type == base.mod.NPCType("RadiumDigger2Tail") || target.type == base.mod.NPCType("RadiumRampager") || target.type == base.mod.NPCType("SneezyInfectedFlinx") || target.type == base.mod.NPCType("SpikyRadioactiveSlime") || target.type == base.mod.NPCType("XenomiteBeast"))
				{
					Main.NewText("<Loreholder> An infected varient of an enemy.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("NuclearSlime"))
				{
					Main.NewText("<Loreholder> WAIT NO THAT SLIME HAS A BOMB!", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("OmegaPilotDroid"))
				{
					Main.NewText("<Loreholder> A pilot. He seemed to be angry with you for destroying his copter.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("StarWyvernHead") || target.type == base.mod.NPCType("StarWyvernCollar") || target.type == base.mod.NPCType("StarWyvernNeck") || target.type == base.mod.NPCType("StarWyvernBody") || target.type == base.mod.NPCType("StarWyvernLeg") || target.type == base.mod.NPCType("StarWyvernTail1") || target.type == base.mod.NPCType("StarWyvernTail2") || target.type == base.mod.NPCType("StarWyvernTail3"))
				{
					Main.NewText("<Loreholder> A fully grown Star Serpent, and Nebuleus's pet.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
				if (target.type == base.mod.NPCType("Nebuleus") || target.type == base.mod.NPCType("BigNebuleus"))
				{
					Main.NewText("<Loreholder> ... I don't know... I just don't know. I can't see her past at all.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
				}
			}
		}
	}
}
