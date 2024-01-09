using System;
using AperionQB.Domain.Entities.QuickBooks;

namespace AperionQB.Application.Interfaces
{
	public interface IInfoHandler { 

		bool UpdateTokens(string a, string b, string c);
		bool updateIntuitInfo(string a, string b, string c);
		IntuitInfo getIntuitInfo();
		bool saveIntuitInfo(IntuitInfo info);
		string getIntuitInfoPath();
		void ensureEntryInDB();

    }
}