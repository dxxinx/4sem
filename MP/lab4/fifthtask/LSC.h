#pragma once
#include <string>

using namespace std;

// Рекурсивное вычисление длины LCS
int lcs(int lenx, const char x[], int leny, const char y[]);

// Динамическое вычисление LCS (возвращает длину и саму подпоследовательность)
int lcsd(const char x[], const char y[], char z[]);