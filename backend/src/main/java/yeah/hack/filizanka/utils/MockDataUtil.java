package yeah.hack.filizanka.utils;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import yeah.hack.filizanka.model.Carriage;
import yeah.hack.filizanka.model.Point;
import yeah.hack.filizanka.model.Train;
import yeah.hack.filizanka.model.TrainRide;
import yeah.hack.filizanka.model.User;
import yeah.hack.filizanka.repository.CarriageRepository;
import yeah.hack.filizanka.repository.PointRepository;
import yeah.hack.filizanka.repository.TrainRepository;
import yeah.hack.filizanka.repository.TrainRideRepository;
import yeah.hack.filizanka.repository.UserRepository;

import javax.annotation.PostConstruct;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.net.URISyntaxException;
import java.nio.file.Paths;
import java.util.List;

@Service
@Transactional
@RequiredArgsConstructor
public class MockDataUtil {

    private final static String CITIES_RESOURCE_FILE_PATH = "/static/miasta.json";
    private final static String CARRIAGES_RESOURCE_FILE_PATH = "/static/wagony.json";
    private final UserRepository userRepository;
    private final TrainRideRepository trainRideRepository;
    private final TrainRepository trainRepository;
    private final PointRepository pointRepository;
    private final CarriageRepository carriageRepository;

    @PostConstruct
    public void postConstruct() {
        importData();
    }

    public void importData() {
        ObjectMapper mapper = new ObjectMapper();
        String pointsJsonString = readLineByLine(CITIES_RESOURCE_FILE_PATH);

        List<Point> points = null;

        try {
            points = mapper.readValue(pointsJsonString, new TypeReference<List<Point>>() {
            });
        } catch (IOException e) {
            e.printStackTrace();
        }

        pointRepository.saveAll(points);

        pointRepository.flush();

        List<Carriage> carriages = null;
        String carriagesJsonString = readLineByLine(CARRIAGES_RESOURCE_FILE_PATH);

        try {
            carriages = mapper.readValue(carriagesJsonString, new TypeReference<List<Carriage>>() {
            });
        } catch (IOException e) {
            e.printStackTrace();
        }
        carriageRepository.saveAll(carriages);

        final Train pendolino = Train.builder().trainId(1L).trainType("Pendolino").build();
        final Train thomas = Train.builder().trainId(2L).trainType("Thomas").build();


        trainRepository.saveAndFlush(pendolino);
        trainRepository.saveAndFlush(thomas);


        final TrainRide trainRide = new TrainRide("1", pendolino, points, points.get(0));

        final TrainRide trainRide2 = new TrainRide("2", thomas, points.subList(0, 10), points.get(0));


        trainRideRepository.save(trainRide);
        trainRideRepository.save(trainRide2);

        userRepository.save(User.builder().userId(1L).name("Emil").currentTrainRide(trainRide)
                .destination(points.get(points.size() - 1)).lastPoint(points.get(0)).credits(0L).build());
        userRepository.save(User.builder().userId(2L).name("Kacper").currentTrainRide(trainRide)
                .destination(points.get(points.size() - 1)).lastPoint(points.get(0)).credits(0L).build());
        userRepository
                .save(User.builder().userId(3L).name("Krystian1").currentTrainRide(trainRide)
                        .destination(points.get(points.size() - 1)).lastPoint(points.get(0)).credits(0L).build());
        userRepository
                .save(User.builder().userId(4L).name("Krystian2").currentTrainRide(trainRide)
                        .destination(points.get(points.size() - 1)).lastPoint(points.get(0)).credits(0L).build());
        userRepository.save(User.builder().userId(5L).name("Paweł").currentTrainRide(trainRide)
                .destination(points.get(points.size() - 1)).lastPoint(points.get(0)).credits(0L).build());

    }

    private String readLineByLine(String resourceFilePath) {
        StringBuilder contentBuilder = new StringBuilder();
        try (BufferedReader br = new BufferedReader(
                new FileReader(Paths.get(MockDataUtil.class.getResource(resourceFilePath).toURI()).toFile()))) {

            String sCurrentLine;
            while ((sCurrentLine = br.readLine()) != null) {
                contentBuilder.append(sCurrentLine).append("\n");
            }
        } catch (IOException e) {
            e.printStackTrace();
        } catch (URISyntaxException e) {
            e.printStackTrace();
        }
        return contentBuilder.toString();
    }
}
