import java.awt.image.BufferedImage;

public class ImageProcessor {

    private BufferedImage bufferedImage;
    private MainController.ShowImage showImageAfterConversion;

    public ImageProcessor(BufferedImage bufferedImage, MainController.ShowImage showImageAfterConversion) {
        this.bufferedImage = bufferedImage;
        this.showImageAfterConversion = showImageAfterConversion;
    }

    private static BufferedImage convertToBlackWhite(BufferedImage bufferedImage) {
        BufferedImage image = new BufferedImage(bufferedImage.getWidth(),
                bufferedImage.getHeight(), bufferedImage.getType());
        for (int i = 0; i < bufferedImage.getWidth(); i++) {
            for (int j = 0; j < bufferedImage.getHeight(); j++) {
                int color = bufferedImage.getRGB(i, j);
                int blue = color & 0xff;
                int green = (color & 0xff00) >> 8;
                int red = (color & 0xff0000) >> 16;
                int bright = (int) (red * 0.299 + green * 0.587 + blue * 0.114);
                image.setRGB(i, j, bright << 16 | bright << 8 | bright);
            }
        }
        return image;
    }

    public void fft() {
        BufferedImage image = convertToBlackWhite(bufferedImage);
        BufferedImage performedImage = new BufferedImage(image.getWidth(),
                image.getHeight(), image.getType());
        int width = performedImage.getWidth();
        int height = performedImage.getHeight();
        final Complex doublePi = Complex.multiply(new Complex(-2 * Math.PI, 0),
                new Complex(0, 1));
        int halfWidth = width / 2;
        int halfHeight = height / 2;
        int[][] buf = new int[width][height];
        double max = -1;
        for (int k = 0; k < width; k++) {
            for (int l = 0; l < height; l++) {
                Complex sc = new Complex(0, 0);
                for (int i = 0; i < width; i++) {
                    for (int j = 0; j < height; j++) {
                        Complex t = new Complex((image.getRGB(i, j) & 0xff), 0);
                        Complex top = new Complex(((double) i * k) / width + ((double) j * l) / height, 0);
                        top.multiply(doublePi);
                        t.multiply(Complex.exp(top));
                        sc.add(t);
                    }
                }
                double t = Math.log(sc.mod()) * 10.0;
                max = Math.max(t, max);
                int x = k + halfWidth * (k < halfWidth ? 1 : -1);
                int y = l + halfHeight * (l < halfHeight ? 1 : -1);
                buf[x][y] = (int) t;
            }
        }
        for (int k = 0; k < width; k++) {
            for (int l = 0; l < height; l++) {
                int t = (int) (((double)buf[k][l] / max) * 255.0);
                performedImage.setRGB(k, l, t << 16 | t << 8 | t);
            }
        }
        showImageAfterConversion.show(performedImage);
    }

    public void reverseFft() {
        BufferedImage image = convertToBlackWhite(bufferedImage);
        BufferedImage performedImage = new BufferedImage(image.getWidth(),
                image.getHeight(), image.getType());
        int width = performedImage.getWidth();
        int height = performedImage.getHeight();
        final Complex doublePi = Complex.multiply(new Complex(-2 * Math.PI, 0),
                new Complex(0, 1));
        Complex[][] buf = new Complex[width][height];
        for (int k = 0; k < width; k++) {
            for (int l = 0; l < height; l++) {
                Complex sc = new Complex(0, 0);
                for (int i = 0; i < width; i++) {
                    for (int j = 0; j < height; j++) {
                        Complex t = new Complex((image.getRGB(i, j) & 0xff), 0);
                        Complex top = new Complex(((double) i * k) / width + ((double) j * l) / height, 0);
                        top.multiply(doublePi);
                        t.multiply(Complex.exp(top));
                        sc.add(t);
                    }
                }
                buf[k][l] = sc;
            }
        }
        final int wh = width * height;
        final Complex iDoublePi = Complex.multiply(new Complex(2 * Math.PI, 0),
                new Complex(0, 1));
        for (int k = 0; k < width; k++) {
            for (int l = 0; l < height; l++) {
                Complex sc = new Complex(0, 0);
                for (int i = 0; i < width; i++) {
                    for (int j = 0; j < height; j++) {
                        Complex t = new Complex(buf[i][j].getRe(), buf[i][j].getIm());
                        Complex top = new Complex(((double) i * k) / width + ((double) j * l) / height, 0);
                        top.multiply(iDoublePi);
                        t.multiply(Complex.exp(top));
                        sc.add(t);
                    }
                }
                int t = (int) sc.getRe() / wh;
                performedImage.setRGB(k, l, t << 16 | t << 8 | t);
            }
        }
        showImageAfterConversion.show(performedImage);
    }

    public void battervort(int kernelSize, double sigma, int n) {
        int halfsize = (kernelSize - 1) / 2;
        double[][] battervortKernel = new double[kernelSize][kernelSize];
        double x = -halfsize;
        for (int i = 0; i < kernelSize; i++) {
            double y = -halfsize;
            for (int j = 0; j < kernelSize; j++) {
                battervortKernel[i][j] = 1 / (1 + Math.pow((x + y) / (Math.PI * sigma), 2 * n));
                y += 1.0;
            }
            x += 1.0;
        }
        convolve(battervortKernel, kernelSize, halfsize);
    }

    public void gaussian(int kernelSize, double sigma) {
        int halfsize = (kernelSize - 1) / 2;
        double[][] gaussianKernel = new double[kernelSize][kernelSize];
        double x = -halfsize;
        for (int i = 0; i < kernelSize; i++) {
            double y = -halfsize;
            for (int j = 0; j < kernelSize; j++) {
                //gaussianKernel[i][j] = Math.exp(-(x * x + y * y) / (2 * sigma * sigma)) / (2 * Math.PI * sigma * sigma);
                gaussianKernel[i][j] = 1 - Math.exp(-(x * x + y * y) / (2 * Math.PI * sigma * sigma));
                y += 1.0;
            }
            x += 1.0;
        }
        convolve(gaussianKernel, kernelSize, halfsize);
    }

    private void convolve(double[][] kernel, int kernelSize, int halfsize) {
        BufferedImage image = convertToBlackWhite(bufferedImage);
        BufferedImage performedImage = new BufferedImage(image.getWidth(),
                image.getHeight(), image.getType());
        int width = performedImage.getWidth();
        int height = performedImage.getHeight();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                double sum = 0;
                double kernelSum = 0;
                for (int k = Math.max(i - halfsize, 0); k < Math.min(i + halfsize, width - 1); k++) {
                    for (int l = Math.max(j - halfsize, 0); l < Math.min(j + halfsize, height - 1); l++) {
                        sum += (image.getRGB(k, l) & 0xff) * kernel[k % 5][l % 5];
                        kernelSum += kernel[k % kernelSize][l % kernelSize];
                    }
                }
                sum /= kernelSum;
                performedImage.setRGB(i, j, (int)sum << 16 | (int)sum << 8 | (int)sum);
            }
        }
        showImageAfterConversion.show(performedImage);
    }
}
