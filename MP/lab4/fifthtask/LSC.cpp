#include <iostream>
#include <cstring>
#include <algorithm>
#include "LSC.h"

using namespace std;

//РЕКУРСИВНАЯ ВЕРСИЯ
int lcs(int lenx, const char x[], int leny, const char y[])
{
    if (lenx == 0 || leny == 0)
        return 0;

    if (x[lenx - 1] == y[leny - 1])
        return 1 + lcs(lenx - 1, x, leny - 1, y);
    else
        return max(lcs(lenx, x, leny - 1, y),
            lcs(lenx - 1, x, leny, y));
}

//ДИНАМИЧЕСКОЕ ПРОГРАММИРОВАНИЕ

// Функция восстановления LCS
void buildLCS(int i, int j, const char x[], const char y[],
    int** dp, char z[], int& index)
{
    if (i == 0 || j == 0)
        return;

    if (x[i - 1] == y[j - 1])
    {
        buildLCS(i - 1, j - 1, x, y, dp, z, index);
        z[index++] = x[i - 1];
        z[index] = '\0';
    }
    else if (dp[i - 1][j] > dp[i][j - 1])
        buildLCS(i - 1, j, x, y, dp, z, index);
    else
        buildLCS(i, j - 1, x, y, dp, z, index);
}

int lcsd(const char x[], const char y[], char z[])
{
    int m = strlen(x);
    int n = strlen(y);

    // Создаём таблицу DP
    int** dp = new int* [m + 1];
    for (int i = 0; i <= m; i++)
        dp[i] = new int[n + 1];

    // Заполняем нулями
    for (int i = 0; i <= m; i++)
        for (int j = 0; j <= n; j++)
            dp[i][j] = 0;

    // Заполняем таблицу
    for (int i = 1; i <= m; i++)
    {
        for (int j = 1; j <= n; j++)
        {
            if (x[i - 1] == y[j - 1])
                dp[i][j] = dp[i - 1][j - 1] + 1;
            else
                dp[i][j] = max(dp[i - 1][j], dp[i][j - 1]);
        }
    }

    // Восстанавливаем LCS
    int index = 0;
    buildLCS(m, n, x, y, dp, z, index);
    z[index] = '\0';

    int result = dp[m][n];

    // Освобождаем память
    for (int i = 0; i <= m; i++)
        delete[] dp[i];
    delete[] dp;

    return result;
}