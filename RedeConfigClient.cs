using System;
using Terraria.ModLoader.Config;

namespace Redemption
{
	public class RedeConfigClient : ModConfig
	{
		public override ConfigScope Mode
		{
			get
			{
				return 1;
			}
		}

		public static RedeConfigClient Instance;

		[Label("No Combat Text")]
		[Tooltip("Disables combat text, the text that appears above an enemies head, e.g. when a chicken pecks the ground")]
		public bool NoCombatText;

		[Label("Enable Challenge Cores")]
		[Tooltip("Newly created players will have an Empty Core in their inventory, these were used as challenges but their execution was poor")]
		public bool StarterCore;

		[Label("[c/ff0000:Disable Lore Elements]")]
		[Tooltip("WARNING: Disables all boss dialogue, alignment, and certain events from the mod (Don't use while a boss is alive)\nThis mod is heavily based on the lore, so I would not recommend this.")]
		public bool NoLoreElements;

		[Label("Anti-Antti (Disables Antti's music)")]
		[Tooltip("Youtubers will get claimed when using that music, so if you're a Youtuber, enable this. (Getting claimed will NOT affect your channel, unlike copyright strikes)")]
		public bool AntiAntti;

		[Label("Music Replacements for Claimable Music")]
		[Tooltip("Requires enabling 'Anti-Antti' config option. Replaces some of the music that would claim YouTube videos with free-to-use alternatives, to still make fights enjoyable.")]
		public bool MusicReplacements;

		[Label("I'm Arachnophobic!")]
		[Tooltip("Disables Forest Spiders and other spiders from this mod from spawning, won't disable vanilla game's spiders.")]
		public bool NoSpidersInMyTerrariaMod;

		[Label("Disable Druid Class Tag")]
		[Tooltip("Disables the -Druid Class- tooltip that is under druid item names")]
		public bool NoDruidClassTag;

		[Label("Disable Nature Guardian Info")]
		[Tooltip("Disables the ---Guardian Info--- tooltip and contents under it, so you won't know what guardians do what!")]
		public bool NoGuardianInfo;

		[Label("Enable Zelda-Styled Boss Titles")]
		[Tooltip("Enables the Legend of Zelda-style boss introduction text for bosses. (Thanks to Seraph for the code)")]
		public bool BossIntroText;

		[Label("Disable Camera Lock")]
		[Tooltip("Disables the locked camera during specific boss fights")]
		public bool CameraLockDisable;
	}
}
