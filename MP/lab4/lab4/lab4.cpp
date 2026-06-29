#include "stdafx.h"

int _tmain(int argc, _TCHAR* argv[])
{
    setlocale(LC_ALL, "rus");
    srand((unsigned int)time(NULL));

    const int len1 = 300;
    const int len2 = 200;
    char s1[len1 + 1];
    char s2[len2 + 1];

    for (int i = 0; i < len1; i++) s1[i] = 'a' + rand() % 26;
    s1[len1] = '\0';
    for (int i = 0; i < len2; i++) s2[i] = 'a' + rand() % 26;
    s2[len2] = '\0';

    std::cout << "строка s1 (300 симв): " << std::string(s1).substr(0, 300) << "\n";
    std::cout << "строка s2 (200 симв): " << std::string(s2).substr(0, 200) << "\n";

    std::cout << "\nсравнительный анализ времени (мс)" << std::endl;
    std::cout << std::left << std::setw(15) << "доля k"
        << std::setw(15) << "рекурсия"
        << "дин. програм." << std::endl;
    std::cout << "\n";

    double k_values[] = { 1.0 / 25, 1.0 / 20, 1.0 / 15, 1.0 / 10, 1.0 / 5, 1.0 / 2, 1.0 };
    const char* k_names[] = { "1/25", "1/20", "1/15", "1/10", "1/5", "1/2", "1" };

    for (int i = 0; i < 7; i++)
    {
        int lx = (int)(len1 * k_values[i]);
        int ly = (int)(len2 * k_values[i]);

        clock_t t1 = 0, t2 = 0, t3 = 0, t4 = 0;

        t3 = clock();
        levenshtein(lx, s1, ly, s2);
        t4 = clock();

        if (lx <= 15 && ly <= 15) {
            t1 = clock();
            levenshtein_r(lx, s1, ly, s2);
            t2 = clock();
            std::cout << std::left << std::setw(15) << k_names[i]
                << std::setw(15) << ((double)(t2 - t1) / CLOCKS_PER_SEC * 1000)
                << ((double)(t4 - t3) / CLOCKS_PER_SEC * 1000) << std::endl;
        }
        else {
            std::cout << std::left << std::setw(15) << k_names[i]
                << std::setw(15) << "> 1 мин"
                << ((double)(t4 - t3) / CLOCKS_PER_SEC * 1000) << std::endl;
        }
    }
    std::cout << std::endl;
    system("pause");
    return 0;
}