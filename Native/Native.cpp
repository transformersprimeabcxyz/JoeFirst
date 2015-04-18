// Native.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

bool canBePalindrome(TCHAR* inputStr);

int _tmain(int argc, _TCHAR* argv[])
{
	_tprintf(_T("%s %s be a palindrome"), argv[1], canBePalindrome(argv[1]) ? _T("can") : _T("cannot"));
	return 0;
}

bool canBePalindrome(TCHAR* inputStr)
{
	bool charSet[26];

	for (int i = 0; i < 26; i++)
		charSet[i] = false;

	int strLen = _tcslen(inputStr);
	if (strLen < 1)
		return false;

	for (int i = 0; i < strLen; i++)
	{
		TCHAR ch = _totlower(inputStr[i]);

		if ((ch < 'a') || (ch > 'z'))
			return false;

		charSet[ch - 'a'] = !charSet[ch - 'a'];
	}

	bool fOddFound = false;
	for (int i = 0; i < 26; i++)
	{
		if (charSet[i])
		{
			if (fOddFound)
				return false;
			fOddFound = true;
		}
	}
	return true;
}

