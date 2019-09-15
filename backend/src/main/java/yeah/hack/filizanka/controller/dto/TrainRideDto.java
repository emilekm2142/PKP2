package yeah.hack.filizanka.controller.dto;


import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import yeah.hack.filizanka.model.TrainRide;

import java.util.List;
import java.util.stream.Collectors;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class TrainRideDto {

    private String trainRideId;

    private TrainDto train;

    private List<PointDto> points;

    private Long lastVisitedPointId;

    public static TrainRideDto from(TrainRide source) {
        return TrainRideDto.builder()
                .trainRideId(source.getTrainRideId())
                .train(TrainDto.from(source.getTrain()))
                .points(source.getPoints().stream().map(PointDto::from).collect(Collectors.toList()))
                .lastVisitedPointId(source.getLastVisitedPoint().getPointId())
                .build();
    }

}
