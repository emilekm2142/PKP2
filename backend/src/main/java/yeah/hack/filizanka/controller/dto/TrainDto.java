package yeah.hack.filizanka.controller.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import yeah.hack.filizanka.model.Train;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class TrainDto {
    private Long trainId;

    private String trainType;

    public static TrainDto from(Train source) {
        return TrainDto.builder()
                .trainId(source.getTrainId())
                .trainType(source.getTrainType())
                .build();
    }
}
