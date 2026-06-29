#include <iostream>
#include <iomanip> 
#include "TSP.h"
#define N 5

using namespace std;

int main()
{
	setlocale(LC_ALL, "rus");
	int d[N][N] = {
					{ INF, 3, 22, 6, 19 },
					{ 4, INF, 15, 37, 65 }, 
					{ 10, 6, INF, 72, 40 },   
					{ 21, 52, 9, INF, 6 }, 
					{ 82, 35, 45, 15, INF } 
	};
	int r[N];
	int s = salesman(
		N,
		(int*)d,
		r
	);
	cout << "\n-- Задача коммивояжера -- ";
	cout << "\n-- количество городов: " << N;
	cout << "\n-- матрица расстояний : ";

	for (int i = 0; i < N; i++)
	{
		cout << "\n";
		for (int j = 0; j < N; j++)

			if (d[i][j] != INF) cout << setw(3) << d[i][j] << " ";

			else cout << setw(3) << "INF" << " ";
	}

	cout << "\n-- оптимальный маршрут: ";
	for (int i = 0; i < N; i++)
		cout << r[i] + 1 << "-->";

	cout << 1;
	cout << "\n-- длина маршрута     : " << s << "\n";
	system("pause");
	return 0;
}
