public class Complex {
    private double real, imaginary;

    public Complex(double real, double imaginary) {
        this.real = real;
        this.imaginary = imaginary;
    }

    public void add(Complex z) {
        set(add(this,z));
    }

    public void multiply(Complex z) {
        set(multiply(this,z));
    }

    public void set(Complex z) {
        this.real = z.real;
        this.imaginary = z.imaginary;
    }

    public static Complex add(Complex z1, Complex z2) {
        return new Complex(z1.real + z2.real, z1.imaginary + z2.imaginary);
    }

    public static Complex multiply(Complex z1, Complex z2) {
        double _real = z1.real*z2.real - z1.imaginary*z2.imaginary;
        double _imaginary = z1.real*z2.imaginary + z1.imaginary*z2.real;
        return new Complex(_real,_imaginary);
    }

    public double mod() {
        return Math.sqrt(Math.pow(this.real,2) + Math.pow(this.imaginary,2));
    }

    public static Complex exp(Complex z) {
        double a = z.real;
        double b = z.imaginary;
        double r = Math.exp(a);
        a = r*Math.cos(b);
        b = r*Math.sin(b);
        return new Complex(a,b);
    }

    public double getRe() {
        return real;
    }

    public double getIm() {
        return imaginary;
    }
}