import java.util.Arrays;

class CalculatorMKR {

    // исходные данные задачи
    private int n;
    private double timeEnd, lambda1, ro1, c1, lambda2, ro2, c2, q, k, tempEnd, tempZero;

    // для вычислений
    private double h, a1, a2, tau;
    private double[] field, alpha, beta;

    CalculatorMKR() {
        this.n = 100;
        this.timeEnd = 30;
        double length = 0.3;
        this.lambda1 = 105; // 84
        this.lambda2 = 0.22; // 393
        this.ro1 = 7750; // 2800
        this.ro2 = 860; // 7800
        this.c1 = 400; // 920
        this.c2 = 1930; // 500
        this.q = 1e6;
        this.k = 100;
        this.tempEnd = 20;
        this.tempZero = 20;

        h = length / (n - 1.0);
        a1 = lambda1 / (ro1 * c1);
        a2 = lambda2 / (ro2 * c2);
        tau = timeEnd / 100.0;

        field = new double[n];
        alpha = new double[n];
        beta = new double[n];
    }

    double[] calculate(double borderPercent) {
        Arrays.fill(field, tempZero);
        int border = (int) (n * ((100.0 - borderPercent) / 100.0));
        if (border == 0) {
            border++;
        } else {
            border--;
        }

        double time = 0.0;
        while (time < timeEnd) {
            time += tau;
            alpha[0] = 2.0 * a1 * tau / (2.0 * a1 * tau + h * h);
            beta[0] = (h * h * field[0] + 2.0 * a1 * tau * h * q / lambda1) / (2.0 * a1 * tau + h * h);
            for (int i = 1; i < border; i++) {
                double ai, bi, ci, fi;
                ai = lambda1 / (h * h);
                bi = 2.0 * lambda1 / (h * h) + ro1 * c1 / tau;
                ci = lambda1 / (h * h);
                fi = -ro1 * c1 * field[i] / tau;
                alpha[i] = ai / (bi - ci * alpha[i - 1]);
                beta[i] = (ci * beta[i - 1] - fi) / (bi - ci * alpha[i - 1]);
            }
            alpha[border] = 2.0 * a1 * a2 * tau * lambda2 / (2.0 * a1 * a2 * tau * (lambda2 + lambda1 *
                    (1 - alpha[border - 1])) + (h * h) * (a1 * lambda2 + a2 * lambda1));
            beta[border] = (2.0 * a1 * a2 * tau * lambda1 * beta[border - 1] + h * h * (a1 * lambda2 + a2 * lambda1)
                    * field[border]) / (2.0 * a1 * a2 * tau * (lambda2 + lambda1 * (1 - alpha[border - 1]))
                    + h * h * (a1 * lambda2 + a2 * lambda1));
            for (int i = border + 1; i < n - 1; i++) {
                double ai, bi, ci, fi;
                ai = lambda2 / (h * h);
                bi = 2.0 * lambda2 / (h * h) + ro2 * c2 / tau;
                ci = lambda2 / (h * h);
                fi = -ro2 * c2 * field[i] / tau;
                alpha[i] = ai / (bi - ci * alpha[i - 1]);
                beta[i] = (ci * beta[i - 1] - fi) / (bi - ci * alpha[i - 1]);
            }
            field[n - 1] = (lambda2 * h * h * field[n - 1] + 2.0 * a2 * tau * (lambda2 * beta[n - 2] + k * h * tempEnd))
                    / (lambda2 * h * h + 2.0 * a2 * tau * (lambda2 * (1 - alpha[n - 2]) + k * h));
            for (int i = n - 2; i >= 0; i--) {
                field[i] = alpha[i] * field[i + 1] + beta[i];
            }
        }
        for (int i = 0; i < field.length / 2; i++) {
            double temp = field[i];
            field[i] = field[field.length - i - 1];
            field[field.length - i - 1] = temp;
        }
        return field;
    }
}
