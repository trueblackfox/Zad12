using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задание_12
{
    class Program
    {
        static void WriteMas(int[] mas)
        {
            for (int i = 0; i < mas.Length; i++)
            {
                Console.Write(mas[i] + " ");
            }
            Console.WriteLine();
        }

        static void SimpleChoiceSorting(int[] mas)
        {//сортировка простым выбором
            int compare = 0;
            int changes = 0;
            int min;//минимальный элемент в неотсортированной части массива
            int mini;//индекс минимального элемента в неотсортированной части массива
            for (int i = 0; i < mas.Length; i++)//сдвигаем край отсортированной части массива вправо
            {
                compare++;
                min = mas[i];
                mini = i;
                for (int j = i + 1; j < mas.Length; j++)//перебираем неотсортированную часть массива
                {
                    compare++;
                    if (min > mas[j])//ищем минимальный элемент в неотсортированной части
                    {
                        min = mas[j];
                        mini = j;//запоминем его индекс                       
                    }
                    compare++;
                }
                compare++;
                int k = mas[i];//вспомогательная переменная для пересылки
                mas[i] = min;//меняем минимальный элемент и первый в неотсортированной части местами
                mas[mini] = k;
                if (mini != i)
                    changes++;
            }
            compare++;
            Console.WriteLine("Массив, отсортированный с методом простого выбора: ");

            // WriteMas(mas);

            Console.WriteLine("Количество сравнений: " + compare);
            Console.WriteLine("Количество пересылок: " + changes);
        }



        static int[] MakeSortedTree(int[] mas, int maxElem, int elem, ref int compare, ref int changes)

        {//функция, строящая сортировочное дерево

            int maxDescendant = elem;//индекс максимального из двух потомков данного элемента

            int LeftDescendant = elem * 2 + 1;//индекс левого потомка данного элемента

            int RightDescendant = elem * 2 + 2;//индекс правого потомка данного элемента

            while (LeftDescendant < maxElem)//пока не дойдем до границы неотсортированной части массива
            {
                compare++;
                if (RightDescendant >= maxElem)//если мы дошли до последнего элемента в неотсортированной части и правый потомок уже в отсортированной части
                {
                    maxDescendant = LeftDescendant;

                }
                else//если мы еще не дошли до конца неотсортированной части и правый потомок тоже находится в ней
                if (mas[LeftDescendant] > mas[RightDescendant])//находим максимальный элемент и  записываем его индекс в maxDescendant
                {
                    maxDescendant = LeftDescendant;
                    compare++;
                }
                else
                {
                    maxDescendant = RightDescendant;
                    compare++;
                }
                compare++;
                if (mas[maxDescendant] <= mas[elem])//если потомок не больше данного элемента, то заканчивам построение сортировочного дерева
                {
                    compare++;
                    break;
                }
                else//если потомок больше данного элемента, то меняем их местами
                {
                    int k = mas[elem];//вспомогательна переменная
                    mas[elem] = mas[maxDescendant];
                    mas[maxDescendant] = k;
                    changes++;
                    elem = maxDescendant;//данный элемент меняет индекс
                    LeftDescendant = elem * 2 + 1;//индекс левого потомка данного элемента
                    RightDescendant = elem * 2 + 2;//индекс правого потомка данного элемента
                }
                compare++;
            }
            compare++;
            return mas;
        }

        static void PyramidalSorting(int[] mas)
        {//функция, в которой выполняется два этапа пирамидальной сортировки
            int changes = 0;
            int compare = 0;
            //первый этап: составляем пирамиду, прогоняя по не ней элементы, имеющие потомков, начиная с самого нижнего

            for (int i = mas.Length / 2 - 1; i >= 0; i--)
            {
                compare++;
                mas = MakeSortedTree(mas, mas.Length, i, ref compare, ref changes);//нижней границы пока нет, так как массив еще не начали сортировать               
            }
            compare++;
            //второй этап: меняем местами первый и последний в неотсортированной части, затем прогоняем новый верхний элемент, составляя пирамиду
            for (int i = mas.Length - 1; i >= 1; i--)
            {
                compare++;
                //меняем местами верхний и нижний элементы неотсортированной части (последний элемент - новый край отсортированной части)
                int k = mas[i];
                mas[i] = mas[0];
                mas[0] = k;
                changes++;
                //составляем пирамиду, передвинув нижний край на один влево. Прогоняем по ней верхний элемент
                mas = MakeSortedTree(mas, i, 0, ref compare, ref changes);
            }
            compare++;
            Console.WriteLine("Массив, отсортированный с помощью пирамидальной сортировки:");
            //WriteMas(mas);
            Console.WriteLine("Количество сравнений: " + compare);
            Console.WriteLine("Количество пересылок: " + changes);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            const int N = 10000;
            Random rnd = new Random();
            int[] mas = new int[N];
            int[] mas1 = new int[N];
            int[] mas2 = new int[N];
            for (int i = 0; i < N; i++)
            {
                mas[i] = rnd.Next(-N, N);
                mas1[i] = 50 - i;
                mas2[i] = i - 50;
            }

            //массив неупорядоченный
            Console.WriteLine("Массив из 10000 элементов, неупорядоченный:");
            //WriteMas(mas);
            SimpleChoiceSorting(mas);
            PyramidalSorting(mas);
            Console.ReadLine();
            //массив упорядоченный по убыванию
            Console.WriteLine("Массив из 10000 элементов, упорядоченный по убыванию:");
            //WriteMas(mas1);
            SimpleChoiceSorting(mas1);
            PyramidalSorting(mas1);
            Console.ReadLine();
            //массив упорядоченный по возрастанию

            Console.WriteLine("Массив из 10000 элементов, упорядоченный по возрастанию:");
            //WriteMas(mas2);
            SimpleChoiceSorting(mas2);
            PyramidalSorting(mas2);
            Console.ReadLine();
        }
    }
}
