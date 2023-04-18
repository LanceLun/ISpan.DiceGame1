using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISpan.DiceGame
{
	public abstract class BaseDiceGame : IDiceGame
	{
		protected List<Dice> _dices = new List<Dice>();

		public string Title { get; }

		public BaseDiceGame(string title)
		{
			Title = title;
		}

		public abstract void Play();


		public virtual int ComputePoints()
		{
			return _dices.Sum(dice => dice.Value);
		}


		public abstract string GetInformation();
		public void EmptyDices()
		{
			_dices.Clear();
		}
	}
}
