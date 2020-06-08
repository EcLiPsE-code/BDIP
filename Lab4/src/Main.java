import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;

public class Main extends Application {

    @Override
    public void start(Stage primaryStage) throws Exception{
        MainController mainController = new MainController(primaryStage);
        FXMLLoader loader = new FXMLLoader(getClass().getResource("main.fxml"));
        loader.setController(mainController);

        Scene scene = new Scene(loader.load());
        primaryStage.setTitle("Lab4");
        primaryStage.setScene(scene);
        primaryStage.show();

        mainController.setRadioButtonsGroup();
    }


    public static void main(String[] args) {
        launch(args);
    }
}
