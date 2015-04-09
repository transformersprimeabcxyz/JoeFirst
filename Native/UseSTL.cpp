
#include <string>
#include <stack>
#include <vector>

using namespace std;

typedef stack<vector<int>> ExprStack;

#define MAX_EXPR = 26;

// getExpression - Converts expression with grouping into non-group expression
//
//	Example: INPUT: A6B4(AB2)3   => OUTPUT: A9B10
string getExpression(string inputExpr)
{
	stack<vector<int>> exprStack;

	ExprStack exprStack;

	for (string::iterator ch = inputExpr.begin(); ch != inputExpr.end(); ch++)
	{
		if (*ch == '(')
			;
		else if (*ch == ')')
			;
		else if (isdigit(*ch))
			;
		else if (isalpha(*ch))
			;
		else
			;
	}

	return nullptr;
}
