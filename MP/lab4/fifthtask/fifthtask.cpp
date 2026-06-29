#include <iostream>
#include <cstring>
#include <ctime>
#include <iomanip>
#include "LSC.h"

using namespace std;

// Функция для взятия первых n символов строки
string getPrefix(const string& s, int n) {
    return s.substr(0, n);
}

int main()
{
    setlocale(LC_ALL, "rus");

    // Исходные строки для варианта 3
    string X_full = "AYCVWES";
    string Y_full = "ECWMSP";

    cout << endl << "================================================" << endl;
    cout << "Сравнение рекурсивного и динамического методов LCS" << endl;
    cout << "Вариант 3: X = " << X_full << ", Y = " << Y_full << endl;
    cout << "================================================" << endl;

    cout << endl << "Длина | Рекурсия (мкс) | Динамическое (мкс)" << endl;
    cout << "----------------------------------------------" << endl;

    // Массивы для хранения результатов
    int lengths[] = { 2, 3, 4, 5, 6, 7 };
    double rec_times[6], dp_times[6];

    for (int idx = 0; idx < 6; idx++)
    {
        int n = lengths[idx];

        // Берём первые n символов из X и Y
        string X_prefix = getPrefix(X_full, n);
        string Y_prefix = getPrefix(Y_full, n);

        char z[100] = "";

        // Измеряем рекурсивный метод
        clock_t t1 = clock();
        int rec_result = lcs(X_prefix.length(), X_prefix.c_str(),
            Y_prefix.length(), Y_prefix.c_str());
        clock_t t2 = clock();
        double rec_time = (double)(t2 - t1);

        // Измеряем динамический метод
        clock_t t3 = clock();
        int dp_result = lcsd(X_prefix.c_str(), Y_prefix.c_str(), z);
        clock_t t4 = clock();
        double dp_time = (double)(t4 - t3);

        // Сохраняем результаты
        rec_times[idx] = rec_time;
        dp_times[idx] = dp_time;

        // Выводим результаты
        cout << "  " << n << "    |    " << setw(8) << rec_time << "    |    " << setw(8) << dp_time << endl;
    }

    cout << "----------------------------------------------" << endl;
    cout << endl << "Примечание: время в микросекундах (1 мс = 1000 мкс)" << endl;

    // Вывод финального результата для полных строк
    cout << endl << "================================================" << endl;
    cout << "Результат для полных строк:" << endl;

    char final_z[100] = "";
    int final_length = lcsd(X_full.c_str(), Y_full.c_str(), final_z);

    cout << "X = " << X_full << endl;
    cout << "Y = " << Y_full << endl;
    cout << "LCS = " << final_z << endl;
    cout << "Длина LCS = " << final_length << endl;
    cout << "================================================" << endl;

    return 0;
}