import java.util.ArrayList;
import java.util.Arrays;

class CalculatorMKE {

    // исходные данные задачи
    private int n, timeSteps;
    private double elementLength, c1, c2, ro1, ro2, lambda1, lambda2, A, tau, q, k, tempEnd, tempZero;

    // для вычислений
    private double[][] field;
    private double[][] ci = {{2.0, 1.0}, {1.0, 2.0}};   // коэффициенты для последующих вычислений
    private double[][] ki = {{1.0, -1.0}, {-1.0, 1.0}}; //

    CalculatorMKE() {
        this.n = 100;                   // Кол-во элементов
        this.timeSteps = 100;           // Шаги по времени
        double length = 0.3;            // Длина стрежня
        double timeEnd = 30;            // Время
        this.lambda1 = 0.22; // 393     // Коэффициенты теплопроводности
        this.lambda2 = 105; // 84       //
        this.ro1 = 860; // 7800         // Плотности материалов
        this.ro2 = 7750; // 2800        //
        this.c1 = 1930; // 500          // Теплоемкость вроде
        this.c2 = 400; // 920
        this.q = 1e6;                   // Тепловой поток
        this.k = 100;                   // коэф теплоотдачи
        this.A = 1;
        this.tempEnd = 20;              // Температура внутри стержня
        this.tempZero = 20;             // Начальная температура

        this.elementLength = length / 100;
        this.tau = timeEnd / timeSteps;
    }

    double[] calculate(double borderPercent) {
        field = new double[timeSteps + 1][n + 1];       // Наши результаты
        Arrays.fill(field[0], tempZero);

        double[][] C = new double[n + 1][n + 1];        // Матрица коэффициентов для вычисления
        double ciCoef1 = (c1 * ro1 * A * elementLength) / 6;
        double ciCoef2 = (c2 * ro2 * A * elementLength) / 6;
        fillWithCoefficients(ciCoef1, ci, C, 0, (int)((C.length - 1) * (borderPercent / 100.0)));               // borderPercent - соотношение материалов к длине стержня
        fillWithCoefficients(ciCoef2, ci, C, (int)((C.length - 1) * (borderPercent / 100.0)), C.length - 1);

        double[][] K = new double[n + 1][n + 1];        // Матрица коэффициентов для вычисления
        double kiCoef1 = (A * lambda1) / elementLength;
        double kiCoef2 = (A * lambda2) / elementLength;
        fillWithCoefficients(kiCoef1, ki, K, 0, (int)((K.length - 1) * (borderPercent / 100.0)));
        fillWithCoefficients(kiCoef2, ki, K, (int)((K.length - 1) * (borderPercent / 100.0)), K.length - 1);

        for (int i = 0; i < timeSteps; i++) {
            double[][] B = additionOfMatrices(K, 1, C, 2 / tau);            // Левая граница стержня с начальными условиями
            double[][] P = additionOfMatrices(K, -1, C, 2 / tau);

            double[] F = new double[n + 1];

            double leftBorder = fi(0, 0, k, tempEnd, field[i][0], elementLength, A);     // Левая граница стержня
            F[0] = leftBorder;
            F[1] = leftBorder;
            
            double rightBorder = fi(0, q, 0, 0, field[i][0], elementLength, A);     // Правая граница стрежня
            F[n - 1] = rightBorder;
            F[n] = rightBorder;

            double[] tmpT = new double[field[0].length];                                          // Просто времееный массив
            System.arraycopy(field[i], 0, tmpT, 0, tmpT.length);                            // Копирование значений

            double[] ot = matrixMultiplicationElemByElem(P, 1, tmpT, 1);
            double[] right = addingVectors(ot, 1, F, 2);                    // Резултат правой границы стрежня

            //gaussMethod(B, right, i + 1);
            gaussZeidelMethod(B, right,i + 1);                                                  // Расчет результата
        }
        return field[field.length - 1];
    }

    private void fillWithCoefficients(double coef, double[][] local, double[][] global, int beginIndex, int endIndex) {
        double[][] tmp = new double[local.length][local[0].length];
        for (int i = beginIndex; i < endIndex; i++) {
            for (int j = 0; j < local.length; j++) {
                System.arraycopy(local[j], 0, tmp[j], 0, local[0].length);
            }
            multiplicationMatrixByConstant(tmp, coef);
            matrixAdditionByIndex(global, tmp, i);
        }
    }

    private void multiplicationMatrixByConstant(double[][] matrix, double constant) {
        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix[0].length; j++) {
                matrix[i][j] *= constant;
            }
        }
    }

    private void matrixAdditionByIndex(double[][] a, double[][] b, int index) {
        for (int i = index; i < index + b.length; i++) {
            for (int j = index; j < index + b[0].length; j++) {
                a[i][j] += b[i - index][j - index];
            }
        }
    }

    private double[][] additionOfMatrices(double[][] a, double aMultiplier,
                                          double[][] b, double bMultiplier) {
        double[][] result = new double[a.length][a[0].length];
        for (int i = 0; i < a.length; i++) {
            for (int j = 0; j < a[0].length; j++) {
                result[i][j] = aMultiplier * a[i][j] + bMultiplier * b[i][j];
            }
        }
        return result;
    }

    private double[] addingVectors(double[] a, double aMultiplier,
                                   double[] b, double bMultiplier) {
        double[] result = new double[a.length];
        for (int i = 0; i < a.length; i++) {
            result[i] = aMultiplier * a[i] + bMultiplier * b[i];
        }
        return result;
    }

    private double[] matrixMultiplicationElemByElem(double[][] a, double aMultiplier,
                                                    double[] b, double bMultiplier) {
        double[] result = new double[a.length];
        for (int i = 0; i < a.length; i++) {
            for (int j = 0; j < a[0].length; j++) {
                result[i] += aMultiplier * a[i][j] * bMultiplier * b[j];
            }
        }
        return result;
    }

    // Функция граничных условий
    private double fi(double pQ, double pq, double ph, double pTs, double pT, double pL, double pA) {
        return (pQ * pA * pL - pq * pA + ph * pA * (pT - pTs)) * (-0.5);
    }

    private void gaussMethod(double[][] a, double[] b, int ti) {
        for (int i = 0; i < a.length; i++) { //выполнение прямого хода Метода Гауса
            /*получение треугольной матрицы*/
            for (int j = i + 1; j < a.length; j++) {
                double lokKaf = a[j][i] / a[i][i]; //определение коэффициента домножения
                for (int k = 0; k < a.length; k++) { //отнимание строк
                    a[j][k] = a[j][k] - a[i][k] * lokKaf;
                }
                b[j] = b[j] - b[i] * lokKaf;
            }
        }
        /*-------обратный ход метода Гауса---------*/
        int ln = a.length; //размерность матрицы
        for (int i = ln - 1; i >= 0; i--) {
            double hh = b[i]; // hh присваиваем ответ из массива сил
            for (int j = 0; j < ln; j++) {
                hh = hh - field[ti][j] * a[i][j]; //вычитание элементов строки матрицы
            }
            field[ti][i] = hh / a[i][i]; //hh  делим на коэффициент при иксу
        }
    }

    private void gaussZeidelMethod(double[][] a, double[] b,  int ti){
        double[] p = new double[a.length];
        double[] x = new double[a.length];

        Arrays.fill(x, 0);

        do {
            for (int i = 0; i < a[0].length; i++) {
                p[i] = x[i];
            }
            for (int i = 0; i < a[0].length; i++) {
                double temp = 0;
                for (int j = 0; j < i; j++){
                    temp += a[i][j] * x[j];
                }
                for (int j = i + 1; j < a[0].length; j++){
                    temp += a[i][j] * p[j];
                }

                x[i] = (b[i] - temp) / a[i][i];

            }
        }while (!coverage(x, p, a[0].length, 0.0001));

        for (int i = a[0].length - 1; i >= 0 ; i--) {
            field[ti][i] = x[i];
        }
    }
    private boolean coverage(double[] xn, double[] xk, int size, double eps){
        double norm = 0;
        for (int i = 0; i < size; i++) {
            norm += (xn[i] - xk[i]) * (xn[i] - xk[i]);
        }
        System.out.println(Math.sqrt(norm) < eps);
        return Math.sqrt(norm) < eps;
    }
}
