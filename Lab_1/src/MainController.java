import javafx.fxml.FXML;
import javafx.scene.chart.LineChart;
import javafx.scene.chart.XYChart;
import javafx.scene.control.Slider;

import java.io.*;
import java.util.Arrays;

class MainController {
    private static final String header = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?><worksheet version=\"3.0.3\" xmlns=\"http://schemas.mathsoft.com/worksheet30\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:ws=\"http://schemas.mathsoft.com/worksheet30\" xmlns:ml=\"http://schemas.mathsoft.com/math30\" xmlns:u=\"http://schemas.mathsoft.com/units10\" xmlns:p=\"http://schemas.mathsoft.com/provenance10\" xmlns:prd=\"http://schemas.mathsoft.com/point-release-data\"><metadata><userData><title>%s</title><description>%s</description><author>%s</author></userData></metadata><settings><presentation><textRendering><textStyles><textStyle name=\"Normal\"><blockAttr margin-left=\"0\" margin-right=\"0\" text-indent=\"inherit\" text-align=\"left\" list-style-type=\"inherit\" tabs=\"inherit\"/><inlineAttr font-family=\"Arial\" font-charset=\"0\" font-size=\"10\" font-weight=\"normal\" font-style=\"normal\" underline=\"false\" line-through=\"false\" vertical-align=\"baseline\"/></textStyle></textStyles></textRendering><mathRendering equation-color=\"#000\"><operators multiplication=\"narrow-dot\" derivative=\"derivative\" literal-subscript=\"large\" definition=\"colon-equal\" global-definition=\"triple-equal\" local-definition=\"left-arrow\" equality=\"bold-equal\" symbolic-evaluation=\"right-arrow\"/><mathStyles><mathStyle name=\"Variables\" font-family=\"Times New Roman\" font-charset=\"0\" font-size=\"10\" font-weight=\"normal\" font-style=\"normal\" underline=\"false\"/><mathStyle name=\"Constants\" font-family=\"Times New Roman\" font-charset=\"0\" font-size=\"10\" font-weight=\"normal\" font-style=\"normal\" underline=\"false\"/><mathStyle name=\"User 1\" font-family=\"Arial\" font-charset=\"0\" font-size=\"10\" font-weight=\"normal\" font-style=\"normal\" underline=\"false\"/><mathStyle name=\"User 2\" font-family=\"Courier New\" font-charset=\"0\" font-size=\"10\" font-weight=\"normal\" font-style=\"normal\" underline=\"false\"/><mathStyle name=\"User 3\" font-family=\"Arial\" font-charset=\"0\" font-size=\"10\" font-weight=\"bold\" font-style=\"normal\" underline=\"false\"/><mathStyle name=\"User 4\" font-family=\"Times New Roman\" font-charset=\"0\" font-size=\"10\" font-weight=\"normal\" font-style=\"italic\" underline=\"false\"/><mathStyle name=\"User 5\" font-family=\"Times New Roman\" font-charset=\"0\" font-size=\"10\" font-weight=\"normal\" font-style=\"normal\" underline=\"false\"/><mathStyle name=\"User 6\" font-family=\"Arial\" font-charset=\"0\" font-size=\"10\" font-weight=\"normal\" font-style=\"normal\" underline=\"false\"/><mathStyle name=\"User 7\" font-family=\"Times New Roman\" font-charset=\"0\" font-size=\"10\" font-weight=\"normal\" font-style=\"normal\" underline=\"false\"/><mathStyle name=\"Math Text Font\" font-family=\"Times New Roman\" font-charset=\"0\" font-size=\"14\" font-weight=\"normal\" font-style=\"normal\" underline=\"false\"/></mathStyles><dimensionNames mass=\"mass\" length=\"length\" time=\"time\" current=\"current\" thermodynamic-temperature=\"temperature\" luminous-intensity=\"luminosity\" amount-of-substance=\"substance\" display=\"false\"/><symbolics derivation-steps-style=\"vertical-insert\" show-comments=\"false\" evaluate-in-place=\"false\"/><results numeric-only=\"true\"><general precision=\"3\" show-trailing-zeros=\"false\" radix=\"dec\" complex-threshold=\"10\" zero-threshold=\"15\" imaginary-value=\"i\" exponential-threshold=\"3\"/><matrix display-style=\"auto\" expand-nested-arrays=\"false\"/><unit format-units=\"true\" simplify-units=\"true\" fractional-unit-exponent=\"false\"/></results></mathRendering><pageModel show-page-frame=\"false\" show-header-frame=\"false\" show-footer-frame=\"false\" header-footer-start-page=\"1\" paper-code=\"9\" orientation=\"portrait\" print-single-page-width=\"false\" page-width=\"595.5\" page-height=\"842.25\"><margins left=\"86.4\" right=\"86.4\" top=\"86.4\" bottom=\"86.4\"/><header use-full-page-width=\"false\"/><footer use-full-page-width=\"false\"/></pageModel><colorModel background-color=\"#fff\" default-highlight-color=\"#ffff80\"/><language math=\"ru\" UI=\"ru\"/></presentation><calculation><builtInVariables array-origin=\"0\" convergence-tolerance=\"0.001\" constraint-tolerance=\"0.001\" random-seed=\"1\" prn-precision=\"4\" prn-col-width=\"8\"/><calculationBehavior automatic-recalculation=\"true\" matrix-strict-singularity-check=\"true\" optimize-expressions=\"false\" exact-boolean=\"true\" strings-use-origin=\"false\" zero-over-zero=\"0\"><compatibility multiple-assignment=\"MC12\" local-assignment=\"MC12\"/></calculationBehavior><units><currentUnitSystem name=\"si\" customized=\"false\"/></units></calculation><editor view-annotations=\"false\" view-regions=\"false\"><ruler is-visible=\"false\" ruler-unit=\"cm\"/><grid granularity-x=\"6\" granularity-y=\"6\"/></editor><fileFormat image-type=\"image/png\" image-quality=\"75\" save-numeric-results=\"true\" exclude-large-results=\"false\" save-text-images=\"false\" screen-dpi=\"96\"/><miscellaneous><handbook handbook-region-tag-ub=\"1\" can-delete-original-handbook-regions=\"true\" can-delete-user-regions=\"true\" can-print=\"true\" can-copy=\"true\" can-save=\"true\" file-permission-mask=\"4294967295\"/></miscellaneous></settings><regions><region region-id=\"1\" left=\"0\" top=\"0\" width=\"0\" height=\"0\"><math>";
    private static final String matrix = "<ml:matrix rows=\"1\" cols=\"%d\">";
    private static final String matrixElement = "<ml:real>%f</ml:real>";
    private static final String footer = "</ml:matrix></math></region></regions></worksheet>";

    private static final String title = "КСКР Лаб 1";
    private static final String description = "Современные численные методы решения граничных задач";
    private static final String author = "Курако Кирилл";

    @FXML private Slider borderSlider;
    @FXML private LineChart<Number, Number> fieldLineChart;

    private CalculatorMKR calculatorMKR = new CalculatorMKR();
    private CalculatorMKE calculatorMKE = new CalculatorMKE();

    @FXML private void calculate() {
        double[] values1 = calculatorMKR.calculate(borderSlider.getValue());
        double[] values2 = calculatorMKE.calculate(borderSlider.getValue());

        int length = Math.min(values1.length, values2.length);

        saveArrayToMathcadXml(Arrays.copyOf(values1, length), "mkr.xmcd");
        saveArrayToMathcadXml(Arrays.copyOf(values2, length), "mke.xmcd");

        fieldLineChart.setCreateSymbols(false);
        fieldLineChart.getData().clear();

        XYChart.Series<Number, Number> series1 = new XYChart.Series<>();
        series1.setName("Температура стержня (МКР)");
        for (int i = 0; i < length; i++) {
            series1.getData().add(new XYChart.Data<>(i, values1[i]));
        }
        fieldLineChart.getData().add(series1);

        XYChart.Series<Number, Number> series2 = new XYChart.Series<>();
        series2.setName("Температура стержня (МКЕ)");
        for (int i = 0; i < length; i++) {
            series2.getData().add(new XYChart.Data<>(i, values2[i]));
        }
        fieldLineChart.getData().add(series2);
    }

    private void saveArrayToMathcadXml(double[] array, String filename) {
        try (FileWriter file = new FileWriter(filename, false)) {
            StringBuilder builder = new StringBuilder();
            builder.append(String.format(header, title, description, author));
            builder.append(String.format(matrix, array.length));
            for (double element : array) {
                builder.append(String.format(matrixElement, element));
            }
            builder.append(footer);
            file.write(builder.toString());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
