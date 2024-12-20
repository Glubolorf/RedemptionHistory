using System;
using Microsoft.Xna.Framework;

namespace Redemption.StructureHelper.ChestHelper.GUI
{
	internal class GuaranteedRuleElement : ChestRuleElement
	{
		public GuaranteedRuleElement() : base(new ChestRuleGuaranteed())
		{
			this.color = new Color(200, 0, 0);
		}

		public GuaranteedRuleElement(ChestRule rule) : base(rule)
		{
			this.color = new Color(200, 0, 0);
		}
	}
}
