package yeah.hack.filizanka.service;

import lombok.RequiredArgsConstructor;
import org.apache.lucene.spatial.util.GeoDistanceUtils;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import yeah.hack.filizanka.model.Point;
import yeah.hack.filizanka.model.Skin;
import yeah.hack.filizanka.model.TrainRide;
import yeah.hack.filizanka.model.User;
import yeah.hack.filizanka.repository.PointRepository;
import yeah.hack.filizanka.repository.SkinRepository;
import yeah.hack.filizanka.repository.TrainRideRepository;
import yeah.hack.filizanka.repository.UserRepository;

import java.util.Comparator;
import java.util.Iterator;
import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
@Transactional
public class UserService {

    private final UserRepository userRepository;
    private final TrainRideRepository trainRideRepository;
    private final PointRepository pointRepository;
    private final SkinRepository skinRepository;


    public User getUserById(Long id) {
        return userRepository.getOne(id);
    }

    public User updateCurrentTrainRide(Long id, String trainRideId, Long startingStationId, Long destinationId) {
        final User user = userRepository.getOne(id);
        final TrainRide newTrainRide = trainRideRepository.getOne(trainRideId);

        user.setCurrentTrainRide(newTrainRide);
        if (startingStationId != null) {
            user.setLastPoint(pointRepository.getOne(startingStationId));
        } else {
            user.setLastPoint(newTrainRide.getPoints().get(0));
        }

        if (startingStationId != null) {
            user.setLastPoint(pointRepository.getOne(destinationId));
        } else {
            user.setDestination(newTrainRide.getPoints().get(newTrainRide.getPoints().size() - 1));
        }

        return userRepository.saveAndFlush(user);
    }

    public User updateActiveSkin(Long id, Long skinId) {

        final User user = userRepository.getOne(id);
        final Skin skin = skinRepository.getOne(skinId);
        user.setActiveSkin(skin);

        return userRepository.saveAndFlush(user);

    }

    public User updateUserLocation(Long id, Double lng, Double lat) {
        final User user = userRepository.getOne(id);
        final Point lastUserPoint = user.getLastPoint();
        final Point destination = user.getDestination();

        final TrainRide trainRide = user.getCurrentTrainRide();

        if (trainRide == null) {
            return user;
        }

        final List<Point> points = trainRide.getPoints();

        final int lastUserPointIndex = points.indexOf(lastUserPoint);
        final int destinationIndex = points.indexOf(destination);

        final Point newLastUserPoint =
                findClosestPoint(points.subList(lastUserPointIndex, destinationIndex + 1), lng, lat);

        System.out.println(newLastUserPoint);

        final int newLastUserPointIndex = points.indexOf(newLastUserPoint);

        hatchEggs(id, points.subList(lastUserPointIndex, newLastUserPointIndex + 1));

        user.setLastPoint(newLastUserPoint);

        if (newLastUserPoint.equals(destination)) {
            user.setCurrentTrainRide(null);
            user.setDestination(null);
        }

        return userRepository.saveAndFlush(user);
    }

    private Point findClosestPoint(List<Point> points, Double lng, Double lat) {
        return points.stream()
                .min(Comparator.comparing(point -> GeoDistanceUtils.haversin(point.getLat(), point.getLng(), lat, lng)))
                .get();
    }

    private void hatchEggs(Long id, List<Point> points) {

        if (points.size() <= 1) {
            return;
        }
        final Iterator<Point> fromIterator = points.subList(0, points.size() - 1).iterator();
        final Iterator<Point> toIterator = points.subList(1, points.size()).iterator();
        double accumulator = 0.0;
        while (fromIterator.hasNext() && toIterator.hasNext()) {
            Point from = fromIterator.next();
            Point to = toIterator.next();
            accumulator += GeoDistanceUtils.haversin(from.getLat(), from.getLng(), to.getLat(), to.getLng());
        }

        final Double accumulatedValue = accumulator;
        final User user = userRepository.getOne(id);
        user.setEggs(user.getEggs().stream().map(egg -> {
            egg.setKilometersCollected(egg.getKilometersCollected() + accumulatedValue / 1000.0);
            return egg;
        }).collect(Collectors.toSet()));

        userRepository.saveAndFlush(user);
    }
}
