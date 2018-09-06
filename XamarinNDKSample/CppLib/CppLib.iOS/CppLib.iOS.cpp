#include "CppLib.h"

extern "C" int Add_Integers(int a, int b)
{
	//call to CppLib.Shared.Add_Integers_Internal
	return Add_Integers_Internal(a, b);
}
