import javafx.embed.swing.SwingFXUtils;
import javafx.fxml.FXML;
import javafx.scene.control.RadioButton;
import javafx.scene.control.ToggleGroup;
import javafx.scene.image.ImageView;
import javafx.stage.FileChooser;
import javafx.stage.Stage;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

public class MainController {

    private Stage stage;

    private ImageProcessor imageProcessor = null;

    @FXML private ImageView originalImageView;
    @FXML private ImageView resultImageView;

    @FXML private RadioButton fftRadioButton;
    @FXML private RadioButton reverseFftRadioButton;
    @FXML private RadioButton battervortRadioButton;
    @FXML private RadioButton gaussianRadioButton;

    MainController(Stage stage) {
        this.stage = stage;
    }


    @FXML
    private void openImage() {
        FileChooser chooser = new FileChooser();
        chooser.setTitle("Открыть изображение");
        File file = chooser.showOpenDialog(stage);
        if (file == null) {
            return;
        }
        BufferedImage bufferedImage = null;
        try {
            bufferedImage = ImageIO.read(file);
        } catch (IOException ex) {
            ex.printStackTrace();
        }
        if (bufferedImage == null) {
            return;
        }
        resetRadioButtons();
        originalImageView.setImage(SwingFXUtils.toFXImage(bufferedImage, null));
        imageProcessor = new ImageProcessor(bufferedImage,
                bufferedImageAfterConversion -> resultImageView.setImage(SwingFXUtils.toFXImage(bufferedImageAfterConversion, null)));
    }

    @FXML
    private void performConversion() {
        if (imageProcessor != null) {
            if (fftRadioButton.isSelected()) {
                imageProcessor.fft();
            } else if (reverseFftRadioButton.isSelected()) {
                imageProcessor.reverseFft();
            } else if (battervortRadioButton.isSelected()) {
                imageProcessor.battervort(15, 4, 1);
            } else if (gaussianRadioButton.isSelected()) {
                imageProcessor.gaussian(5, 4);
            }
        }
    }

    void setRadioButtonsGroup() {
        ToggleGroup toggleGroup = new ToggleGroup();
        fftRadioButton.setToggleGroup(toggleGroup);
        reverseFftRadioButton.setToggleGroup(toggleGroup);
        battervortRadioButton.setToggleGroup(toggleGroup);
        gaussianRadioButton.setToggleGroup(toggleGroup);
    }

    private void resetRadioButtons() {
        fftRadioButton.setSelected(false);
        reverseFftRadioButton.setSelected(false);
        battervortRadioButton.setSelected(false);
        gaussianRadioButton.setSelected(false);
    }


    public interface ShowImage {
        void show(BufferedImage bufferedImage);
    }
}
