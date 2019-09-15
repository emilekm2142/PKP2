package yeah.hack.filizanka.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import yeah.hack.filizanka.model.Point;
import yeah.hack.filizanka.model.Train;
import yeah.hack.filizanka.model.TrainRide;
import yeah.hack.filizanka.model.User;
import yeah.hack.filizanka.repository.PointRepository;
import yeah.hack.filizanka.repository.TrainRepository;
import yeah.hack.filizanka.repository.TrainRideRepository;
import yeah.hack.filizanka.repository.UserRepository;

import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
@Transactional
public class TrainRideService {

    private final TrainRideRepository trainRideRepository;
    private final PointRepository pointRepository;
    private final TrainRepository trainRepository;
    private final UserRepository userRepository;

    public TrainRide getTrainRideById(String id) {
        return trainRideRepository.getOne(id);
    }

    public Set<User> getTrainRidePassengersById(String id) {

        final TrainRide currentTrainRide = trainRideRepository.getOne(id);

        return userRepository.findAllByCurrentTrainRide(currentTrainRide);

    }

    public TrainRide updateTrainRide(String id, List<Long> pointIds, Long trainId){
        final TrainRide trainRide = trainRideRepository.getOne(id);

        if(pointIds != null) {
            final List<Point> points = pointIds.stream().map(pointRepository::getOne).collect(Collectors.toList());
            trainRide.setPoints(points);
            trainRide.setLastVisitedPoint(points.get(0));
        }

        if(trainId != null) {
            final Train train = trainRepository.getOne(trainId);
            trainRide.setTrain(train);
        }

        return trainRideRepository.saveAndFlush(trainRide);
    }

}
