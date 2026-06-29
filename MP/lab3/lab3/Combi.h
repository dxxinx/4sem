#pragma once 
namespace combi
{
	struct  permutation
	{
		const static bool L = true;
		const static bool R = false;

		short  n,
			* sset;
		bool* dart;
		permutation(short n = 1);

		void reset();

		__int64 getfirst();
		__int64 getnext();

		short ntx(short i);
		unsigned __int64 np;

		unsigned __int64 count() const;
	};
};